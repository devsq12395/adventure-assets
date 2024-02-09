using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI_Upg : MonoBehaviour
{
    
    public static GameUI_Upg I;
	public void Awake(){ I = this; }

    public GameObject go;
    public int NUMBER_OF_BUTTONS;
    public List<Button> buttons;
    public List<TextMeshProUGUI> txt;
    public List<DB_Upgrade.upgrade> upgrades;
    
    public bool isShow = false;

    void Start() {
        go.SetActive (true);

        upgrades = new List<DB_Upgrade.upgrade> ();

        go.SetActive (false);
    }

    void Update() {
        
    }

    public void setup (){
        for (int i = 0; i < NUMBER_OF_BUTTONS; i++){
            DB_Upgrade.upgrade _new = DB_Upgrade.I.get_upgrade ();
            upgrades.Add (_new);

            txt [i].text = _new.name;
        }
    }

    public void show (){
        go.SetActive (true);
        isShow = true;
        setup ();
    }

    public void hide (){
        go.SetActive (false);
        isShow = false;
    }
}
