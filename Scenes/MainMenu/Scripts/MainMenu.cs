using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {
    public static MainMenu I;
	public void Awake(){ I = this; }

    public string login;

    public TextMeshProUGUI headerTxt_Gold;

    void Start() {
        login = "tommy";

        // Setup all scripts
        MM_Profile.I.setup ();
        MM_Area.I.setup ();
        MM_Party.I.setup ();

        MM_ItemCheck.I.setup ();
        MM_ChangeParty.I.setup ();
        MM_Inventory.I.setup ();
        MM_Char.I.setup ();
        MM_Mission.I.setup ();
        MM_Craft.I.setup ();

        MM_Map.I.setup ();

        update_header ();
    }

    void Update() {
        
    }

    public void show_popup (string _popup){
        MM_Profile.I.toggle_show (false);
        MM_Area.I.toggle_show (false);
        MM_Party.I.toggle_show (false);

        switch (_popup) {
            case "profile":             MM_Profile.I.toggle_show (true); break;
            case "area":                MM_Area.I.toggle_show (true); break;
            case "party":               MM_Party.I.toggle_show (true); break;
        }
    }

    public void update_header (){
        headerTxt_Gold.text = JsonSaving.I.load ("gold");
    }

    public void update_gold (int _inc){
        int gold = int.Parse (JsonSaving.I.load ("gold"));
        gold += _inc;
        JsonSaving.I.save ("gold", gold.ToString ());
        update_header ();
    }
}
