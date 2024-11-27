using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_TutKey : MonoBehaviour {
    public static MM_TutKey I;
	public void Awake(){ I = this; }

	public GameObject go, goUIMove, goUIEnter;
	public bool showEnter;

	public void setup (){
		showEnter = true;
	}

	public void show_all (bool _isShow){
		go.SetActive (_isShow);
	}

	public void show_one (string _whichOne, bool _isShow){
		switch (_whichOne){
			case "move": goUIMove.SetActive (_isShow); break;
			case "enter": 
				if (showEnter) {
					if (!_isShow) {
						goUIEnter.SetActive (false); 
						showEnter = false;
					}
				} else {
					if (_isShow) {
						goUIEnter.SetActive (true); 
						showEnter = true;
					}
				}
				break;
		}
	}

}