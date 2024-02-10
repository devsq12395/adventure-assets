using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public static MainMenu I;
	public void Awake(){ I = this; }

    void Start() {
        MM_Save.I.setup ();
    }

    void Update() {
        
    }

    
}
