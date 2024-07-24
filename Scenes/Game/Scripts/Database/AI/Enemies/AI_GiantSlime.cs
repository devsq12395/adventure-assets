using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_GiantSlime : InGameAI {

    private int moveStep = 0;
    private float moveInterval = 0f;

    public override void on_update() {
        ContObj.I.face_player(inGameObj);

        stateTime += Time.deltaTime;

        switch (state) {
            case 0:
                // Move closer to player for 1 second
                if (stateTime >= 1f) {
                    MoveTowardsPlayer();
                    stateTime = 0;
                    state = 1;
                }
                break;
            case 1:
                // Stop moving and prepare for attack
                ContObj.I.move_walk_to_pos_stop(inGameObj);
                PerformRandomMove();
                break;
            case 2:
                // Performing moves with intervals
                PerformMoveStep();
                break;
            case 3:
                // Pause for 1.5 seconds after performing moves
                if (stateTime >= 1.5f) {
                    stateTime = 0;
                    state = 0;
                }
                break;
        }
    }

    private void MoveTowardsPlayer() {
        InGameObject player = ContPlayer.I.player;
        Vector2 playerPos = player.gameObject.transform.position;

        ContObj.I.move_walk_to_pos(inGameObj, playerPos);
        ContObj.I.change_facing(inGameObj, (gameObject.transform.position.x > playerPos.x) ? "left" : "right");
    }

    private void PerformRandomMove() {
        int randomMove = Random.Range(0, 3); // Now including "giant-slime-dash"
        if (randomMove == 0) {
            moveInterval = 0.75f;
            moveStep = 3; // 3 shotguns
            UseSkill("giant-slime-shotgun");
        } else if (randomMove == 1) {
            moveInterval = 1f;
            moveStep = 2; // 2 flame waves
            UseSkill("giant-slime-flame-wave");
        } else {
            moveInterval = 0f;
            moveStep = 1; // 1 dash
            UseSkill("giant-slime-dash");
        }
        state = 2;
        stateTime = 0;
    }

    private void PerformMoveStep() {
        if (stateTime >= moveInterval) {
            if (moveStep > 1) {
                moveStep--;
                stateTime = 0;
                if (moveStep % 2 == 0) {
                    UseSkill("giant-slime-shotgun");
                } else {
                    UseSkill("giant-slime-flame-wave");
                }
            } else {
                moveStep = 0;
                stateTime = 0;
                state = 3;
            }
        }
    }

    private void UseSkill(string skillName) {
        ContObj.I.use_skill_active(inGameObj, skillName);
    }
}
