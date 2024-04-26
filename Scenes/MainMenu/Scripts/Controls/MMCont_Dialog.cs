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
			// Dialogs
			case "show-intro-2": show_intro_2 (_dialog); break;
			case "show-dialog-vic-2": show_dialog_vic_2 (_dialog); break;
			case "show-dialog-vic-3": show_dialog_vic_3 (_dialog); break;
			case "show-dialog-vic-4": show_dialog_vic_4 (_dialog); break;

			case "show-dialog-vic-5": show_dialog_vic_5 (_dialog); break;
			case "show-dialog-vic-6": show_dialog_vic_6 (_dialog); break;
			case "show-dialog-vic-7": show_dialog_vic_7 (_dialog); break;
			case "show-dialog-vic-8": show_dialog_vic_8 (_dialog); break;
			case "show-dialog-vic-9": show_dialog_vic_9 (_dialog); break;

			case "start-mission-vic-1": start_mission_vic_1 (); break;
			case "start-mission-vic-2": start_mission_vic_2 (); break;

			// Test Shop
			case "shopTest01": btn_shopTest01 (); break;
			case "shopSell": btn_shopSell (); break;
			case "shop-bella-vita": btn_shop ("bella-vita"); break;

			case "shopStregaBuy": btn_shopStregaBuy (); break;

			case "recruit-anastasia": btn_recruitAnastasia (_dialog); break;

			case "back-to-inventory": btn_backToInventory (_dialog); break;
			case "back-craft-success": btn_backCraftSuccess (_dialog); break;

			case "back": btn_back (_dialog); break;
			
			// Default
			default:
				
				break;
		}
	}

	/*
		BUTTONS will be set here:
	*/
	public MiniDialog create_dialog (string _dialogID){
		string _json = $"dialogs.{_dialogID}";
		bool _isMini = JsonReading.I.read ("dialogs", $"{_json}.isMini") == "1";

		GameObject _new = Instantiate (_isMini ? goDialogMini : goDialog, goCanvas.transform);
		MiniDialog _newDialog = _new.GetComponent<MiniDialog>();
		set_dialog (_newDialog, _dialogID);

		_newDialog.tween_in ();
		return _newDialog;
	}

	public MiniDialog set_dialog (MiniDialog _dialog, string _dialogID){
		string _json = $"dialogs.{_dialogID}";

		_dialog.ID = _dialogID;
		_dialog.tName.text = JsonReading.I.read ("dialogs", $"{_json}.name");
		_dialog.textShowing = "";
		_dialog.textToShow = JsonReading.I.read ("dialogs", $"{_json}.desc");
		_dialog.port.sprite = Sprites.I.get_sprite (JsonReading.I.read ("dialogs", $"{_json}.portImg"));
		_dialog.isTweenOut = JsonReading.I.read ("dialogs", $"{_json}.isTweenOut") == "1";

		_dialog.tweeningOut = false;
		if (_dialog.tweenInText_default) _dialog.tweenInText = true;

		foreach (GameObject _input in _dialog.inputs) {
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
		_dialog.windows_showOnTweenDone.ForEach ((_window) => _window.gameObject.SetActive (false));
		_dialog.onContinueCallbackID = JsonReading.I.read ("dialogs", $"{_json}.inputEmptyContinue");

		return _dialog;
	}

	/*
		Functions for the input UI goes here
	*/
	public void show_intro_1 () => create_dialog ("intro-1");
	public void show_intro_2 (MiniDialog _dialog) {
		set_dialog (_dialog, "intro-2");
		JsonSaving.I.save ("main-menu-start-callback", "");
	}

	public void show_dialog_vic_2 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-2");
	public void show_dialog_vic_3 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-3");
	public void show_dialog_vic_4 (MiniDialog _dialog) {
		set_dialog (_dialog, "dialog-vic-4");
		JsonSaving.I.save ("activity.dialog-with-vic", "1");
	}
	public void start_mission_vic_1 (){
		MM_Mission.I.show ("vic-1");
	}

	public void show_dialog_vic_5 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-5");
	public void show_dialog_vic_6 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-6");
	public void show_dialog_vic_7 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-7");
	public void show_dialog_vic_8 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-8");
	public void show_dialog_vic_9 (MiniDialog _dialog) {
		set_dialog (_dialog, "dialog-vic-9");
		JsonSaving.I.save ("activity.dialog-with-vic", "3");
	}
	public void start_mission_vic_2 (){
		MM_Mission.I.show ("vic-2");
	}

	private void btn_shopTest01 (){
		MM_Inventory.I.show ("buy", "test-shop");
	}

	private void btn_shopStregaBuy (){
		MM_Inventory.I.show ("buy", "strega");
	}

	private void btn_shop (string _shopName){
		MM_Inventory.I.show ("buy", _shopName);
	}

	private void btn_recruitAnastasia (MiniDialog _dialog){
		MM_Craft.I.show ("anastasia", "char");
		_dialog.tween_out ();
	}

	private void btn_shopSell (){
		MM_Inventory.I.show ("sell", "");
	}

	private void btn_backToInventory (MiniDialog _dialog){
		MM_BuyOrSell.I.hide ();
		_dialog.tween_out ();
	}

	private void btn_backCraftSuccess (MiniDialog _dialog){
		MM_Craft.I.hide ();
		_dialog.tween_out ();
	}

	private void btn_back (MiniDialog _dialog){ 
		_dialog.tween_out ();
	}

	/*
		Special Dialog texts
	*/
	public void set_special_texts (MiniDialog _dialog, string _dialogID){
		switch (_dialogID) {
			default: break;
		}
	}
}