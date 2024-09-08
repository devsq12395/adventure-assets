using System.Collections;
using UnityEngine;

public class AI_MafiaCaptain : InGameAI {

    public float summonCooldown = 10f;  // Cooldown time for summoning reinforcements
    public float punchRange = 2f;  // Range for heavy punch attack
    public float shootInterval = 1.5f;  // Time between shots in the shooting phase
    public float summonDelay = 5f;  // Delay before summoning minions
    public int maxMinions = 3;  // Maximum number of minions that can be summoned

    private bool isSummoning = false;
    private int currentMinions = 0;
    private float lastSummonTime;
    private bool isShooting = false;
    private ObjGlitter objGlitterComponent;

    private void Start() {
        // Initialize glitter component for effects (if any)
        objGlitterComponent = gameObject.GetComponent<ObjGlitter>();
    }

    public override void on_update() {
        ContObj.I.face_player(inGameObj);

        stateTime += Time.deltaTime;

        if (isSummoning || isShooting) return;

        if (state == 0) {
            // Randomly choose between summoning, punching, or shooting
            if (stateTime >= 3f) {
                int randomAction = Random.Range(0, 3);

                if (randomAction == 0 && Time.time - lastSummonTime >= summonCooldown && currentMinions < maxMinions) {
                    StartCoroutine(SummonReinforcements());
                } else if (randomAction == 1) {
                    StartCoroutine(PunchAttack());
                } else {
                    StartCoroutine(ShootingPhase());
                }

                stateTime = 0;
                state = 1;
            }
        } else if (state == 1) {
            if (stateTime >= 2f) {
                state = 0; // Reset to decide next action
                stateTime = 0;
            }
        }
    }

    // Mafia Captain uses a heavy punch attack if the player is within range
    private IEnumerator PunchAttack() {
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        float distance = Vector2.Distance(gameObject.transform.position, _pPos);

        if (distance <= punchRange) {
            // Punch the player
            ContObj.I.move_walk_to_pos_stop(inGameObj);
            ContObj.I.use_skill_active(inGameObj, "mafia-punch");
        }

        yield return new WaitForSeconds(0.5f);

        state = 1;
    }

    // Mafia Captain starts shooting at the player from a distance
    private IEnumerator ShootingPhase() {
        isShooting = true;

        for (int i = 0; i < 3; i++) {
            ContObj.I.use_skill_active(inGameObj, "mafia-gun-shot");
            yield return new WaitForSeconds(shootInterval);
        }

        isShooting = false;
        state = 1;
    }

    // Mafia Captain summons minions to assist him in combat
    private IEnumerator SummonReinforcements() {
        isSummoning = true;

        // Enable glitter effect if available
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = true;
        }

        yield return new WaitForSeconds(summonDelay);

        for (int i = 0; i < maxMinions; i++) {
            ContObj.I.create_obj("mobster", gameObject.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0), 2);
            currentMinions++;
        }

        // Disable glitter effect after summoning
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = false;
        }

        lastSummonTime = Time.time;
        isSummoning = false;
        state = 1;
    }

    // Call this method when a minion is killed to decrease the count
    public void OnMinionKilled() {
        currentMinions--;
    }
}
