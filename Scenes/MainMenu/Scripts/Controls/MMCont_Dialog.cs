using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MMCont_Dialog : MonoBehaviour {
    public static MMCont_Dialog I;
	public void Awake(){ I = this; }

	public GameObject goDialog, goDialogMini, goCanvas;

	/*
		This is the database for when an input UI has been interacted
		The functions will be at the bottom of this script
	*/
	public void input (MiniDialog _dialog, string _id){
		switch (_id) {
			// Test Shop
			case "shopTest01": btn_shopTest01 (); break;
			case "shopSell": btn_shopSell (); break;
			case "shop-bella-vita": btn_shop ("bella-vita"); break;

			case "shopStregaBuy": btn_shopStregaBuy (); break;

			case "recruit-anastasia": btn_recruitAnastasia (); break;

			case "back-to-inventory": btn_backToInventory (); break;
			case "back-craft-success": btn_backCraftSuccess (); break;
			
			// Default
			default:
				
				break;
		}

		Destroy (_dialog.go);
	}

	/*
		BUTTONS will be set here:
	*/
	public MiniDialog create_dialog (string _dialogID){
		string _json = $"dialogs.{_dialogID}";
		bool _isMini = JsonReading.I.read ("dialogs", $"{_json}.isMini") == "1";

		GameObject _new = Instantiate (_isMini ? goDialogMini : goDialog, goCanvas.transform);
		MiniDialog _newDialog = _new.GetComponent<MiniDialog>();

		_new.transform.localPosition = new Vector2 (
			float.Parse (JsonReading.I.read ("dialogs", $"{_json}.posX")),
			float.Parse (JsonReading.I.read ("dialogs", $"{_json}.posY"))
		);

		_newDialog.ID = _dialogID;
		_newDialog.tName.text = JsonReading.I.read ("dialogs", $"{_json}.name");
		_newDialog.tDesc.text = JsonReading.I.read ("dialogs", $"{_json}.desc");
		_newDialog.port.sprite = MM_Sprites.I.get_sprite (JsonReading.I.read ("dialogs", $"{_json}.portImg"));

		foreach (GameObject _input in _newDialog.inputs) {
			MiniDialog_Input _script = _input.GetComponent<MiniDialog_Input>();
			
			string 	_jsonIn = $"{_json}.{_script.inputName}",
				_id = JsonReading.I.read ("dialogs", $"{_jsonIn}.id");

			_script.txt.text = JsonReading.I.read ("dialogs", $"{_jsonIn}.text");

			if (_id == "") {
				_input.SetActive (false);
			} else {
				_input.SetActive (true);
				_script.ID = _id;
			}
		}

		return _newDialog;
	}

	/*
		Functions for the input UI goes here
	*/
	private void btn_shopTest01 (){
		MM_Inventory.I.show ("buy", "test-shop");
	}

	private void btn_shopStregaBuy (){
		MM_Inventory.I.show ("buy", "strega");
	}

	private void btn_shop (string _shopName){
		MM_Inventory.I.show ("buy", _shopName);
	}

	private void btn_recruitAnastasia (){
		MM_Craft.I.show ("anastasia", "char");
	}

	private void btn_shopSell (){
		MM_Inventory.I.show ("sell", "");
	}

	private void btn_backToInventory (){
		MM_BuyOrSell.I.hide ();
	}

	private void btn_backCraftSuccess (){
		MM_Craft.I.hide ();
	}

	/*
		Special Dialog texts
	*/
	public void set_special_texts (MiniDialog _dialog, string _dialogID){
		switch (_dialogID) {
			
		}
	}
}