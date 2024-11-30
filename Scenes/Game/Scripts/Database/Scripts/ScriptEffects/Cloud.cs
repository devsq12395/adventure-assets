using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float moveSpeed;
    private float mapSizeX;
    private const float destroyDistance = 50f;

    void Start()
    {
        moveSpeed = 1f;
    }

    void Update()
    {
        InGameEffect inGameEffect = GetComponent<InGameEffect>();
        inGameEffect.curPos += Vector3.right * moveSpeed * Time.deltaTime;

        if (inGameEffect.curPos.x > ContMap.I.details.size.x || inGameEffect.curPos.x < -ContMap.I.details.size.x)
        {
            Destroy(gameObject);
        }

        InGameObject inGameObject = ContPlayer.I.player;
        if (Vector2.Distance(inGameObject.transform.position, inGameEffect.curPos) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
