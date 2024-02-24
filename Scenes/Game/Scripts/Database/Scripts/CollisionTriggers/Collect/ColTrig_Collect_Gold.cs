using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_Collect_Gold : ColTrig {

    public int amount;
    public override void on_hit_enemy (InGameObject _hit){
        if (_hit != ContPlayer.I.player) return;

        ContPlayer.I.get_gold (amount);
        Destroy (gameObject);
    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
