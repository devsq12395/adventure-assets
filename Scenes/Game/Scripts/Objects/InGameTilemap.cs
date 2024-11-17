using UnityEngine;
using UnityEngine.Tilemaps;

public class InGameTilemap : MonoBehaviour
{
    private TilemapRenderer tilemapRenderer;
    private Camera mainCamera;

    void Start()
    {
        // Get the TilemapRenderer from the child GameObject
        tilemapRenderer = GetComponentInChildren<TilemapRenderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (tilemapRenderer != null && mainCamera != null)
        {
            bool _visible = IsVisibleFrom(tilemapRenderer, mainCamera);
            tilemapRenderer.enabled = _visible;
        }
    }

    private bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
