using UnityEngine;

public class Spiral : MonoBehaviour
{
    public float speed = 5f; // The constant speed at which the object will move
    public float angularSpeed = 50f; // Angular speed in degrees per second

    private float angle = 0f; // Current angle for the spiral movement
    private Vector3 spawnPosition; // The position where the object spawns
    private float totalDistanceTraveled = 0f; // Accumulated distance traveled

    void Start()
    {
        spawnPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        // Increment the total distance traveled based on speed and time
        totalDistanceTraveled += speed * Time.deltaTime;

        // Increment the angle based on angular speed
        angle += angularSpeed * Time.deltaTime;

        // Update the radius using the total distance traveled
        float currentRadius = totalDistanceTraveled;

        // Calculate the new position using polar coordinates
        float x = spawnPosition.x + currentRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = spawnPosition.y + currentRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Update the object's position
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
