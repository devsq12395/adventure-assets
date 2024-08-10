using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MUI_Overlay : MonoBehaviour
{
    public static MUI_Overlay I;

    public Image i_blood, i_zoom;

    private void Awake()
    {
        I = this;

        // Ensure the images have a CanvasGroup component for alpha control
        AddCanvasGroup(i_blood);
        AddCanvasGroup(i_zoom);
    }

    private void AddCanvasGroup(Image image)
    {
        if (image != null && image.GetComponent<CanvasGroup>() == null)
        {
            image.gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void show_overlay(string _name)
    {
        Image targetImage = null;

        switch (_name)
        {
            case "blood":
                targetImage = i_blood;
                break;
            case "zoom":
                targetImage = i_zoom;
                break;
        }

        if (targetImage != null)
        {
            CanvasGroup canvasGroup = targetImage.GetComponent<CanvasGroup>();
            targetImage.gameObject.SetActive(true);
            canvasGroup.alpha = 0;

            // Sequence to handle the tweening effect
            Sequence sequence = DOTween.Sequence();
            sequence.Append(canvasGroup.DOFade(0.7f, 0.15f))
                    .Append(canvasGroup.DOFade(0f, 0.6f))
                    .OnComplete(() => targetImage.gameObject.SetActive(false));  // Deactivate after the sequence
        }
    }
}