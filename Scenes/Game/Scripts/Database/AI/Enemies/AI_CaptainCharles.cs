using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CaptainCharles : InGameAI {

    private Vector2 randPoint = Vector2.zero;
    
    public override void on_update (){
        stateTime += Time.deltaTime;

        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position,
                _gPos = gameObject.transform.position;

        if (randPoint == Vector2.zero) {
            float _randAng = (gameObject.transform.position.x < _pPos.x) ? 
                Random.Range (90, 270) :
                Random.Range (270, 450);
            randPoint = Calculator.I.get_next_point_in_direction (gameObject.transform.position, _randAng, 8f);
            ContObj.I.move_walk_to_pos (inGameObj, randPoint);
            ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > randPoint.x) ? "left" : "right"));

            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, randPoint) <= 0.5f) {
                randPoint = Vector2.zero;
            }
        }

        if (state <= 6) {
            if (stateTime >= 0.25f) {
                ContObj.I.use_skill_active (inGameObj, "attack");

                stateTime = 0;
                state++;
            }
        } else if (state == 7) {
            if (stateTime >= 4f) {
                stateTime = 0;
                state++;
            }
        } else if (state <= 10) {
            if (stateTime >= 0.25f) {
                ContObj.I.use_skill_active (inGameObj, "charles-flame-wave");

                stateTime = 0;
                state++;
            }
        } else if (state == 11) {
            if (stateTime >= 4f) {
                stateTime = 0;
                state = 0;
            }
        }
    }
}
