using UnityEngine;
using UnityEngine.Tilemaps;

public class InGameTilemap : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Transform playerTransform;

    private float renderDistance;

    void Start()
    {
        // Get the TilemapRenderer from the child GameObject
        tilemapRenderer = GetComponentInChildren<TilemapRenderer>();
        renderDistance = 100f;
        
        // Reference to the player transform
        if (ContPlayer.I != null && ContPlayer.I.player != null)
        {
            playerTransform = ContPlayer.I.player.transform;
        }
        else
        {
            Debug.LogError("ContPlayer or player reference is missing! Ensure the player is initialized.");
        }

        if (tilemapRenderer == null)
        {
            Debug.LogError("TilemapRenderer not found! Make sure the script is attached to a GameObject with a TilemapRenderer.");
        }
    }

    void Update()
    {
        if (tilemapRenderer != null && playerTransform != null)
        {
            bool _visible = IsPlayerWithinRenderDistance();
            if (tilemapRenderer.enabled != _visible)
            {
                tilemapRenderer.enabled = _visible;
            }
        }
    }

    private bool IsPlayerWithinRenderDistance()
    {
        // Calculate the distance between the player and the tilemap
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Check if the distance is within the render distance
        return distanceToPlayer <= renderDistance;
    }
}
