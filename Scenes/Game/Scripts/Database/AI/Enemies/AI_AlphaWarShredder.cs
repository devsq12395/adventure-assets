using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_AlphaWarShredder : InGameAI {
    
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
                    stateTime >= 3f) {
                
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                
                stateTime = 0;
                state = 2;
            }
        } else if (state <= 7) {
            if (stateTime >= 0.4f) {
                ContObj.I.use_skill_active (inGameObj, "war-shredder-shotgun");

                stateTime = 0;
                state++;
            }
        } else if (state <= 14) {
            if (stateTime >= 0.25f) {
                ContObj.I.use_skill_active (inGameObj, "attack");

                stateTime = 0;
                state++;
            }
        }  else if (state <= 15) {
            if (stateTime >= 0.25f) {
                ContObj.I.use_skill_active (inGameObj, "war-shredder-dash");

                stateTime = 0;
                state = 16;
            }
        } else if (state == 16) {
             if (stateTime >= 4f) {
                stateTime = 0;
                state = 0;
            }
        }
    }
}
