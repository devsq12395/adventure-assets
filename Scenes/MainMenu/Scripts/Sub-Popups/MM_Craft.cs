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

	public TextMeshProUGUI tName, tDesc, tRequires, tWindowTitle, tBtnText;
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
		imgWindow.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
		imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);	

		item = _name;
		type = _type;

		string _goldReq, _itemReq;

		switch (type) {
			case "item":
				// Need to create one that will use Inv2.cs, when you need it
				break;
			case "char":
				tWindowTitle.text = "Recruit Character";
				tBtnText.text = "Recruit";

				DB_Chars.CharData _charData = DB_Chars.I.get_char_data (_name);

				if (ZPlayerPrefs.GetInt ($"charUnlocked.{_name}") == 1) {
					go.SetActive (false);					
					MMCont_Dialog.I.create_dialog ("hero-recruited");
					return;
				}

				tName.text = _charData.nameUI;
				tDesc.text = _charData.desc;

				goldCost = _charData.goldCost;

				_goldReq = $"Recruit Cost: {goldCost.ToString ()}";
				iPort.sprite = Sprites.I.get_sprite (_name);

				break;
		}
	}

	public void craft (){
		int _playerGold = ZPlayerPrefs.GetInt ("gold");
		MM_Inventory.Item _itemInLoop;
		Dictionary<MM_Inventory.Item, int> _items = new Dictionary<MM_Inventory.Item, int> ();

		if (goldCost > _playerGold) {
			MMCont_Dialog.I.create_dialog ("buy-not-enough-gold");
			return;
		}

		SaveHandler.I.gain_gold (-goldCost);

		switch (type) {
			case "item":
				// Need to create one that will use Inv2.cs, when you need it
				break;
			case "char":
				ZPlayerPrefs.SetInt ($"charUnlocked.{item}", 1);
				MMCont_Dialog.I.create_dialog ("buy-recruit-success");
				SoundHandler.I.play_sfx ("buying");
				break;
		}
	}

	public void hide (){
		imgWindow.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
	}

}