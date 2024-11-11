using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_BarrelDth : EvtTrig
{
    private bool hasSpawned = false;
    private List<string> spawnableItems = new List<string> { "coin", "coin-pack" };

    public override void use()
    {
        if (hasSpawned) return;

        string toSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
        int owner = 1;
        ContObj.I.create_obj(toSpawn, gameObject.transform.position, owner);

        hasSpawned = true;
    }
}
