using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Area : MonoBehaviour {
    public static MM_Area I;
	public void Awake(){ I = this; }

    public GameObject go;

    public void setup (){
        go.SetActive (true);

        go.SetActive (false);
    }

    public void toggle_show (bool _isShow){
        go.SetActive (_isShow);
    }

    public void btn_show_map (string _mapID){
        MM_Map.I.show (_mapID);
    }
}
