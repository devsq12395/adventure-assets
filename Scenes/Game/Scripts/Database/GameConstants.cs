using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour {

    public static GameConstants I;
	public void Awake(){ I = this; }

    /*
        REMEMBER = THIS IS SUPPOSED TO BE SET AT UNITY EDITOR

        Use as GameConstants.YOUR_CONST, not GameConstants.I.YOUR_CONST
    */

    public const float COLL_DUR = 0.75f;
    public const float RENDER_DIST = 25f;


    public const int INVENTORY_SLOTS = 5;
    public const int MAX_NUM_OF_CHARS = 4;
    public const int MAX_NUM_OF_SKILLS = 3;
}
