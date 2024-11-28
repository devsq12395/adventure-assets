using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_Collect_Gold : ColTrig {

    public int amount;

    public override void on_hit_collectible (InGameObject _hit){
        if (_hit != ContPlayer.I.player) return;

        Destroy (gameObject);
    }
}
