using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CaptainBeatrice : InGameAI {

    private int moveCount = 0;
    private int currentMove = 0;
    private int dashSlashCount = 0;  // To keep track of the number of slashes in DashAndSlash
    private bool isDashing = false;  // To handle dash movement without coroutine
    private string dashDirection = "";
    private float dashSpeed = 10f;  // Adjust as needed
    private float dashDuration = 0.5f;  // Duration for each dash segment
    private Vector3 dashTarget;

    public override void on_update() {
        ContObj.I.face_player(inGameObj);
        
        stateTime += Time.deltaTime;

        // Handle dashing movement if the boss is in the dashing state
        if (isDashing) {
            HandleDash();
            return;  // Skip the rest of the logic during dashing
        }

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
                StartDash("right", topLeft.x, bottomRight.x);
                stateTime = 0;
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
                StartDash("left", bottomRight.x, topLeft.x);
                stateTime = 0;
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
                stateTime = 0; // Reset stateTime after finishing move
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
            stateTime = 0; // Reset stateTime after finishing move
        }
    }

    private void SetTeleportTarget(Vector2 target) {
        Skill_TeleportBeatrice skill = gameObject.GetComponent<Skill_TeleportBeatrice>();
        if (skill != null) {
            skill.targetPoint = target;
        }
    }

    // Refactored Dash and Slash logic without Coroutine
    private void StartDash(string direction, float minX, float maxX) {
        dashDirection = direction;
        dashTarget = (direction == "left") ? Vector3.left : Vector3.right;
        isDashing = true;
        dashSlashCount = 0;  // Start fresh
    }

    private void HandleDash() {
        if (dashSlashCount < 4) {
            // Perform dash and slash
            ContObj.I.use_skill_active(inGameObj, "captain-beatrice-slash");

            float elapsedTime = 0f;
            while (elapsedTime < dashDuration) {
                Vector3 newPosition = gameObject.transform.position + dashTarget * dashSpeed * Time.deltaTime;
                if (newPosition.x < minX || newPosition.x > maxX) {
                    break; // Stop if 3 units before the edge is reached
                }
                gameObject.transform.position = newPosition;
                elapsedTime += Time.deltaTime;
            }

            dashSlashCount++;
            stateTime = 0; // Reset stateTime after each dash and slash
        } else {
            isDashing = false;  // Finish dashing
            moveCount++;
        }
    }
}
