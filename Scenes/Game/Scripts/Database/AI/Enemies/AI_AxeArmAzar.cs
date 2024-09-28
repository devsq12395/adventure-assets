using UnityEngine;

public class AI_AxeArmAzar : InGameAI {

    // Public variables for customization
    public float chargeInterval = 20f;
    public float randomSkillInterval = 5f;
    public float moveSpeed = 2f; // Slow movement speed
    public float attackInterval = 0.75f; // Interval between attacks

    private float chargeTimer = 0f;
    private float randomSkillTimer = 0f;

    private bool isUsingAttackSkill = false;
    private int attackCount = 0; // Track how many times "attack" has been used
    private float attackTimer = 0f; // Timer to manage attack intervals

    public override void on_start() {
        // Initialize timers
        chargeTimer = 0f;
        randomSkillTimer = 0f;
        isUsingAttackSkill = false;
        attackCount = 0;
        attackTimer = 0f;
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
            ContObj.I.use_skill_active(inGameObj, "charge");
            chargeTimer = 0f; // Reset charge timer
        }

        // Check if using attack skill in sequence
        if (isUsingAttackSkill) {
            handle_attack_sequence();
        }
        // Check if it's time to use a random skill
        else if (randomSkillTimer >= randomSkillInterval) {
            use_random_skill(); // Use a random skill (with the new logic)
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

    // Helper method to use a random skill (modified with attack sequence)
    private void use_random_skill() {
        int randomSkill = Random.Range(0, 2); // Now we have 2 skills: attack sequence or shuriken
        if (randomSkill == 0) {
            start_attack_sequence(); // Start attack sequence skill
        } else if (randomSkill == 1) {
            ContObj.I.use_skill_active(inGameObj, "random-shuriken");
        }
    }

    // Start the attack sequence skill
    private void start_attack_sequence() {
        isUsingAttackSkill = true;
        attackCount = 0; // Reset the attack count
        attackTimer = 0f; // Reset the attack interval timer
    }

    // Handle the attack sequence, firing "attack" 3 times at intervals
    private void handle_attack_sequence() {
        attackTimer += Time.deltaTime;

        // Perform an attack every attackInterval seconds
        if (attackTimer >= attackInterval) {
            ContObj.I.use_skill_active(inGameObj, "attack");
            attackCount++;
            attackTimer = 0f; // Reset the timer for the next attack

            // End the sequence after 3 attacks
            if (attackCount >= 3) {
                isUsingAttackSkill = false;
            }
        }
    }
}
