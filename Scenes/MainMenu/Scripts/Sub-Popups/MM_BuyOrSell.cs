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
	public MM_Inventory.Item item;

	private int amountCur, amountMax, cost, costTotal;
	private string nameUI, costStr;

	public void show (string _mode, MM_Inventory.Item _item){
		mode = _mode;
		item = _item;

		amountCur = 1;
		amountMax = (mode == "sell") ? _item.stack : 9999;

		nameUI = JsonReading.I.read ("items", $"items.{item.name}.name-ui");
		costStr = JsonReading.I.read ("items", $"items.{item.name}.cost");
		cost = int.Parse (costStr);
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
		int gold = int.Parse (JsonSaving.I.load ("gold"));

		switch (mode) {
			case "buy":
				if (gold >= costTotal) {
					MainMenu.I.update_gold (-costTotal);
					MM_Inventory.I.add_item (item.name, amountCur);
					MMCont_Dialog.I.create_dialog ("buy-success");
				} else {
					MMCont_Dialog.I.create_dialog ("buy-not-enough-gold");
				}
				hide ();
				break;
			case "sell":
				MainMenu.I.update_gold (costTotal);
				MM_Inventory.I.remove_stack (item, amountCur);
				hide ();
				break;
		}
	}
}