using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Craft : MonoBehaviour {
    public static MM_Craft I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Image imgWindow;

	public TextMeshProUGUI tName, tDesc, tRequires;
	public Image iPort;

	public string item, type; // type: item, char

	private int goldCost;
	private Dictionary<string, int> itemReqs;

	public void setup (){
		itemReqs = new Dictionary<string, int>();
	}

	public void show (string _name, string _type){
		itemReqs.Clear ();

		go.SetActive (true);
		imgWindow.transform.localScale = Vector3.zero;
		imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);	

		item = _name;
		type = _type;

		string _goldReq, _itemReq;

		switch (type) {
			case "item":
				tName.text = JsonReading.I.read ("items", $"items.{_name}.name-ui");
				tDesc.text = JsonReading.I.read ("items", $"items.{_name}.desc");

				_goldReq = $"{JsonReading.I.get_str ("gold")}: {JsonReading.I.read ("items", $"items.{_name}.cost")}";
				_itemReq = string.Join ("\n", JsonReading.I.read ("items", $"items.{_name}.requires").Split(',').Select((_req) => {
					string[] _values = _req.Split ('%');
					itemReqs.Add (_values [1], int.Parse (_values [0]));
					return $"{JsonReading.I.read ("items", $"items.{_values [1]}.name-ui")} x{_values [0]}";
				}));

				tRequires.text = $"{JsonReading.I.get_str ("requires")}:\n{_goldReq}\n{_itemReq}";

				goldCost = int.Parse (JsonReading.I.read ("items", $"items.{_name}.cost"));
				break;
			case "char":
				if (JsonSaving.I.load ("pool").Split (',').Contains (_name)) {
					go.SetActive (false);					
					MMCont_Dialog.I.create_dialog ("hero-recruited");
					return;
				}

				tName.text = JsonReading.I.get_str ($"{_name}-name");
				tDesc.text = JsonReading.I.read ("chars", $"chars.{_name}.bio.info");

				_goldReq = $"{JsonReading.I.get_str ("gold")}: 0";
				_itemReq = string.Join ("\n", JsonReading.I.read ("chars", $"chars.{_name}.requires").Split(',').Select((_req) => {
					string[] _values = _req.Split ('%');
					itemReqs.Add (_values [1], int.Parse (_values [0]));
					return $"{JsonReading.I.read ("items", $"items.{_values [1]}.name-ui")} x{_values [0]}";
				}));

				tRequires.text = $"{JsonReading.I.get_str ("requires")}:\n{_goldReq}\n{_itemReq}";

				goldCost = 0;

				break;
		}
	}

	public void craft (){
		int _playerGold = int.Parse (JsonSaving.I.load ("gold"));
		MM_Inventory.Item _itemInLoop;
		Dictionary<MM_Inventory.Item, int> _items = new Dictionary<MM_Inventory.Item, int> ();

		if (goldCost > _playerGold) {
			MMCont_Dialog.I.create_dialog ("buy-not-enough-gold");
			return;
		}

		foreach (var _reqs in itemReqs) {
			if (!MM_Inventory.I.has_item_from_inv (_reqs.Key)) {
				MMCont_Dialog.I.create_dialog ("buy-not-enough-reqs");
				return;
			} else {
				_itemInLoop = MM_Inventory.I.get_item_from_inv (_reqs.Key);
				if (_itemInLoop.stack < _reqs.Value) {
					MMCont_Dialog.I.create_dialog ("buy-not-enough-reqs");
					return;
				} else {
					_items.Add (_itemInLoop, _reqs.Value);
				}
			}
		}
		if (goldCost > _playerGold) {
			MMCont_Dialog.I.create_dialog ("buy-not-enough-reqs");
			return;
		}

		JsonSaving.I.gain_gold (-goldCost);
		foreach (var _item in _items) {
			MM_Inventory.I.remove_stack (_item.Key, _item.Value);
		}
		switch (type) {
			case "item":
				MM_Inventory.I.add_item (item, 1);
				MMCont_Dialog.I.create_dialog ("buy-craft-success");
				break;
			case "char":
				List<string> _chars = JsonSaving.I.load ("pool").Split (',').ToList ();
				_chars.Add (item);
				JsonSaving.I.save ("pool", string.Join (',', _chars.ToArray()));
				MMCont_Dialog.I.create_dialog ("buy-recruit-success");
				break;
		}
	}

	public void hide (){
		imgWindow.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
	}

}