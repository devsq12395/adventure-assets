using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_BG : MonoBehaviour
{
    public Image imagePrefab;  // Assign a prefab of the UI Image (flag or game title) from the Inspector
    public int rows;  // Number of rows
    public int columns;  // Number of columns
    public float spacingX;  // Horizontal spacing between images
    public float spacingY;  // Vertical spacing between images
    public float scrollSpeed;  // Speed of horizontal scrolling
    public Canvas canvas;  // Reference to the Canvas in which the images will be created

    private List<Image> imageInstances = new List<Image>();
    private RectTransform canvasRect;  // To store the bounds of the canvas

    void Start()
    {
        rows = 3;  // Number of rows
        columns = 6;  // Number of columns
        spacingX = 150f;  // Horizontal spacing between images
        spacingY = 150f;  // Vertical spacing between images
        scrollSpeed = 2.0f;  // Speed of horizontal scrolling

        // Get the Canvas RectTransform to define boundaries
        canvasRect = canvas.GetComponent<RectTransform>();

        // Create the grid of images in a diagonal pattern
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Alternate between image and empty spot based on the pattern (0 and X)
                if ((row + col) % 2 == 0)
                {
                    Vector3 position = new Vector3(col * spacingX, row * spacingY, 0);
                    Image image = Instantiate(imagePrefab, canvas.transform);
                    RectTransform imageRect = image.GetComponent<RectTransform>();
                    imageRect.anchoredPosition = position;  // Set anchored position within the Canvas
                    imageInstances.Add(image);
                }
            }
        }
    }

    void Update()
    {
        // Scroll the images horizontally
        foreach (Image image in imageInstances)
        {
            RectTransform imageRect = image.GetComponent<RectTransform>();
            imageRect.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime * 100;

            // Reset image position if it goes off-screen to the left
            if (imageRect.anchoredPosition.x < -canvasRect.rect.width)
            {
                float yPos = imageRect.anchoredPosition.y;  // Maintain Y position
                imageRect.anchoredPosition = new Vector2(canvasRect.rect.width + spacingX, yPos);
            }
        }
    }
}
