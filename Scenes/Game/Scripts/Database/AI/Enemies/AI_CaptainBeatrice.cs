using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_CaptainBeatrice : InGameAI {

    private int moveCount = 0;
    private int currentMove = 0;
    private int dashSlashCount = 0;
    private bool isDashSlashing = false;
    private string dashDirection = "";
    private float dashSpeed;
    private Vector3 dashTarget;

    // Random movement variables
    private Vector3 randomMoveTarget;
    private float moveSpeed = 5f;
    private bool isMovingToRandomPoint = false;

    private float randomSkillTimer, randomSkillInterval;
    private int dashSlashStartCount = 0;

    public Afterimage afterimage;

    public override void on_start () {
        randomSkillInterval = 1f;
        dashSpeed = 20f;
    }

    public override void on_update() {
        stateTime += Time.deltaTime;

        if (isDashSlashing) {
            HandleDash();
            return;
        }

        InGameObject player = ContPlayer.I.player;
        Vector2 playerPos = player.gameObject.transform.position;
        ContObj.I.face_player(inGameObj);

        if (isMovingToRandomPoint) {
            MoveToRandomPoint();
        } else {
            SetRandomMoveTarget();
        }

        if (state > 0) return;

        randomSkillTimer += Time.deltaTime;
        if (randomSkillTimer >= randomSkillInterval) {
            if (dashSlashStartCount < 5) {
                int _randomMove = Random.Range (0, 2);

                switch (_randomMove) {
                    case 0:     PerformMove1(); break;
                    default:    PerformMove3(); break;
                }

                randomSkillTimer = 0;
                dashSlashStartCount++;
            } else {
                PerformMove2 ();
                dashSlashStartCount = 0;
            }
        }
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

    private void PerformMove2() { Debug.Log ("move 2");
        Vector2 mapSize = ContMap.I.details.size;
        StartDash("right", mapSize.x - 3, 3);
        afterimage = gameObject.AddComponent<Afterimage>();
    }

    private void PerformMove3() {
        if (moveCount < 3) {
            if (stateTime >= 1.5f) {
                Vector2 randomPoint = new Vector2(Random.Range(3, ContMap.I.details.size.x - 3), Random.Range(3, ContMap.I.details.size.y - 3));
                SetTeleportTarget(randomPoint);
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-teleport");
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-slash");
                stateTime = 0;
                moveCount++;
            }
        } else {
            state = 0;
            moveCount = 0;
            stateTime = 0;
        }
    }

    private void SetTeleportTarget(Vector2 target) {
        Skill_TeleportBeatrice skill = gameObject.GetComponent<Skill_TeleportBeatrice>();
        if (skill != null) {
            skill.targetPoint = target;
        }
    }

    private void StartDash(string direction, float minX, float maxX) {
        dashDirection = direction;
        dashTarget = (direction == "left") ? Vector3.left : Vector3.right;
        isDashSlashing = true;
        dashSlashCount = 0;
    }

    private void HandleDash() {
        if (dashSlashCount < 2) {
            Vector2 mapSize = ContMap.I.details.size;
            float minX = -mapSize.x + 3;
            float maxX = mapSize.x - 3;

            Vector3 newPosition = gameObject.transform.position + dashTarget * dashSpeed * Time.deltaTime;
            if ((dashDirection == "left" && newPosition.x < minX) || (dashDirection == "right" && newPosition.x > maxX)) {
                ContObj.I.use_skill_active(inGameObj, "captain-beatrice-slash");
                dashDirection = (dashDirection == "left") ? "right" : "left";
                dashTarget = (dashDirection == "left") ? Vector3.left : Vector3.right;
                dashSlashCount++;
            }
            gameObject.transform.position = newPosition;

        } else {
            isDashSlashing = false;  // Finish dashing
            Destroy (afterimage);
            moveCount++;
        }
    }

    private void SetRandomMoveTarget() {
        Vector2 mapSize = ContMap.I.details.size;
        randomMoveTarget = new Vector3(Random.Range(3, mapSize.x - 3), Random.Range(3, mapSize.y - 3), 0);
        isMovingToRandomPoint = true;
    }

    private void MoveToRandomPoint() {
        Vector3 direction = (randomMoveTarget - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed;
        ContObj.I.move_walk_to_pos(inGameObj, newPos);

        if (Vector3.Distance(transform.position, randomMoveTarget) < 2f) {
            isMovingToRandomPoint = false;
            stateTime = 0;
        }
    }
}
