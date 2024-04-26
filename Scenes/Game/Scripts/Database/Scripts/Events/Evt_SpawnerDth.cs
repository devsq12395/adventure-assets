using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_SpawnerDth : EvtTrig {

    public string toSpawn;
    public int owner;

    private bool hasSpawned;
    
    public override void use (){
        if (hasSpawned) return;

        ContObj.I.create_obj (toSpawn, gameObject.transform.position, owner);
        hasSpawned = true;
    }
}
