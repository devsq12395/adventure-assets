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
    }

    void Update()
    {
        if (tilemapRenderer != null)
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
        if (!ContPlayer.I.player || ContPlayer.I.player == null) return false;
        
        // Calculate the distance between the player and the tilemap
        float distanceToPlayer = Vector2.Distance(transform.position, ContPlayer.I.player.transform.position);

        // Check if the distance is within the render distance
        return distanceToPlayer <= renderDistance;
    }
}
