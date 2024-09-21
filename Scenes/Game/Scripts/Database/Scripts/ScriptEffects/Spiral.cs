using UnityEngine;

public class Spiral : MonoBehaviour
{
    public float speed = 5f; // The constant speed at which the object will move
    public float angularSpeed = 50f; // Angular speed in degrees per second

    private float angle = 0f; // Current angle for the spiral movement
    private Vector3 spawnPosition; // The position where the object spawns

    void Start()
    {
        spawnPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        // Calculate the amount of distance traveled in the spiral per frame
        float distanceTraveled = speed * Time.deltaTime;

        // Increment the angle based on angular speed
        angle += angularSpeed * Time.deltaTime;

        // Update the current radius to ensure constant speed
        float currentRadius = distanceTraveled / (Mathf.Deg2Rad * angularSpeed);

        // Calculate the new position using polar coordinates
        float x = spawnPosition.x + currentRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = spawnPosition.y + currentRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Update the object's position
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
