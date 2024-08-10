using System.Collections;
using UnityEngine;

public class AI_Centurion : InGameAI {

    public float attackRange = 5f; // Range within which the Centurion attacks
    public float attackRate = 0.7f; // Attack rate in seconds

    private float attackCooldown = 0f;

    public override void on_update() {
        // Face the player
        ContObj.I.face_player(inGameObj);

        Vector2 playerPosition = ContPlayer.I.player.gameObject.transform.position;
        Vector2 centurionPosition = gameObject.transform.position;
        float distanceToPlayer = Calculator.I.get_dist_from_2_points(centurionPosition, playerPosition);

        
        if (distanceToPlayer <= attackRange) {
            // Attack while within attack range
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f) {
                ContObj.I.use_skill_active(inGameObj, "attack");
                attackCooldown = attackRate;
            }
        } else {
            ContObj.I.move_walk_to_pos(inGameObj, playerPosition);
        }
    }
}
