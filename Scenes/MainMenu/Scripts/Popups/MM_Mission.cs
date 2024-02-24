using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Mission : MonoBehaviour {
    public static MM_Mission I;
	public void Awake(){ I = this; }

    public GameObject goPop_chooseMis, goPop_confirmMis;

    public TextMeshProUGUI title, desc;

    public void setup (){
        go.SetActive (true);

        go.SetActive (false);
    }

}