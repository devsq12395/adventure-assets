using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EmbracedMage : InGameAI {

    private Vector2 randPoint;
    
    public override void on_update (){
        float rotationSpeed = 75f;

        stateTime += Time.deltaTime;

        if (state == 0) {
            if (stateTime >= 2f) {
                InGameObject _p = ContPlayer.I.player;
                Vector2 _pPos = _p.gameObject.transform.position,
                        _gPos = gameObject.transform.position;

                float _randAng = (gameObject.transform.position.x < _pPos.x) ? 
                    Random.Range (90, 270) :
                    Random.Range (270, 450);
                randPoint = Calculator.I.get_next_point_in_direction (gameObject.transform.position, _randAng, 8f);
                ContObj.I.move_walk_to_pos (inGameObj, randPoint);
                ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > randPoint.x) ? "left" : "right"));

                stateTime = 0;
                state = 1;
            }
        } else if (state == 1) {
            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, randPoint) <= 0.5f || 
                    stateTime >= 1.5f) {
                
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                ContObj.I.use_skill_active (inGameObj, "attack");
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
