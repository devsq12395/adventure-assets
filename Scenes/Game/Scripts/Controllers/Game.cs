using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public static Game I;
	public void Awake(){ I = this; }
    
    public bool gameReady = false, isPaused = false;

    void Start() {
        // Setups
        MUI_CharPane.I.setup ();
        string _mapName = ContMap.I.setup_map ();
        ContPlayer.I.setup_player ();
        GameUI_InGameTxt.I.setup ();
        ContItem.I.setup ();
        MUI_HPBars.I.setup ();
        MUI_Tutorial.I.setup ();
        ContScore.I.setup ();
        ContCollectibles.I.spawn_collectible_per_map_piece();

        string _mission = ZPlayerPrefs.GetString("missionCur");
        DB_Missions.MissionData _data = DB_Missions.I.get_mission_data(_mission);
        ContObjective.I.setup_and_set_starting_objective (_data.objective);
        
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
        ContEnemies.I.check_spawn_enemies ();
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
