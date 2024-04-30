using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PrismDrone : InGameAI {


    private Vector2 randPoint = Vector2.zero;
    public float moveInterval;
    
    public override void on_update (){
        stateTime += Time.deltaTime;

        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position,
                _gPos = gameObject.transform.position;

        if (moveInterval <= 0) {
            float _randAng = Random.Range (0, 360);
            randPoint = Calculator.I.get_next_point_in_direction (gameObject.transform.position, _randAng, 4f);
            ContObj.I.move_walk_to_pos (inGameObj, randPoint);
            moveInterval = 1;
        } else {
            moveInterval -= Time.deltaTime;

            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, randPoint) <= 0.5f) {
                moveInterval = 0;
            }
        }

        if (stateTime >= 3f) {
            ContObj.I.use_skill_active (inGameObj, "attack");
            stateTime = 0;
        }
    }
}
