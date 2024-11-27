using UnityEngine;

public class ForcedMove : MonoBehaviour
{
    public enum MoveMode { Constant, Knockback }
    public MoveMode moveMode;
    public float speed;
    public float duration;
    public float targetAngle;
    public string moveName;

    private Vector2 direction;
    public float elapsedTime;

    public InGameObject inGameObject;

    void Start()
    {
        // Calculate direction based on target angle
        float radians = targetAngle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        elapsedTime = 0f;

        inGameObject = gameObject.GetComponent<InGameObject>();
        if (!inGameObject) {
            Destroy (this);
        }
    }

    public void setup_forced_move(float speed, float duration, float targetAngle, MoveMode moveMode, string moveName) {
        this.speed = speed;
        this.duration = duration;
        this.targetAngle = targetAngle;
        this.moveMode = moveMode;
        this.moveName = moveName;

        // Calculate direction based on target angle
        float radians = targetAngle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        elapsedTime = 0f;

        inGameObject = gameObject.GetComponent<InGameObject>();
        if (!inGameObject) {
            Destroy(this);
        }
    }

    public void update_pos () {
        if (elapsedTime < duration || duration <= 0)
        {
            switch (moveMode)
            {
                case MoveMode.Constant:
                    inGameObject.curPos += direction * speed * Time.deltaTime;
                    break;
                case MoveMode.Knockback:
                    float knockbackSpeed = Mathf.Lerp(speed, 0, elapsedTime / duration);
                    inGameObject.curPos += direction * knockbackSpeed * Time.deltaTime;
                    break;
            }
            elapsedTime += Time.deltaTime;
        }
    }

    public static void AddForcedMove(GameObject obj, float speed, float duration, float targetAngle, MoveMode moveMode, string moveName) {
        var existingMoves = obj.GetComponents<ForcedMove>();
        foreach (var move in existingMoves) {
            if (move.moveName == moveName) {
                Destroy(move);
            }
        }

        var newMove = obj.AddComponent<ForcedMove>();
        newMove.setup_forced_move(speed, duration, targetAngle, moveMode, moveName);
    }
}