using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Area : MonoBehaviour {
    public static MM_Area I;
	public void Awake(){ I = this; }

    public GameObject go;

    public string curArea;

    public TextMeshProUGUI title, desc;

    public void setup (){
        curArea = MM_Save.I.load ("area");

        go.SetActive (true);

        setup_texts ();

        go.SetActive (false);
    }

    public void toggle_show (bool _isShow){
        go.SetActive (_isShow);
    }

    private void setup_texts (){
        string  _login                  = MainMenu.I.login,
                _title                  = MM_Strings.I.get_str ($"{curArea}-welcome"),
                _status                 = MM_Save.I.load ("status"),
                _areaDesc               = MM_Strings.I.get_str ($"{_login}-areadesc-{curArea}-{_status}");

        title.text = _title;
        desc.text = _areaDesc;
    }

    public void btn_show_map (){
        MM_Map.I.show ();
    }
}
