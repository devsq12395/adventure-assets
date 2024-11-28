using UnityEngine;

public class ForcedMoveEffect : MonoBehaviour
{
    public float speed;
    public float duration;
    public float targetAngle;
    public string moveName;
    public string moveMode; // Use string instead of enum

    private Vector2 direction;
    public float elapsedTime;

    public InGameEffect inGameEffect;

    void Start()
    {
        // Calculate direction based on target angle
        float radians = targetAngle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        elapsedTime = 0f;

        inGameEffect = gameObject.GetComponent<InGameEffect>();
        if (!inGameEffect) {
            Destroy(this);
        }
    }

    public void setup_forced_move(float speed, float duration, float targetAngle, string moveMode, string moveName) {
        this.speed = speed;
        this.duration = duration;
        this.targetAngle = targetAngle;
        this.moveMode = moveMode;
        this.moveName = moveName;

        // Calculate direction based on target angle
        float radians = targetAngle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        elapsedTime = 0f;
    }

    public void update_pos() {
        if (elapsedTime < duration || duration <= 0) {
            if (moveMode == "constant") {
                inGameEffect.curPos += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
            } else if (moveMode == "knockback") {
                float knockbackSpeed = Mathf.Lerp(speed, 0, elapsedTime / duration);
                inGameEffect.curPos += new Vector3(direction.x, direction.y, 0) * knockbackSpeed * Time.deltaTime;
            }
            elapsedTime += Time.deltaTime;
        }
    }

    public static void AddForcedMove(GameObject obj, float speed, float duration, float targetAngle, string moveMode, string moveName) {
        var existingMoves = obj.GetComponents<ForcedMoveEffect>();
        foreach (var move in existingMoves) {
            if (move.moveName == moveName) {
                Destroy(move);
            }
        }
        var newMove = obj.AddComponent<ForcedMoveEffect>();
        newMove.setup_forced_move(speed, duration, targetAngle, moveMode, moveName);
    }
}