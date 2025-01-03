using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game I;
	public void Awake(){ I = this; }
    
    public bool gameReady = false, isPaused = false;

    public float checkEnemyCounter = 1f;

    void Start() {
        // Setups
        MUI_CharPane.I.setup ();
        ContMap.I.setup_map ();
        ContPlayer.I.setup_player ();
        GameUI_InGameTxt.I.setup ();
        ContItem.I.setup ();
        MUI_HPBars.I.setup ();
        MUI_Tutorial.I.setup ();

        FightCountdown.I.setup ();
        FightCountdown.I.start_count ("start");
        
        gameReady = true;

        SoundHandler.I.play_bgm ("game");

        ContObj.I.create_obj ("barrel", new Vector2 (-1, -1), 2);
        ContObj.I.create_obj ("barrel", new Vector2 (-1, -2), 2);
    }

    void Update() {
        if (!gameReady || isPaused) return;
        
        ContPlayer.I.update ();
        
        MUI_HPBars.I.update_bars ();
        MUI_CharPane.I.update ();
        ContEnemies.I.update_arrows ();

        checkEnemyCounter -= Time.deltaTime;
        if (checkEnemyCounter <= 0) {
            checkEnemyCounter = 1;
            ContDamage.I.check_enemy_count ();
        }
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
