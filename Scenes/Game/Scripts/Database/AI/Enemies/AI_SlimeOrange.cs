using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SlimeOrange : InGameAI {


    private Vector2 randPoint;
    
    public override void on_update (){
        stateTime += Time.deltaTime;

        if (state == 0) {
            if (stateTime >= 2f) {
                float _randAng = Random.Range (0, 360);
                randPoint = Calculator.I.get_next_point_in_direction (gameObject.transform.position, _randAng, 8f);
                ContObj.I.move_walk_to_pos (inGameObj, randPoint);
                ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > randPoint.x) ? "left" : "right"));

                stateTime = 0;
                state = 1;
            }
        } else if (state == 1) {
            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, randPoint) <= 0.5f || 
                    stateTime >= 1f) {
                
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                stateTime = 0;
                state = 2;
            }
        } else if (state == 2) {
            if (stateTime >= 1f) {
                state = 0;
            }
        }
    }
}
