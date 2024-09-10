using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NodeBubble : MonoBehaviour {
    
    public GameObject go;
    public string dir; // Left, Right, Up, Down
    
    public TextMeshProUGUI tTitle, tDesc; // Title and description text, set in the editor
    public Image iArea;

    private Vector3 originalPosition;
    private Vector3 hiddenPosition;
    private float animationDuration = 0.5f; // Duration for tweening
    private Vector3 nodePosition; // Where the node circle is

    private void Start() {
        go = gameObject;

        go.transform.localScale = Vector3.zero;
        go.SetActive(false); 
    }

    public void show() {
        go.SetActive(true);
        
        go.transform.DOScale(new Vector3(1f, 1f, 1), animationDuration).SetEase(Ease.OutBack); // Grow from scale 0 to 1
    }

    public void hide() {
        go.transform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack).OnComplete(() => {
            go.SetActive(false); // Deactivate after hiding
        });
    }
}
