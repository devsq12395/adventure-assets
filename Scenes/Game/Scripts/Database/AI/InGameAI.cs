using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAI : MonoBehaviour {

    public InGameObject inGameObj;
    public bool isStart, isReady, isActivated;

    public int state = 0;
    public float stateTime = 0f;

    public Vector2 goPos;

    public virtual void on_start (){
        // Should be empty on this script
        // Ok to put codes when overriding from another script. Check AI_Assassin.cs.
    }

    public virtual void on_ready (){
        // The above statement applies here too
    }

    public virtual void on_update (){

    }

    void Update (){
        if (Game.I.isPaused) return;

        if (!isStart) {
            inGameObj = gameObject.GetComponent <InGameObject> ();

            on_start ();
            isStart = true;
        }
        if (!isReady) {
            on_ready ();
            isReady = true;
        }
        if (!isActivated) {
            check_if_player_nearby ();
            return;
        }

        on_update ();
    }

    public void check_if_player_nearby (){
        if (Calculator.I.get_dist_from_player (gameObject.transform.position) <= 17) {
            isActivated = true;
        }
    }
}
