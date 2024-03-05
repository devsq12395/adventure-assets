using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MMCont_Dialog : MonoBehaviour {
    public static MMCont_Dialog I;
	public void Awake(){ I = this; }

	public GameObject goDialog, goCanvas;

	public void create_dialog (string _dialogID){
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

			if (_id == "") {
				_input.SetActive (false);
			} else {
				_input.SetActive (true);

				_script.ID = _id;
				if (_script.inputName != "inputTxtBox") {
					_script.txt.text = JsonReading.I.read ("dialogs", $"{_jsonIn}.text");
				}
			}
		}
	}

	public void input (MiniDialog _dialog, string _id){
		switch (_id) {

			default:
				Destroy (_dialog.go);
				break;
		}
	}
}