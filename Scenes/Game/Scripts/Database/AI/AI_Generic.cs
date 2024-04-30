using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Generic : InGameAI {
    public override void on_ready (){
        ContObj.I.const_move_ang_set (inGameObj, 0f, inGameObj.speed);
    }
    public override void on_update (){
        GameObject _player = Game.I.get_player_obj ();
        float _ang = Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _player.transform.position);
        
        inGameObj.constMovAng_ang = _ang;

        ContObj.I.face_player (inGameObj);
    }
}
