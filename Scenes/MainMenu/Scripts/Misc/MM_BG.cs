using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM_BG : MonoBehaviour
{
    public Image imagePrefab;  // Assign a prefab of the UI Image (flag or game title) from the Inspector
    public Canvas canvas;  // Reference to the Canvas in which the images will be created

    private List<Image> imageInstances = new List<Image>();
    private int rows;  // Number of rows
    private int columns;  // Number of columns
    private float spacingX;  // Horizontal spacing between images
    private float spacingY;  // Vertical spacing between images
    private float scrollSpeed;  // Speed of horizontal scrolling
    private float batchWidth;  // Width of a single batch
    private int batchesCount;  // Number of batches to create

    void Start()
    {
        // Set the number of rows, columns, spacing, and scroll speed
        rows = 3;  // Number of rows
        columns = 6;  // Number of columns

        // Calculate spacing based on screen size
        spacingX = Screen.width / (columns + 1);  // Dynamic horizontal spacing based on screen width
        spacingY = Screen.height / (rows + 1);    // Dynamic vertical spacing based on screen height
        scrollSpeed = 2.0f;  // Speed of horizontal scrolling

        // Calculate the total width of one batch
        batchWidth = columns * spacingX;
        batchesCount = 2;  // Number of batches to create

        // Create the initial batches of images
        CreateInitialBatches();
    }

    void CreateInitialBatches()
    {
        // Create two batches of images
        for (int batch = 0; batch < batchesCount; batch++)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    if ((row + col) % 2 == 0) // Alternate flags
                    {
                        Image image = Instantiate(imagePrefab, canvas.transform);
                        RectTransform imageRect = image.GetComponent<RectTransform>();

                        // Set the anchor and pivot to top-left for proper positioning
                        imageRect.anchorMin = new Vector2(0, 1);
                        imageRect.anchorMax = new Vector2(0, 1);
                        imageRect.pivot = new Vector2(0, 1);

                        // Calculate the position based on screen size and batch index
                        Vector2 position = new Vector2(col * spacingX + (batch * batchWidth), -row * spacingY);  // Shift based on batch index
                        imageRect.anchoredPosition = position;

                        // Add the image to the list for scrolling
                        imageInstances.Add(image);
                    }
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

            // Move the image to the left based on scrollSpeed
            imageRect.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime * 100;

            // Check if the first column of the first batch has gone off-screen
            if (imageRect.anchoredPosition.x < -spacingX)
            {
                // Reset the position to the right side just outside the visible area
                float yPos = imageRect.anchoredPosition.y;  // Maintain the Y position
                imageRect.anchoredPosition = new Vector2((batchesCount * batchWidth) + spacingX, yPos);
            }
        }

        // Check if we need to instantiate a new batch of images
        if (imageInstances.Count > 0 && imageInstances[0].GetComponent<RectTransform>().anchoredPosition.x < -spacingX)
        {
            CreateNewBatch();
        }
    }

    void CreateNewBatch()
    {
        // Create a new batch of images positioned just behind the last visible batch
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if ((row + col) % 2 == 0) // Alternate flags
                {
                    Image image = Instantiate(imagePrefab, canvas.transform);
                    RectTransform imageRect = image.GetComponent<RectTransform>();

                    // Set the anchor and pivot to top-left for proper positioning
                    imageRect.anchorMin = new Vector2(0, 1);
                    imageRect.anchorMax = new Vector2(0, 1);
                    imageRect.pivot = new Vector2(0, 1);

                    // Calculate the position just behind the last visible batch
                    Vector2 position = new Vector2(batchesCount * batchWidth + (col * spacingX), -row * spacingY);
                    imageRect.anchoredPosition = position;

                    // Add the image to the list for scrolling
                    imageInstances.Add(image);
                }
            }
        }
    }
}
