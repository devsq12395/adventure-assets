using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SampleAssist : InGameAI {


    private Vector2 randPoint;
    public override void on_start (){
        
    }

    public override void on_ready (){
        Vector2 _pos = ContPlayer.I.player.gameObject.transform.position;

        bool _isLeft = (ContPlayer.I.player.facing == "left");
        float[] _angs = {
            ((_isLeft) ? 180 : 0),
            ((_isLeft) ? 135 : 45),
            ((_isLeft) ? 225 : 325)
        };
        foreach (float _ang in _angs) {
            ContObj.I.create_missile ("SampleAssist_Missile", _pos, 1, _ang);
        }
    }
    public override void on_update (){
        
    }
}
