using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour {

    public static GameConstants I;
	public void Awake(){ I = this; }

    /*
        Can be set here
        Script is attached in SCRIPTS/_GLOBAL_SCRIPTS
    */

    public const float COLL_DUR = 0.75f;
    public const float RENDER_DIST = 25f;


    public const int INVENTORY_SLOTS = 5;
    public const int MAX_NUM_OF_CHARS = 4;
    public const int MAX_NUM_OF_SKILLS = 3;
}
