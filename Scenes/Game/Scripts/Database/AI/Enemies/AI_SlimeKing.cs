using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SlimeKing : InGameAI {

    private float stateTime_slime = 0;
    
    public override void on_update (){
        stateTime += Time.deltaTime;
        stateTime_slime += Time.deltaTime;

        // Slime Spawn
        if (stateTime_slime >= 4) {
            stateTime_slime = 0;
            ContObj.I.create_obj ("slime-blue", gameObject.transform.position, 2);
        }

        // Main
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
                    stateTime >= 1f) {
                
                ContObj.I.move_walk_to_pos_stop (inGameObj);
                
                stateTime = 0;
                state = 2;
            }
        } else if (state == 2) {
            int _randMove = Random.Range (0, 1);

            switch (_randMove) {
                case 0:
                    ContObj.I.move_walk_to_pos_stop (inGameObj);
                    ContObj.I.use_skill_active (inGameObj, "slime-king-spread");
                    break;
                case 1:
                    ContObj.I.move_walk_to_pos_stop (inGameObj);
                    ContObj.I.use_skill_active (inGameObj, "slime-king-flame-wave");
                    break;
            }
        } 
    }
}
