using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogObj : MonoBehaviour {
    public TextMeshProUGUI dialogText;
    public Button closeButton;

    private RectTransform dialogRectTransform;
    private Vector2 initialSize;

    private void Start() {
        closeButton.onClick.AddListener(CloseDialog);
        dialogRectTransform = GetComponent<RectTransform>();
        initialSize = dialogRectTransform.sizeDelta;
    }

    public void SetDialogText(string text) {
        dialogText.text = text;
        AdjustDialogSize();
        PlayDialogAnimation();
    }

    private void AdjustDialogSize() {
        Vector2 textSize = new Vector2(dialogText.preferredWidth, dialogText.preferredHeight);
        dialogRectTransform.sizeDelta = initialSize + textSize;
    }

    private void PlayDialogAnimation() {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    private void CloseDialog() {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
    }
}