
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniDialog_Input : MonoBehaviour {

	public GameObject go;
	public MiniDialog dialogP;
	public TextMeshProUGUI txt;
	public TMP_InputField inputField;

	public string inputName, ID;

	public void input_send (){
		dialogP.tweenOutCallbackID = ID;
		dialogP.tween_out ();
	}
}