
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniDialog : MonoBehaviour {

	public GameObject go;
	public TextMeshProUGUI tName, tDesc;
	public Image port;

	public List<GameObject> inputs;

	public string ID, storage1, storage2, storage3, storage4, storage5;

	public MiniDialog_Input get_input_obj (string _inputName){
		return inputs.FirstOrDefault(input => input.GetComponent<MiniDialog_Input>().inputName == _inputName).GetComponent<MiniDialog_Input>();
	}
}