using UnityEngine;

public class AI_FireSpirit : InGameAI {

    // Internal state variables
    private float actionTimer = 0f;
    private float skillInterval;

    public override void on_start() {
        actionTimer = 0f;
        skillInterval = 4f;
    }

    public override void on_update() {
        // If enough time has passed, use a random skill
        if (actionTimer >= skillInterval) {
            use_random_skill(); // Use a random skill from the list
            actionTimer = 0f; // Reset action timer
        }
    }

    // Helper method to use a random skill
    private void use_random_skill() {
        ContObj.I.use_skill_active(inGameObj, "random-fireball");
    }
}
