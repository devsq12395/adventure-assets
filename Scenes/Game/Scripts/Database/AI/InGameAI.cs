using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAI : MonoBehaviour {

    public InGameObject inGameObj;
    public bool isStart, isReady;

    public int state = 0;
    public float stateTime = 0f;

    public Vector2 goPos;

    public virtual void on_start (){
        inGameObj = gameObject.GetComponent <InGameObject> ();

        Debug.Log ("asd");
    }

    public virtual void on_ready (){
        
    }

    void Update (){
        if (!isStart) {
            on_start ();
            isStart = true;
        }
        if (!isReady) {
            on_ready ();
            isReady = true;
        }

        on_update ();
    }

    public virtual void on_update (){

    }
}
