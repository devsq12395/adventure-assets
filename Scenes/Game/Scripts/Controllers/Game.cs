using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game I;
	public void Awake(){ I = this; }
    
    public bool gameReady = false, isPaused = false;

    void Start() {
        // PlayerPrefs for testing
        PlayerPrefs.SetInt ("player_charSel", 0);

        PlayerPrefs.SetString ("Item1", "sample1");
        PlayerPrefs.SetInt ("Item1_Stack", 5);
        for (int i = 2; i <= 20; i++) {
            PlayerPrefs.SetString ("Item" + i.ToString (), "");
            PlayerPrefs.SetInt ("Item" + i.ToString () + "_Stack", 0);
        }
        
        // Setups
        MUI_CharPane.I.setup ();
        ContMap.I.setup_map ();
        ContPlayer.I.setup_player ();
        GameUI_InGameTxt.I.setup ();
        ContItem.I.setup ();

        ContEnemies.I.spawn_enemies ();
        
        gameReady = true;
    }

    void Update() {
        if (!gameReady || isPaused) return;
        
        ContPlayer.I.update ();
        
        MUI_HPBars.I.update_bars ();
        MUI_CharPane.I.update ();
        ContEnemies.I.update_arrows ();
    }

    public void pause_game (){
        isPaused = true;
    }

    public void resume_game (){
        isPaused = false;
    }

    // Accesibility
    public GameObject get_player_obj (){
        return ContPlayer.I.player.gameObject;
    }
}
