using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GiantSlime : InGameAI {
    
    public override void on_update (){
        stateTime += Time.deltaTime;

        if (state == 0) {
            if (stateTime >= 2f) {
                InGameObject _p = ContPlayer.I.player;
                Vector2 _pPos = _p.gameObject.transform.position;

                ContObj.I.move_walk_to_pos (inGameObj, _pPos);
                ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > _pPos.x) ? "left" : "right"));

                stateTime = 0;
                state = 1;
            }
        } else if (state == 1) {
            InGameObject _p = ContPlayer.I.player;
            Vector2 _pPos = _p.gameObject.transform.position;
            
            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, _pPos) <= 7f || 
                    stateTime >= 1f) {
                
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                
                stateTime = 0;
                state = 2;
            }
        } else if (state <= 7) {
            if (stateTime >= 0.4f) {
                ContObj.I.use_skill_active (inGameObj, "giant-slime-shotgun");

                stateTime = 0;
                state++;
            }
        } else if (state == 8) {
             ContObj.I.use_skill_active (inGameObj, "giant-slime-dash");
                
            stateTime = 0;
            state = 0;
        }
    }
}
