using UnityEngine;

public class AI_HobGoblin : InGameAI {

    // Public variables for customization
    public float chargeInterval = 20f;
    public float randomSkillInterval = 5f;
    public float moveSpeed = 2f; // Slow movement speed
    private float chargeTimer = 0f;
    private float randomSkillTimer = 0f;

    public override void on_start() {
        // Initialize timers
        chargeTimer = 0f;
        randomSkillTimer = 0f;
    }

    public override void on_update() {
        InGameObject player = ContPlayer.I.player;
        Vector2 playerPos = player.gameObject.transform.position;

        // Update timers
        chargeTimer += Time.deltaTime;
        randomSkillTimer += Time.deltaTime;

        // Move slowly towards the player
        move_towards_player(playerPos);

        // Check if it's time to use the hobgoblin charge skill
        if (chargeTimer >= chargeInterval) {
            ContObj.I.use_skill_active(inGameObj, "hobgoblin-charge");
            chargeTimer = 0f; // Reset charge timer
        }

        // Check if it's time to use a random skill
        if (randomSkillTimer >= randomSkillInterval) {
            use_random_skill(); // Use a random skill (you can input the skills yourself)
            randomSkillTimer = 0f; // Reset random skill timer
        }
    }

    // Moves the hobgoblin slowly towards the player
    private void move_towards_player(Vector2 playerPos) {
        Vector2 currentPos = gameObject.transform.position;

        // Calculate direction towards the player
        Vector2 directionToPlayer = (playerPos - currentPos).normalized;

        // Move towards the player slowly
        Vector2 newPos = currentPos + directionToPlayer * moveSpeed * Time.deltaTime;
        ContObj.I.move_walk_to_pos(inGameObj, newPos);

        // Change facing direction based on movement
        ContObj.I.change_facing(inGameObj, currentPos.x > playerPos.x ? "left" : "right");
    }

    // Helper method to use a random skill (you can input your random skills here)
    private void use_random_skill() {
        // Example: Randomly select and use a skill (you can modify this as needed)
        int randomSkill = Random.Range(0, 3); // Assuming you have 3 random skills
        switch (randomSkill) {
            case 0:
                ContObj.I.use_skill_active(inGameObj, "axe-barrage");
                break;
            case 1:
                ContObj.I.use_skill_active(inGameObj, "bladestorm");
                break;
            case 2:
                ContObj.I.use_skill_active(inGameObj, "axe-boomerang");
                break;
            case 3:
                ContObj.I.use_skill_active(inGameObj, "orbiting-axes");
                    // Spiral first, then after a set distance, remove Spiral then add Orbit
                break;
        }
    }
}
