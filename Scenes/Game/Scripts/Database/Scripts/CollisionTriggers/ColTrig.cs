using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig : MonoBehaviour {

    public string scriptTag = "collider";
    
    void Setup (){
        on_setup ();
    }

    void Update (){

    }

    public virtual void on_setup () {
        
    }

    public virtual void on_hit_enemy (InGameObject _hit) {
        
    }

    public virtual void on_hit_ally (InGameObject _hit) {
        
    }
}
