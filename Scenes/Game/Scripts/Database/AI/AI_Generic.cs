using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Generic : InGameAI {
    public override void on_ready (){
        ContObj.I.const_move_ang_set (inGameObj, 0f, inGameObj.speed);
    }
    public override void on_update (){
        GameObject _player = Game.I.get_player_obj ();
        
        ContObj.I.move_walk_to_pos (inGameObj, _player.transform.position);

        ContObj.I.face_player (inGameObj);
    }
}
