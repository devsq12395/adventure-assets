using System.Collections;
using UnityEngine;

public class AI_LucaTheTerror : InGameAI {

    private float channelingTime; // Duration for channeling effect
    private float effectInterval; // Interval between effect spawns
    private float moveSpeed; // Speed for constant movement
    private float chargeCooldown; // Cooldown time for the charge ability

    private bool isChanneling = false;
    private bool isCharging = false; // Indicates if Luca is currently charging
    private float chargeCooldownTimer = 0f; // Tracks cooldown for the charge ability
    private ObjGlitter objGlitterComponent;

    private void Start() {
        // Initialize the ObjGlitter component
        objGlitterComponent = gameObject.GetComponent<ObjGlitter>();
    }

    // Function to initialize constants
    public override void on_start() {
        channelingTime = 2f; // Duration for channeling effect
        effectInterval = 0.25f; // Interval between effect spawns
        moveSpeed = 2f; // Speed for constant movement
        chargeCooldown = 10f; // Cooldown for charge ability
    }

    public override void on_update() {
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        // Update charge cooldown timer
        if (chargeCooldownTimer > 0) {
            chargeCooldownTimer -= Time.deltaTime;
        }

        ContObj.I.face_player(inGameObj);

        stateTime += Time.deltaTime;
        if (isChanneling || isCharging) return;

        // Constant movement towards the player
        move_towards_player(_pPos);

        if (state == 0) {
            if (stateTime >= 3.5f) {
                stateTime = 0;
                state = 1;
            }
        } else if (state == 1) {
            if (Calculator.I.get_dist_from_2_points(gameObject.transform.position, _pPos) <= 7f || 
                stateTime >= 1f) {
                ContObj.I.move_walk_to_pos_stop(inGameObj);

                stateTime = 0;
                state = 2;
            }
        } else if (state <= 7) {
            if (stateTime >= 0.25f) {
                ContObj.I.use_skill_active(inGameObj, "attack");

                stateTime = 0;
                state++;
            }
        } else if (state == 8) {
            int _randMove = Random.Range(0, 4); // Adding charge as a new random move (0, 1, 2, 3)

            ContObj.I.move_walk_to_pos_stop(inGameObj);
            if (_randMove == 0 || _randMove == 1) {
                isChanneling = true;
                StartCoroutine(ChannelingEffect(_randMove));
            } else if (_randMove == 2) {
                ContObj.I.use_skill_active(inGameObj, "luca-teleport");
                state = 9;
            } else if (_randMove == 3 && chargeCooldownTimer <= 0) {
                use_charge(); // Trigger the charge move
            }
        } else if (state == 9) {
            ContObj.I.move_walk_to_pos(inGameObj, _pPos);
            ContObj.I.change_facing(inGameObj, ((gameObject.transform.position.x > _pPos.x) ? "left" : "right"));

            stateTime = 0;
            state = 0;
        }
    }

    // Method for constant movement towards the player
    private void move_towards_player(Vector2 playerPos) {
        if (!isChanneling && !isCharging) {
            Vector2 currentPos = gameObject.transform.position;

            // Calculate direction towards the player
            Vector2 directionToPlayer = (playerPos - currentPos).normalized;

            // Move towards the player slowly
            Vector2 newPos = currentPos + directionToPlayer * moveSpeed;
            ContObj.I.move_walk_to_pos(inGameObj, newPos);

            // Change facing direction based on movement
            ContObj.I.change_facing(inGameObj, currentPos.x > playerPos.x ? "left" : "right");
        }
    }

    // Coroutine to handle the channeling effect
    private IEnumerator ChannelingEffect(int move) {
        // Enable ObjGlitter component
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = true;
        }

        float elapsedTime = 0f;

        while (elapsedTime < channelingTime) {
            // Create effect every effectInterval
            ContEffect.I.create_effect("explosion2", gameObject.transform.position);

            yield return new WaitForSeconds(effectInterval);
            elapsedTime += effectInterval;
        }

        // Perform the skill after channeling
        if (move == 0) {
            ContObj.I.use_skill_active(inGameObj, "luca-flame-wave");
        } else if (move == 1) {
            ContObj.I.use_skill_active(inGameObj, "luca-spread");
        }

        // Disable ObjGlitter component
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = false;
        }

        isChanneling = false;
        state = 9;
    }

    // Method to trigger the "charge" move
    private void use_charge() {
        isCharging = true;
        ContObj.I.use_skill_active(inGameObj, "charge");
        chargeCooldownTimer = chargeCooldown; // Set the cooldown for the next charge

        // Move faster for the duration of the charge
        StartCoroutine(HandleChargeMovement());
    }

    // Coroutine to handle the charge movement duration
    private IEnumerator HandleChargeMovement() {
        float chargeDuration = 1.5f; // Duration of the charge movement
        float chargeSpeedMultiplier = 3f; // Speed multiplier during charge
        float elapsedTime = 0f;

        while (elapsedTime < chargeDuration) {
            InGameObject _p = ContPlayer.I.player;
            Vector2 _pPos = _p.gameObject.transform.position;
            Vector2 currentPos = gameObject.transform.position;
            Vector2 directionToPlayer = (currentPos - _pPos).normalized;

            // Move at increased speed during the charge
            Vector2 newPos = currentPos + directionToPlayer * moveSpeed * chargeSpeedMultiplier * Time.deltaTime;
            ContObj.I.move_walk_to_pos(inGameObj, newPos);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isCharging = false;
        state = 9; // Continue with normal movement or actions after charge
    }
}
