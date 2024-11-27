using UnityEngine;

public class AI_AssassinMob : InGameAI {

    // Public variables for customization
    public float shurikenThrowInterval;
    public int shurikenThrowCount;
    public float moveDuration;
    public float evadeDistance;

    // Internal state variables
    private int shurikenThrown;
    private bool isEvading = false, isWalkingNow = false;
    private float actionTimer = 0f;
    private Vector2 evadeDirection;

    public override void on_start() {
        // Initialize default values
        shurikenThrowInterval = 1.75f;
        shurikenThrowCount = 2;
        moveDuration = 1f;
        evadeDistance = 5f;
        
        // Reset internal variables
        shurikenThrown = 0;
        isEvading = false;
        actionTimer = 0f;
    }

    public override void on_update() {
        // Update the action timer
        actionTimer += Time.deltaTime;

        InGameObject player = ContPlayer.I.player;

        if (!isEvading) {
            // Throw shuriken at the player
            if (shurikenThrown < shurikenThrowCount) {
                if (actionTimer >= shurikenThrowInterval) {
                    ContObj.I.use_skill_active(inGameObj, "attack");
                    shurikenThrown++;
                    actionTimer = 0f;
                }
            } else {
                // After throwing, start evading
                isEvading = true;
                evadeDirection = get_evade_direction();
                actionTimer = 0f;
            }
        } else {
            // Evade in the calculated random direction for the set duration
            if (actionTimer < moveDuration) {
                if (!isWalkingNow) {
                    Vector2 evadePos = (Vector2)gameObject.transform.position + evadeDirection * evadeDistance;
                    ContObj.I.move_walk_to_pos(inGameObj, evadePos);
                    ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > evadePos.x) ? "left" : "right"));

                    isWalkingNow = true;
                }
            } else {
                // End evading and reset to throw shurikens again
                ContObj.I.move_walk_to_pos_stop(inGameObj);
                isEvading = false;
                isWalkingNow = false;
                shurikenThrown = 0;
                actionTimer = 0f;
            }
        }
    }

    // Helper method to determine a random direction for evading
    private Vector2 get_evade_direction() {
        // Generate a random angle for movement
        float angle = Random.Range(0f, 360f);
        Vector2 evadeDir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return evadeDir.normalized;
    }
}
