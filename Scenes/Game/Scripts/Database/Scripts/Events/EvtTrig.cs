using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvtTrig : MonoBehaviour {

    public string evtTrigger = "death";
    // Trigger lists:
    // - spawn - triggers on owner object's spawn from ContObj.cs
    // - death - triggers on owner object's death
    // - update - on every frame
    public string evtName;

    public virtual void setup (){
        
    }

    public virtual void use (){
        
    }
}
