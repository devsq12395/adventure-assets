using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {    

    public static GameInput I;
	public void Awake(){ I = this; }

    public bool mouseClicked;

    private Vector2 heroMov = new Vector2 (0, 0);
    void Start() {
        
    }

    void Update() {
        key_input ();
        mouse_input ();
    }

    public void key_input (){
        heroMov.x = Input.GetAxis("Horizontal");
        heroMov.y = Input.GetAxis("Vertical");
        ContObj.I.input_move (ContPlayer.I.player, heroMov);

        if (Input.GetButtonDown ("SkillShift")) ContPlayer.I.use_skill ("shift");
        for (int _cs = 0; _cs < GameConstants.MAX_NUM_OF_SKILLS; _cs++) {
            if (Input.GetButtonDown ("Skill" + (_cs + 1).ToString ())) ContPlayer.I.use_skill ("Skill" + (_cs + 1).ToString ());
        }
        
        for (int _cs = 0; _cs < GameConstants.MAX_NUM_OF_CHARS; _cs++) {
            if (Input.GetButtonDown ("CharSwitch" + (_cs + 1).ToString ())) ContPlayer.I.change_char (_cs);
        }
        
        if (Input.GetButtonDown ("Inventory")) GameUI_Inv.I.show ("check");

        // if (Input.GetButtonDown ("Items1"))
    }

    public void mouse_input (){
        mouseClicked = Input.GetMouseButtonDown (0);

        if (mouseClicked) {
            ContPlayer.I.use_skill ("mouse1");
        }
    }
}
