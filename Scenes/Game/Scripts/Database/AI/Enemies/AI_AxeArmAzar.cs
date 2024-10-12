using UnityEngine;

public class AI_AxeArmAzar : InGameAI {

    // Public variables for customization
    public float randomSkillInterval;
    public float moveSpeed; // Slow movement speed
    public float attackInterval; // Interval between attacks
    public float chargeInterval; // Interval between charges
    public int chargeSequenceCount; // Number of times to charge in a sequence

    private float randomSkillTimer = 0f;

    private bool isUsingAttackSkill = false;
    private bool isUsingChargeSkill = false; // Track if in charge sequence

    private int attackCount = 0; // Track how many times "attack" has been used
    private int chargeCount = 0; // Track how many times "charge" has been used

    private float attackTimer = 0f; // Timer to manage attack intervals
    private float chargeTimer = 0f; // Timer to manage charge intervals

    public override void on_start() {
        // Initialize timers
        randomSkillTimer = 0f;
        randomSkillInterval = 1.5f;
        moveSpeed = 2;
        attackInterval = 0.75f;
        chargeInterval = 2f;
        chargeSequenceCount = 2;
        isUsingAttackSkill = false;
        isUsingChargeSkill = false; // Initialize as not using charge sequence
        attackCount = 0;
        chargeCount = 0;
        attackTimer = 0f;
        chargeTimer = 0f;
    }

    public override void on_update() {
        InGameObject player = ContPlayer.I.player;
        Vector2 playerPos = player.gameObject.transform.position;

        // Update timers
        randomSkillTimer += Time.deltaTime;

        // Move slowly towards the player
        move_towards_player(playerPos);

        // Check if using attack skill in sequence
        if (isUsingAttackSkill) {
            handle_attack_sequence();
        } 
        // Check if using charge skill in sequence
        else if (isUsingChargeSkill) {
            handle_charge_sequence();
        }
        // Check if it's time to use a random skill
        else if (randomSkillTimer >= randomSkillInterval) {
            use_random_skill(); // Use a random skill (attack sequence or charge)
            randomSkillTimer = 0f; // Reset random skill timer
        }
    }

    // Moves the hobgoblin slowly towards the player
    private void move_towards_player(Vector2 playerPos) {
        Vector2 currentPos = gameObject.transform.position;

        // Calculate direction towards the player
        Vector2 directionToPlayer = (playerPos - currentPos).normalized;

        // Move towards the player slowly
        Vector2 newPos = currentPos + directionToPlayer * moveSpeed;
        ContObj.I.move_walk_to_pos(inGameObj, newPos);

        // Change facing direction based on movement
        ContObj.I.change_facing(inGameObj, currentPos.x > playerPos.x ? "left" : "right");
    }

    // Helper method to use a random skill (modified with attack and charge sequences)
    private void use_random_skill() {
        int randomSkill = Random.Range(0, 2); // Now we have 2 skills: attack sequence or charge sequence
        if (randomSkill == 0) {
            start_attack_sequence(); // Start attack sequence skill
        } else if (randomSkill == 1) {
            start_charge_sequence(); // Start charge sequence skill
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

    // Start the charge sequence skill
    private void start_charge_sequence() {
        isUsingChargeSkill = true;
        chargeCount = 0; // Reset the charge count
        chargeTimer = 0f; // Reset the charge interval timer
    }

    // Handle the charge sequence, performing charges at intervals
    private void handle_charge_sequence() {
        chargeTimer += Time.deltaTime;

        // Perform a charge every chargeInterval seconds
        if (chargeTimer >= chargeInterval) {
            ContObj.I.use_skill_active(inGameObj, "charge");
            chargeCount++;
            chargeTimer = 0f; // Reset the timer for the next charge

            // End the sequence after the specified number of charges
            if (chargeCount >= chargeSequenceCount) {
                isUsingChargeSkill = false;
            }
        }
    }
}
