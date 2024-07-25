using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CaptainBeatrice : InGameAI {

    private int moveCount = 0;
    private int currentMove = 0;

    public override void on_update() {
        ContObj.I.face_player(inGameObj);
        
        stateTime += Time.deltaTime;

        if (state == 0) {
            if (stateTime >= 5f) {
                PerformNextMove();
                stateTime = 0;
            }
        } else if (state == 1) {
            PerformMove1();
        } else if (state == 2) {
            PerformMove2();
        } else if (state == 3) {
            PerformMove3();
        }
    }

    private void PerformNextMove() {
        moveCount = 0;
        currentMove = (currentMove % 3) + 1; // Cycle through moves 1, 2, and 3
        state = currentMove;
        stateTime = 0;
    }

    private void PerformMove1() {
        if (moveCount < 5) {
            if (stateTime >= 0.5f) {
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-slash");
                moveCount++;
                stateTime = 0;
            }
        } else {
            moveCount = 0;
            state = 0;
        }
    }

    private void PerformMove2() {
        Vector2 mapSize = ContMap.I.details.size;
        Vector2 topLeft = new Vector2(3, mapSize.y - 3); // 3 units before the edge
        Vector2 bottomRight = new Vector2(mapSize.x - 3, 3); // 3 units before the edge

        if (moveCount == 0) {
            SetTeleportTarget(new Vector2(3, Random.Range(3, mapSize.y - 3))); // Random y position
            ContObj.I.use_skill_active(inGameObj, "captain-beatrice-teleport");
            stateTime = 0;
            moveCount++;
        } else if (moveCount == 1) {
            if (stateTime >= 1f) {
                StartCoroutine(DashAndSlash("right", 4, topLeft.x, bottomRight.x));
                moveCount++;
            }
        } else if (moveCount == 2) {
            if (stateTime >= 2f) {
                SetTeleportTarget(new Vector2(mapSize.x - 3, Random.Range(3, mapSize.y - 3))); // Random y position
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-teleport");
                stateTime = 0;
                moveCount++;
            }
        } else if (moveCount == 3) {
            if (stateTime >= 1f) {
                StartCoroutine(DashAndSlash("left", 4, bottomRight.x, topLeft.x));
                moveCount++;
            }
        } else if (moveCount == 4) {
            if (stateTime >= 2f) {
                Vector2 randomPoint = new Vector2(
                    Random.Range(3, mapSize.x - 3),
                    Random.Range(3, mapSize.y - 3)
                );
                SetTeleportTarget(randomPoint);
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-teleport");
                state = 0;
                moveCount = 0;
            }
        }
    }

    private void PerformMove3() {
        Vector2 mapSize = ContMap.I.details.size;

        if (moveCount < 3) {
            if (stateTime >= 1.5f) {
                Vector2 randomPoint = new Vector2(
                    Random.Range(3, mapSize.x - 3),
                    Random.Range(3, mapSize.y - 3)
                );
                SetTeleportTarget(randomPoint);
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-teleport");

                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-slash");
                stateTime = 0;
                moveCount++;
            }
        } else {
            state = 0;
            moveCount = 0;
        }
    }

    private void SetTeleportTarget(Vector2 target) {
        Skill_TeleportBeatrice skill = gameObject.GetComponent<Skill_TeleportBeatrice>();
        if (skill != null) {
            skill.targetPoint = target;
        }
    }

    private IEnumerator DashAndSlash(string direction, int slashes, float minX, float maxX) {
        InGameObject _ownerComp = gameObject.GetComponent<InGameObject>();
        Vector3 dashDirection = (direction == "left") ? Vector3.left : Vector3.right;
        float dashSpeed = 10f; // Adjust as needed
        float dashDuration = 0.5f; // Duration for each dash segment

        for (int i = 0; i < slashes; i++) {
            ContObj.I.use_skill_active(inGameObj, "captain-beatrice-slash");

            // Perform dash
            float elapsedTime = 0f;
            while (elapsedTime < dashDuration) {
                Vector3 newPosition = gameObject.transform.position + dashDirection * dashSpeed * Time.deltaTime;
                if (newPosition.x < minX || newPosition.x > maxX) {
                    break; // Stop if 3 units before the edge is reached
                }
                if (DB_Conditions.I.can_move (_ownerComp)) {
                    gameObject.transform.position = newPosition;
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // Ensure moveCount and stateTime are reset correctly after dash
        moveCount++;
        stateTime = 0;
    }
}
