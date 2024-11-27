using System.Collections;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    // Public variables to be set in the Unity Inspector
    public float interval = 0.01f; // Time interval between afterimages
    public float duration = 2f; // Duration for which the afterimages will appear
    public float afterimageLifetime = 0.2f; // Lifetime of each afterimage

    // Private variables
    private float timer = 0f; // Timer to track interval
    private float elapsedTime = 0f; // Timer to track duration
    private SpriteRenderer originalSpriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer from the original GameObject
        originalSpriteRenderer = GetComponent<SpriteRenderer>();

        // Start the coroutine to create afterimages
        StartCoroutine(SpawnAfterimages());
    }

    IEnumerator SpawnAfterimages()
    {
        while (elapsedTime < duration)
        {
            // Increment the elapsed time by the time since the last frame
            elapsedTime += Time.deltaTime;

            // Check if it's time to spawn a new afterimage
            if (timer >= interval)
            {
                CreateAfterimage();
                timer = 0f; // Reset timer after spawning afterimage
            }

            timer += Time.deltaTime; // Increment timer by the time since the last frame
            yield return null; // Wait for the next frame
        }

        // Remove the script after the duration ends
        Destroy(this);
    }

    void CreateAfterimage()
    {
        // Create a new GameObject for the afterimage
        GameObject afterimage = new GameObject($"{gameObject.name}_Afterimage");

        // Set its position and rotation to match the original GameObject
        afterimage.transform.position = transform.position;
        afterimage.transform.rotation = transform.rotation;
        afterimage.transform.localScale = transform.localScale;

        // Add a SpriteRenderer to the afterimage and set its sprite to match the original
        SpriteRenderer afterimageRenderer = afterimage.AddComponent<SpriteRenderer>();
        afterimageRenderer.sprite = originalSpriteRenderer.sprite;
        afterimageRenderer.color = new Color(afterimageRenderer.color.r, afterimageRenderer.color.g, afterimageRenderer.color.b, 0.5f);
        afterimageRenderer.sortingLayerID = originalSpriteRenderer.sortingLayerID; // Match sorting layer
        afterimageRenderer.sortingOrder = originalSpriteRenderer.sortingOrder; // Match sorting order

        // Set the afterimage to be destroyed after its lifetime
        Destroy(afterimage, afterimageLifetime);
    }
}
