using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Profile : MonoBehaviour {
    public static MM_Profile I;
	public void Awake(){ I = this; }

    public GameObject go;

    public TextMeshProUGUI title, desc;

    public void setup (){
        go.SetActive (true);

        setup_desc ();

        go.SetActive (false);
    }

    public void toggle_show (bool _isShow){
        go.SetActive (_isShow);
    }

    private void setup_desc (){
        string  _login          = MainMenu.I.login,
                _name           = $"{MM_Strings.I.get_str ("name")}{MM_Strings.I.get_str ($"{_login}-name-full")}",
                _date           = $"{MM_Strings.I.get_str ("date")}{MM_Strings.I.get_str (ZPlayerPrefs.GetString ("date"))}",
                _gold           = $"{JsonReading.I.get_str ("gold")}: {ZPlayerPrefs.GetInt("gold")}";

        desc.text = $"{_name}\n{_date}\n{_gold}";
    }
}
