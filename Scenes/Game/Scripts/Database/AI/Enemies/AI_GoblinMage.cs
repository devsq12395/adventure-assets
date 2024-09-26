using UnityEngine;

public class AI_GoblinMage : InGameAI {

    // Public variables for customization
    public float moveChangeInterval;

    // Internal state variables
    private float actionTimer = 0f;
    private float moveTimer = 0f;
    private Vector2 targetPos;

    public int moveNum;

    public override void on_start() {
        // Initialize default values
        moveChangeInterval = 2f; // Time between changing movement target
        actionTimer = 5f;
        moveTimer = 0f;
        moveNum = 1;

        // Set an initial random movement target
        targetPos = get_random_position();
        ContObj.I.move_walk_to_pos(inGameObj, targetPos);
        ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > targetPos.x) ? "left" : "right"));
    }

    public override void on_update() {
        // Update timers
        actionTimer -= Time.deltaTime;
        moveTimer += Time.deltaTime;

        // Check if it's time to change the movement target
        if (moveTimer >= moveChangeInterval) {
            // Check if the AI has reached the current target position
            if (Vector2.Distance((Vector2)gameObject.transform.position, targetPos) < 0.1f) {
                targetPos = get_random_position(); // Get a new random position within map bounds
                ContObj.I.move_walk_to_pos(inGameObj, targetPos);
                ContObj.I.change_facing (inGameObj, ((gameObject.transform.position.x > targetPos.x) ? "left" : "right"));
            }
            moveTimer = 0f; // Reset move timer
        }

        // If enough time has passed, use a random skill
        if (actionTimer <= 0) {
            use_random_skill(); 
        } else {
            actionTimer -= Time.deltaTime;
        }
    }

    // Helper method to use a random skill
    private void use_random_skill() { Debug.Log ("use");
        if (moveNum <= 3) {
            ContObj.I.use_skill_active(inGameObj, "fireball-shotgun");
            actionTimer = (moveNum <= 2) ? 2f : 7f;
        } else {
            ContObj.I.use_skill_active(inGameObj, "fire-orbs");
            actionTimer = 15f;
            moveNum = 0;
        }

        moveNum++;
    }

    // Helper method to get a random position within the map bounds
    private Vector2 get_random_position() {
        Vector2 mapSize = ContMap.I.details.size;
        Vector2 mapMin = Vector2.zero;
        Vector2 mapMax = new Vector2(mapSize.x, mapSize.y);

        // Generate a random position within the map's bounds
        float randomX = Random.Range(mapMin.x, mapMax.x);
        float randomY = Random.Range(mapMin.y, mapMax.y);
        return new Vector2(randomX, randomY);
    }
}