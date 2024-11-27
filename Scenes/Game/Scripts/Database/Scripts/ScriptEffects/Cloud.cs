using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float moveSpeed;
    private float mapSizeX;

    void Start()
    {
        moveSpeed = 1f;
    }

    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (transform.position.x > ContMap.I.details.size.x || transform.position.x < -ContMap.I.details.size.x)
        {
            Destroy(gameObject);
        }
    }
}
