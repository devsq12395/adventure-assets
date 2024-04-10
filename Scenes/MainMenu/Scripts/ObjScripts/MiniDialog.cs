
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MiniDialog : MonoBehaviour {

	public GameObject go;
	public TextMeshProUGUI tName, tDesc;
	public Image port;
	public List<Image> windows;

	public List<GameObject> inputs;

	public string ID, storage1, storage2, storage3, storage4, storage5;

	public MiniDialog_Input get_input_obj (string _inputName){
		return inputs.FirstOrDefault(input => input.GetComponent<MiniDialog_Input>().inputName == _inputName).GetComponent<MiniDialog_Input>();
	}

	public void tween_in (){
		windows.ForEach ((_window) => {
            _window.transform.localScale = Vector3.zero;
            _window.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        });
	}

	public void tween_out (){
		windows.ForEach ((_window) => {
            _window.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            	if (go) Destroy (go);
            });
        });
	}
}