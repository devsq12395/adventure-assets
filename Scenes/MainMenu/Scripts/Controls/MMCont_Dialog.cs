using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MMCont_Dialog : MonoBehaviour {
    public static MMCont_Dialog I;
	public void Awake(){ I = this; }

	public GameObject goDialog, goCanvas;

	/*
		This is the database for when an input UI has been interacted
		The functions will be at the bottom of this script
	*/
	public void input (MiniDialog _dialog, string _id){
		switch (_id) {
			// Test Shop
			case "shopTest01": btn_shopTest01 (); break;
			case "shopTest02": btn_shopTest02 (); break;
			
			// Default
			default:
				Destroy (_dialog.go);
				break;
		}
	}

	/*
		BUTTONS will be set here:
	*/
	public MiniDialog create_dialog (string _dialogID){
		GameObject _new = Instantiate (goDialog, goCanvas.transform);
		MiniDialog _newDialog = _new.GetComponent<MiniDialog>();
		string _json = $"dialogs.{_dialogID}";

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

	private void btn_shopTest02 (){
		MM_Inventory.I.show ("sell", "");
	}

	/*
		Special Dialog texts
	*/
	public void set_special_texts (MiniDialog _dialog, string _dialogID){
		switch (_dialogID) {
			
		}
	}
}