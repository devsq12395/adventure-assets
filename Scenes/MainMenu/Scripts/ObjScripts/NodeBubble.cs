using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NodeBubble : MonoBehaviour {
    
    public GameObject go;
    public string dir; // Left, Right, Up, Down
    
    public TextMeshProUGUI tTitle, tDesc; // Title and description text, set in the editor

    private Vector3 originalPosition;
    private Vector3 hiddenPosition;
    private float animationDuration = 0.5f; // Duration for tweening
    private Vector3 nodePosition; // Where the node circle is

    private void Start() {
        go = gameObject;

        // Determine the hidden position based on the direction
        switch (dir.ToLower()) {
            case "left":
                hiddenPosition = originalPosition + new Vector3(-200f, 0f, 0f); // Move off-screen to the left
                break;
            case "right":
                hiddenPosition = originalPosition + new Vector3(200f, 0f, 0f); // Move off-screen to the right
                break;
            case "up":
                hiddenPosition = originalPosition + new Vector3(0f, 200f, 0f); // Move off-screen upwards
                break;
            case "down":
                hiddenPosition = originalPosition + new Vector3(0f, -200f, 0f); // Move off-screen downwards
                break;
            default:
                hiddenPosition = originalPosition;
                break;
        }

        // Start with the bubble at the node position with scale 0 (hidden)
        go.transform.localPosition = originalPosition;
        go.transform.localScale = Vector3.zero;
        go.SetActive(false); // Make sure the bubble is hidden initially
    }

    public void show() {
        // Activate the game object, move it to its target position, and scale up from the node
        go.SetActive(true);
        
        // Tween movement from the node circle to the original position, while scaling up the bubble
        go.transform.DOLocalMove(originalPosition, animationDuration).SetEase(Ease.OutBack);
        go.transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack); // Grow from scale 0 to 1
    }

    public void hide() {
        // Tween back to the node circle position, while scaling back down to 0
        go.transform.DOLocalMove(originalPosition, animationDuration).SetEase(Ease.InBack);
        go.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() => {
            go.SetActive(false); // Deactivate after hiding
        });
    }
}
