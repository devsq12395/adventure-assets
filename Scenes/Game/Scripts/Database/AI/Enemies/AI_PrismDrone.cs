using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PrismDrone : InGameAI {


    private Vector2 randPoint;
    
    public override void on_update (){
        stateTime += Time.deltaTime;

        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position,
                _gPos = gameObject.transform.position;

        if (!randPoint) {
            float _randAng = (gameObject.transform.position.x < _pPos.x) ? 
                Random.Range (90, 270) :
                Random.Range (270, 450);
            randPoint = Calculator.I.get_next_point_in_direction (gameObject.transform.position, _randAng, 8f);
            ContObj.I.move_walk_to_pos (inGameObj, randPoint);

            if (Calculator.I.get_dist_from_2_points (gameObject.transform.position, randPoint) <= 0.5f) {
                randPoint = null;
            }
        }

        if (stateTime >= 1f) {
            ContObj.I.use_skill_active (inGameObj, "attack");
            stateTime = 0;
        }
    }
}
