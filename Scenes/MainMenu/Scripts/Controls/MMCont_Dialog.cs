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
		This is the callback database for when an input UI has been interacted
		The functions will be at the bottom of this script
	*/
	public void input (MiniDialog _dialog, string _id){
		switch (_id) {

			// On 0.3 above, the default of this will call set_dialog (_dialog, _id), preventing repetition. <- Only works for Continuation Dialogs.
			// For New Dialogs (where the old one is set to be destroyed so you need to create a new one), you will find the case statement for this above the default.

			// This is how I simplified the generic dialogs since 0.2
			case "show-dialog-field-1": set_dialog (_dialog, "dialog-field-1"); break;
			case "show-dialog-field-2": set_dialog (_dialog, "dialog-field-2"); break;
			case "show-dialog-field-3": set_dialog (_dialog, "dialog-field-3"); break;
			case "show-dialog-field-4": set_dialog (_dialog, "dialog-field-4"); break;
			case "show-dialog-field-5": set_dialog (_dialog, "dialog-field-5"); break;
			case "show-dialog-field-6": set_dialog (_dialog, "dialog-field-6"); break;
			case "start-mission-field-1": MM_Mission.I.show ("field-1"); break;

			case "show-dialog-beatrice-1": set_dialog (_dialog, "dialog-beatrice-1"); break;
			case "show-dialog-beatrice-2": set_dialog (_dialog, "dialog-beatrice-2"); break;
			case "show-dialog-beatrice-3": set_dialog (_dialog, "dialog-beatrice-3"); break;
			case "show-dialog-beatrice-4": set_dialog (_dialog, "dialog-beatrice-4"); break;
			case "show-dialog-beatrice-5": set_dialog (_dialog, "dialog-beatrice-5"); break;
			case "show-dialog-beatrice-6": set_dialog (_dialog, "dialog-beatrice-6"); break;
			case "show-dialog-beatrice-7": set_dialog (_dialog, "dialog-beatrice-7"); break;
			case "start-mission-beatrice-1": MM_Mission.I.show ("beatrice-1"); break;

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

			case "show-dialog-vic-10": show_dialog_vic_10 (_dialog); break;
			case "show-dialog-vic-11": show_dialog_vic_11 (_dialog); break;
			case "show-dialog-vic-12": show_dialog_vic_12 (_dialog); break;
			case "show-dialog-vic-13": show_dialog_vic_13 (_dialog); break;
			case "show-dialog-vic-14": show_dialog_vic_14 (_dialog); break;
			case "show-dialog-vic-15": show_dialog_vic_15 (_dialog); break;
			case "show-dialog-vic-16": show_dialog_vic_16 (_dialog); break;
			case "show-dialog-vic-17": show_dialog_vic_17 (_dialog); break;
			case "show-dialog-vic-18": show_dialog_vic_18 (_dialog); break;

			case "show-dialog-vic-19": show_dialog_vic_19 (_dialog); break;
			case "show-dialog-vic-20": show_dialog_vic_20 (_dialog); break;

			case "show-dialog-vincenzo-1": show_dialog_vincenzo_1 (_dialog); break;
			case "show-dialog-vincenzo-2": show_dialog_vincenzo_2 (_dialog); break;
			case "show-dialog-vincenzo-3": show_dialog_vincenzo_3 (_dialog); break;
			case "show-dialog-vincenzo-4": show_dialog_vincenzo_4 (_dialog); break;
			case "show-dialog-vincenzo-5": show_dialog_vincenzo_5 (_dialog); break;
			case "show-dialog-vincenzo-6": show_dialog_vincenzo_6 (_dialog); break;
			case "start-mission-vincenzo-1": start_mission_vincenzo_1 (); break;

			case "start-mission-vic-1": start_mission_vic_1 (); break;
			case "start-mission-vic-2": start_mission_vic_2 (); break;
			case "start-mission-vic-3": start_mission_vic_3 (); break;

			case "show-dialog-anthony-1": show_dialog_anthony_1 (_dialog); break;
			case "show-dialog-anthony-2": show_dialog_anthony_2 (_dialog); break;
			case "show-dialog-anthony-3": show_dialog_anthony_3 (_dialog); break;
			case "show-dialog-anthony-4": show_dialog_anthony_4 (_dialog); break;

			case "show-dialog-anthony-5": show_dialog_anthony_5 (_dialog); break;
			case "show-dialog-anthony-6": show_dialog_anthony_6 (_dialog); break;

			case "start-mission-anthony-1": start_mission_anthony_1 (); break;

			case "wooster-square-house-1": show_dialog_wooster_square_house_1 (_dialog); break;
			case "wooster-square-house-2": show_dialog_wooster_square_house_2 (_dialog); break;
			case "wooster-square-house-3": show_dialog_wooster_square_house_3 (_dialog); break;
			case "wooster-square-house-4": show_dialog_wooster_square_house_4 (_dialog); break;
			case "wooster-square-house-5": show_dialog_wooster_square_house_5 (_dialog); break;

			case "recruit-anastasia-1": show_dialog_recruit_anastasia_1 (_dialog); break;
			case "recruit-anastasia-2": show_dialog_recruit_anastasia_2 (_dialog); break;
			case "recruit-anastasia-3": show_dialog_recruit_anastasia_3 (_dialog); break;
			case "recruit-anastasia": recruit_anastasia (_dialog); break;

			case "recruit-sylphine-1": show_dialog_recruit_sylphine_1 (_dialog); break;
			case "recruit-sylphine-2": show_dialog_recruit_sylphine_2 (_dialog); break;
			case "recruit-sylphine-3": show_dialog_recruit_sylphine_3 (_dialog); break;
			case "recruit-sylphine": recruit_sylphine (_dialog); break;

			// Test Shop
			case "shopTest01": btn_shopTest01 (); break;
			case "shopSell": btn_shopSell (); break;

			case "shop-bella-vita": btn_shop ("bella-vita"); break;
			case "shop-bryans-armory": btn_shop ("bryans-armory"); break;
			case "shop-michelle-shop": btn_shop ("michelle-shop"); break;

			case "shopStregaBuy": btn_shopStregaBuy (); break;

			case "back-to-inventory": btn_backToInventory (_dialog); break;
			case "back-craft-success": btn_backCraftSuccess (_dialog); break;
			case "close-inventory-after-equip": callback_close_inventory_after_equip (_dialog); break;

			case "back": case "shopCancel": btn_back (_dialog); break;

			// New Dialogs
			case "old-tavern-stories": case "old-tavern-stories-new-haven-1": case "old-tavern-stories-4-families-1": case "the-old-tavern-close-stories":
				create_dialog (_id);
				break;
			
			// Default - Continuation Dialog
			default:
				set_dialog (_dialog, _id);
				break;
		}
	}

	/*
		BUTTONS will be set here:
	*/
	public MiniDialog create_dialog (string _dialogID){
		DB_Dialogs.DialogData _data = DB_Dialogs.I.get_dialog_data (_dialogID);
		bool _isMini = _data.isMini == "1";

		GameObject _new = Instantiate (_isMini ? goDialogMini : goDialog, goCanvas.transform);
		MiniDialog _newDialog = _new.GetComponent<MiniDialog>();
		set_dialog (_newDialog, _dialogID);

		_newDialog.tween_in ();
		return _newDialog;
	}

	public MiniDialog set_dialog (MiniDialog _dialog, string _dialogID){
		DB_Dialogs.DialogData _data = DB_Dialogs.I.get_dialog_data (_dialogID);

		if (!_dialog) {
			create_dialog (_dialogID);
		}

		_dialog.ID = _dialogID;
		_dialog.tName.text = _data.name;
		_dialog.textShowing = "";
		_dialog.textToShow = _data.desc;
		_dialog.port.sprite = Sprites.I.get_sprite (_data.portImg);
		_dialog.isTweenOut = _data.isTweenOut == "1";

		_dialog.tweeningOut = false;
		if (_dialog.tweenInText_default) {
			_dialog.tweenInText = true;
			SoundHandler.I.play_sfx ("chat");
		}

		for (int i = 0; i < _dialog.inputs.Count; i++){
			DB_Dialogs.InputData _inputData = new DB_Dialogs.InputData ("", "");
			switch (i){
				case 0: _inputData = _data.input1; break;
				case 1: _inputData = _data.input2; break;
				case 2: _inputData = _data.input3; break;
				case 3: _inputData = _data.input4; break;
			}

			MiniDialog_Input _script = _dialog.inputs [i].GetComponent<MiniDialog_Input>();
			
			string _id = _inputData.id;
			_script.txt.text = _inputData.text;

			if (_id == "") {
				_dialog.inputs [i].SetActive (false);
			} else {
				_dialog.inputs [i].SetActive (true);
				_script.ID = _id;
			}
		}

		_dialog.windows_showOnTweenDone.ForEach ((_window) => _window.gameObject.SetActive (false));
		_dialog.onContinueCallbackID = _data.inputEmptyContinue;

		return _dialog;
	}

	/*
		Functions for the input UI goes here
	*/
	public void show_intro_1 () => create_dialog ("intro-1");
	public void show_intro_2 (MiniDialog _dialog) {
		set_dialog (_dialog, "intro-2");
		ZPlayerPrefs.SetString("main-menu-start-callback", "");
	}

	public void show_dialog_vic_2 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-2");
	public void show_dialog_vic_3 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-3");
	public void show_dialog_vic_4 (MiniDialog _dialog) {
		set_dialog (_dialog, "dialog-vic-4");
		PlayerPrefs.SetInt("activity.dialog-with-vic", 1);
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
		PlayerPrefs.SetInt("activity.dialog-with-vic", 3);
	}
	public void start_mission_vic_2 (){
		MM_Mission.I.show ("vic-2");
	}

	public void show_dialog_vic_10 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-10");
	public void show_dialog_vic_11 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-11");
	public void show_dialog_vic_12 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-12");
	public void show_dialog_vic_13 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-13");
	public void show_dialog_vic_14 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-14");
	public void show_dialog_vic_15 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-15");
	public void show_dialog_vic_16 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-16");
	public void show_dialog_vic_17 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-17");
	public void show_dialog_vic_18 (MiniDialog _dialog) {
		set_dialog (_dialog, "dialog-vic-18");
		PlayerPrefs.SetInt("activity.dialog-with-vic", 5);
	}
	public void start_mission_vic_3 (){
		MM_Mission.I.show ("vic-3");
	}

	public void show_dialog_vic_19 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-19");
	public void show_dialog_vic_20 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vic-20");

	public void show_dialog_vincenzo_1 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vincenzo-1");
	public void show_dialog_vincenzo_2 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vincenzo-2");
	public void show_dialog_vincenzo_3 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vincenzo-3");
	public void show_dialog_vincenzo_4 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vincenzo-4");
	public void show_dialog_vincenzo_5 (MiniDialog _dialog) {
		set_dialog (_dialog, "dialog-vincenzo-5");
		PlayerPrefs.SetInt("activity.dialog-with-vincenzo", 5);
	}
	public void start_mission_vincenzo_1 (){
		MM_Mission.I.show ("vincenzo-1");
	}

	public void show_dialog_vincenzo_6 (MiniDialog _dialog) => set_dialog (_dialog, "dialog-vincenzo-6");

	public void show_dialog_anthony_1 (MiniDialog _dialog) => set_dialog (_dialog, "anthony-1");
	public void show_dialog_anthony_2 (MiniDialog _dialog) => set_dialog (_dialog, "anthony-2");
	public void show_dialog_anthony_3 (MiniDialog _dialog) => set_dialog (_dialog, "anthony-3");
	public void show_dialog_anthony_4 (MiniDialog _dialog) {
		set_dialog (_dialog, "anthony-4");
		PlayerPrefs.SetInt("activity.dialog-with-anthony", 1);
	}
	public void start_mission_anthony_1 (){
		MM_Mission.I.show ("anthony-1");
	}

	public void show_dialog_anthony_5 (MiniDialog _dialog) => set_dialog (_dialog, "anthony-5");
	public void show_dialog_anthony_6 (MiniDialog _dialog) => set_dialog (_dialog, "anthony-6");

	public void show_dialog_wooster_square_house_1 (MiniDialog _dialog) => set_dialog (_dialog, "wooster-square-house-1");
	public void show_dialog_wooster_square_house_2 (MiniDialog _dialog) => set_dialog (_dialog, "wooster-square-house-2");
	public void show_dialog_wooster_square_house_3 (MiniDialog _dialog) => set_dialog (_dialog, "wooster-square-house-3");
	public void show_dialog_wooster_square_house_4 (MiniDialog _dialog) => set_dialog (_dialog, "wooster-square-house-4");
	public void show_dialog_wooster_square_house_5 (MiniDialog _dialog) => set_dialog (_dialog, "wooster-square-house-5");

	public void show_dialog_recruit_anastasia_1 (MiniDialog _dialog) {
		if (ZPlayerPrefs.GetInt("charUnlocked.anastasia") == 1) {
			create_dialog ("hero-recruited");
		} else {
			create_dialog ("recruit-anastasia-1");
		}
	}
	public void show_dialog_recruit_anastasia_2 (MiniDialog _dialog) => set_dialog (_dialog, "recruit-anastasia-2");
	public void show_dialog_recruit_anastasia_3 (MiniDialog _dialog) => set_dialog (_dialog, "recruit-anastasia-3");
	public void recruit_anastasia (MiniDialog _dialog) {
		MM_Craft.I.show ("anastasia", "char");
	}

	public void show_dialog_recruit_sylphine_1 (MiniDialog _dialog) {
		if (ZPlayerPrefs.GetInt("charUnlocked.sylphine") == 1) {
			create_dialog ("hero-recruited");
		} else {
			create_dialog ("recruit-sylphine-1");
		}
	}
	public void show_dialog_recruit_sylphine_2 (MiniDialog _dialog) => set_dialog (_dialog, "recruit-sylphine-2");
	public void show_dialog_recruit_sylphine_3 (MiniDialog _dialog) => set_dialog (_dialog, "recruit-sylphine-3");
	public void recruit_sylphine (MiniDialog _dialog) {
		MM_Craft.I.show ("sylphine", "char");
	}

	private void btn_shopTest01 (){
		
	}

	private void btn_shopStregaBuy (){
		MM_Inv2.I.itemSet = "strega";
		MM_Inv2.I.show ("shop");
	}

	private void btn_shop (string _shopName){
		MM_Inv2.I.itemSet = _shopName;
		MM_Inv2.I.show ("shop");
	}

	private void btn_shopSell (){
		
	}

	private void btn_backToInventory (MiniDialog _dialog){
		MM_BuyOrSell.I.hide ();
		_dialog.tween_out ();
	}

	private void callback_close_inventory_after_equip (MiniDialog _dialog){
		MM_Inv2.I.hide ();
		MM_Char.I.set_equips ();
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