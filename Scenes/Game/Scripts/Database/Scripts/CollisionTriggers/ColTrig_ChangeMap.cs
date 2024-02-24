using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_ChangeMap : ColTrig {

    public string NEXT_MAP;
    public override void on_hit_enemy (InGameObject _hit){
        if (_hit != ContPlayer.I.player) return;

        ContMap.I.change_map (NEXT_MAP);
    }
}
