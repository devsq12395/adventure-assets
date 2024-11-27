using System.Collections;
using UnityEngine;

public class AI_LucaTheTerror : InGameAI {

    private float channelingTime; // Duration for channeling effect
    private float effectInterval; // Interval between effect spawns
    private float moveSpeed; // Speed for constant movement

    private bool isChanneling = false;
    private ObjGlitter objGlitterComponent;
    private int shootCount;

    private Vector3 randomMoveTarget;
    private float randomSkillTimer, randomSkillInterval, shootInterval;

    private bool isMovingToRandomPoint = false;

    // Channeling effect control
    private float channelingElapsedTime = 0f;
    private float nextEffectTime = 0f; // Time when the next effect will occur

    private void Start() {
        // Initialize the ObjGlitter component
        objGlitterComponent = gameObject.GetComponent<ObjGlitter>();
        randomSkillInterval = 2f;
    }

    // Function to initialize constants
    public override void on_start() {
        channelingTime = 2f; // Duration for channeling effect
        effectInterval = 0.25f; // Interval between effect spawns
        moveSpeed = 2f; // Speed for constant movement
    }

    public override void on_update() {
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        ContObj.I.face_player(inGameObj);

        // Handle channeling without coroutine
        if (isChanneling) {
            HandleChanneling();
            return;
        }

        if (isMovingToRandomPoint) {
            MoveToRandomPoint();
        } else {
            SetRandomMoveTarget();
        }

        if (shootCount > 0) {
            shootInterval += Time.deltaTime;
            if (shootInterval >= 0.5f) {
                ContObj.I.use_skill_active(inGameObj, "attack");
                shootCount--;
                shootInterval = 0;
                randomSkillTimer = 0;
            }
        }

        randomSkillTimer += Time.deltaTime;
        if (randomSkillTimer >= randomSkillInterval) {
            int _randomMove = Random.Range (0, 4);

            switch (_randomMove) {
                case 0:     
                    ContObj.I.use_skill_active(inGameObj, "attack");
                    shootCount = 7;
                    break;
                case 1: 
                    ContObj.I.use_skill_active(inGameObj, "luca-teleport");
                    break;
                case 2:
                    ContObj.I.use_skill_active(inGameObj, "charge");
                    break;
                default:
                    StartChanneling();
                    break;
            }

            randomSkillTimer = 0;
        }
    }

    // Function to handle the channeling process
    private void HandleChanneling() {
        channelingElapsedTime += Time.deltaTime;

        // Spawn effects at intervals
        if (channelingElapsedTime >= nextEffectTime) {
            ContEffect.I.create_effect("explosion2", gameObject.transform.position);
            nextEffectTime += effectInterval; // Set time for the next effect
        }

        // Check if channeling is done
        if (channelingElapsedTime >= channelingTime) {
            PerformPostChannelingMove();
            isChanneling = false;
            channelingElapsedTime = 0f; // Reset channeling time
            nextEffectTime = 0f; // Reset effect interval timer
        }
    }

    // Function to initiate channeling
    private void StartChanneling() {
        isChanneling = true;
        channelingElapsedTime = 0f;
        nextEffectTime = 0f; // Start the effect immediately

        // Enable ObjGlitter component
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = true;
        }
    }

    // Function to perform a move after channeling is done
    private void PerformPostChannelingMove() {
        if (Random.Range (0, 100) <= 50) {
            ContObj.I.use_skill_active(inGameObj, "luca-flame-wave");
        } else {
            ContObj.I.use_skill_active(inGameObj, "luca-spread");
        }

        // Disable ObjGlitter component
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = false;
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
        }
    }
}
