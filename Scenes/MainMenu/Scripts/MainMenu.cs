using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public static MainMenu I;
	public void Awake(){ I = this; }

    public string login;

    void Start() {
        login = "tommy";

        // Setup all scripts
        MM_Save.I.setup ();

        MM_Profile.I.setup ();
        MM_Area.I.setup ();
        MM_Party.I.setup ();

        MM_ChangeParty.I.setup ();
        MM_Inventory.I.setup ();
        MM_Char.I.setup ();
        MM_Mission.I.setup ();

        MM_Map.I.setup ();

        // Generate map
        MM_Map.I.generate_map ();

        show_popup ("profile");
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
}
