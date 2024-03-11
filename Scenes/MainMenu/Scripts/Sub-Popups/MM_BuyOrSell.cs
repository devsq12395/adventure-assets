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

	public string mode;
	public MM_Inventory.Item item;

	public void show (string _mode, MM_Inventory.Item _item){
		mode = _mode;
		item = _item;
		
		go.SetActive (true);
	}

	public void hide (){
		go.SetActive (false);
	}
}