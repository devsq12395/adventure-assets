using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public static MainMenu I;
	public void Awake(){ I = this; }

    public string login;

    void Start() {
        login = "tommy";

        MM_Save.I.setup ();

        MM_Profile.I.setup ();
        MM_Area.I.setup ();

        show_popup ("profile");
    }

    void Update() {
        
    }

    public void show_popup (string _popup){
        switch (_popup) {
            case "profile":             MM_Profile.I.toggle_show (true); break;
            case "area":                MM_Area.I.toggle_show (true); break;
        }
    }
}
