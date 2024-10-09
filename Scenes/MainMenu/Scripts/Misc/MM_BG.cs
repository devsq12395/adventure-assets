using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagScroller : MonoBehaviour
{
    public GameObject flagPrefab;  // Prefab of the flag
    public Canvas parentCanvas;  // Assign this in the editor (the canvas where the flag canvases will be added as children)
    private List<Canvas> canvases = new List<Canvas>();  // List to store the flag canvases
    private float canvasWidth;  // Width of each flag canvas

    public int rows;  // Number of rows in each flag canvas
    public int columns;  // Number of columns in each flag canvas
    public float spacingX;  // Horizontal spacing between flags
    public float spacingY;  // Vertical spacing between flags
    public float scrollSpeed;  // Speed of scrolling

    public float xOffset;  // Horizontal offset for positioning
    public float yOffset;  // Vertical offset for positioning

    void Start()
    {
        // Initialize variables directly in Start
        rows = 11;  // Number of rows
        columns = 4;  // Number of columns
        spacingX = 300f;  // Horizontal spacing
        spacingY = 150f;  // Vertical spacing
        scrollSpeed = 50f;  // Scrolling speed

        xOffset = 0f;  // Initial X offset
        yOffset = 500f;  // Initial Y offset

        // Create two flag canvases for seamless scrolling
        for (int i = 0; i < 5; i++)
        {
            CreateCanvas(i);
        }
    }

    // Creates a flag canvas dynamically and fills it with flags in a diagonal pattern
    void CreateCanvas(int index)
    {
        // Dynamically create a new Canvas GameObject
        GameObject newCanvasObj = new GameObject("FlagCanvas" + index);
        Canvas newCanvas = newCanvasObj.AddComponent<Canvas>();
        newCanvas.renderMode = RenderMode.WorldSpace;  // Keep WorldSpace so it can be a child of the parent canvas
        newCanvasObj.AddComponent<CanvasScaler>();  // Add a CanvasScaler for scaling UI elements
        newCanvasObj.AddComponent<GraphicRaycaster>();  // Add a GraphicRaycaster for UI interactions

        // Set the parent to the defined parentCanvas in the editor
        newCanvas.transform.SetParent(parentCanvas.transform, false);

        // Position the new canvas to the right of the previous canvas (based on index)
        RectTransform canvasRect = newCanvas.GetComponent<RectTransform>();
        canvasWidth = columns * spacingX;  // Calculate canvas width based on column count and spacing
        canvasRect.sizeDelta = new Vector2(canvasWidth, parentCanvas.GetComponent<RectTransform>().rect.height);  // Set canvas size
        canvasRect.anchoredPosition = new Vector2(index * canvasWidth + xOffset, yOffset);  // Position the canvas with offset

        // Fill the canvas with flags in a diagonal pattern
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Diagonal pattern logic: Place a flag when (row + col) is even
                if ((row + col) % 2 == 0)
                {
                    // Instantiate flag prefab inside the canvas
                    GameObject flag = Instantiate(flagPrefab, newCanvas.transform);
                    RectTransform flagRect = flag.GetComponent<RectTransform>();

                    // Set position of the flag based on row and column (Diagonal arrangement)
                    Vector2 position = new Vector2(col * spacingX, -row * spacingY);
                    flagRect.anchoredPosition = position;
                }
            }
        }

        // Add the new canvas to the list of canvases
        canvases.Add(newCanvas);
    }

    void Update()
    {
        // Scroll each canvas to the left
        foreach (Canvas canvas in canvases)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            canvasRect.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

            // Check if the canvas has gone off the screen (left side)
            if (canvasRect.anchoredPosition.x < -canvasWidth * 2)
            {
                // Reposition the canvas to the right of the last canvas
                RectTransform lastCanvasRect = canvases[canvases.Count - 1].GetComponent<RectTransform>();
                canvasRect.anchoredPosition = new Vector2(lastCanvasRect.anchoredPosition.x + canvasWidth, yOffset);

                // Move the canvas to the end of the list to keep the scrolling order
                canvases.Remove(canvas);
                canvases.Add(canvas);
            }
        }
    }
}
