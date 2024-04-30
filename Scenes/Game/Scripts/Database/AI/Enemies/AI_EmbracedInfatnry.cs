using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EmbracedInfantry : InGameAI {
    
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
            
            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, _pPos) <= 4f || 
                    stateTime >= 2f) {
                
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                
                stateTime = 0;
                state = 2;
            }
        } else if (state <= 3) {
            if (stateTime >= 0.75f) {
                ContObj.I.use_skill_active (inGameObj, "attack");

                stateTime = 0;
                state++;
            }
        } else if (state == 4) {
            ContObj.I.use_skill_active (inGameObj, "embraced-infantry-dash");
                
            stateTime = 5;
            state = 0;
        } else if (state == 5) {
            if (stateTime >= 3f) {
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                    
                stateTime = 0;
                state = 0;
            }
        }
    }
}
