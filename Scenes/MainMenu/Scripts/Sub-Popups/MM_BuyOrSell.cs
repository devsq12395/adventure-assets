using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_BuyOrSell : MonoBehaviour {
    public static MM_BuyOrSell I;
	public void Awake(){ I = this; }

	public GameObject go;
	public Image imgWindow;
	
	public TextMeshProUGUI tName, tDesc;

	public string mode;
	public Inv2.Item item;

	private int amountCur, amountMax, cost, costTotal;
	private string nameUI, costStr;

	public void show (string _mode, Inv2.Item _item){
		mode = _mode;
		item = _item;

		amountCur = 1;
		amountMax = (mode == "sell") ? _item.stack : 9999;

		Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (item.name);
		nameUI = _data.nameUI;
		costStr = $"${_data.cost}";
		cost = _data.cost;
		cost /= (mode == "buy") ? 1 : 2;
		costTotal = cost;
		
		go.SetActive (true);
		imgWindow.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
		imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

		setup_texts ();
	}

	public void hide (){
		imgWindow.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
	}

	private void setup_texts (){
		tName.text = nameUI;

		switch (mode) {
			case "buy":
				tDesc.text = $"Cost: ${cost}\nAmount to buy: {amountCur}\nTotal Costs: ${costTotal}";
				break;
			case "sell":
				tDesc.text = $"Sells for: {cost}\nAmount to sell: {amountCur}/{amountMax}\nYou'll get: ${costTotal}";
				break;
		}
	}

	public void btn_modify_amount (int _inc) {
		amountCur = Mathf.Clamp (amountCur + _inc, 1, amountMax);
		costTotal = amountCur * cost;

		setup_texts ();
	}

	public void btn_action (){
		int gold = ZPlayerPrefs.GetInt("gold");

		switch (mode) {
			case "buy":
				if (gold >= costTotal) {
					MainMenu.I.update_gold (-costTotal);
					Inv2.I.add_item (item.name);
					MMCont_Dialog.I.create_dialog ("buy-success");
					SoundHandler.I.play_sfx ("buying");
				} else {
					MMCont_Dialog.I.create_dialog ("buy-not-enough-gold");
				}
				hide ();
				break;
			case "sell":
				MainMenu.I.update_gold (costTotal);
				// MM_Inventory.I.remove_stack (item, amountCur); - MM_Inventory is now unused as of 2024/08/13
				SoundHandler.I.play_sfx ("buying");
				hide ();
				break;
		}
	}
}