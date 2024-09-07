using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig : MonoBehaviour {

    public string scriptTag = "collider";
    public List<int> hitIDs;
    
    void Start (){
        hitIDs = new List<int>();
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
