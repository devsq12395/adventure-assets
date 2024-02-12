using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_ChangeParty : MonoBehaviour {
    public static MM_ChangeParty I;
	public void Awake(){ I = this; }

    public GameObject go;

    public void toggle_show (bool _isShow){
        go.SetActive (_isShow);
    }
}
