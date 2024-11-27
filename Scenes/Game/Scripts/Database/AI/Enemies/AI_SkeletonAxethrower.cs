using UnityEngine;

public class AI_SkeletonAxethrower : InGameAI {

    private float SKILL_COOLDOWN;
    private float skillTimer = 0f;
    private Skill_AxeSkeleton axeSkill;

    public override void on_start() {
        SKILL_COOLDOWN = 1.5f;

        axeSkill = GetComponent<Skill_AxeSkeleton>();
        if (axeSkill == null) {
            Debug.LogError("Skill_AxeSkeleton component is missing.");
            enabled = false;
            return;
        }
    }

    public override void on_update() {
        if (axeSkill == null) return;

        // Update the skill cooldown timer
        skillTimer -= Time.deltaTime;

        // Get the player's position
        Vector2 playerPos = ContPlayer.I.player.gameObject.transform.position;
        Vector2 currentPos = transform.position;
        float distanceToPlayer = Vector2.Distance(currentPos, playerPos);

        if (distanceToPlayer > 18f) {
            // Move towards the player
            ContObj.I.move_walk_to_pos(inGameObj, playerPos);
            ContObj.I.change_facing(inGameObj, (currentPos.x > playerPos.x) ? "left" : "right");
        } else if (skillTimer <= 0f) {
            // Use the skill if within range and cooldown is over
            axeSkill.targetPoint = playerPos;
            axeSkill.use_active();
            skillTimer = SKILL_COOLDOWN; // Reset the cooldown timer
        } else {
            // Stand still while waiting for cooldown
            ContObj.I.change_velocity(inGameObj, Vector2.zero);
        }
    }
}
