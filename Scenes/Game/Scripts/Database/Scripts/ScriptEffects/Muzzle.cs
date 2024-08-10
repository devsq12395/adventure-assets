using UnityEngine;
using DG.Tweening;

public class Muzzle : MonoBehaviour
{
    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Store the original scale
        originalScale = transform.localScale;

        // Set the initial scale to zero
        transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

        // Get the SpriteRenderer component to tween the alpha
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tween to the original scale in 0.2 seconds
        transform.DOScale(originalScale, 0.05f).OnComplete(() =>
        {
            // Tween the scale and alpha to zero in 0.8 seconds
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(new Vector3 (0.2f, 0.2f, 0.2f), 0.2f));
            sequence.Join(spriteRenderer.DOFade(0, 0.2f));
            sequence.OnComplete(() =>
            {
                // Destroy the GameObject after the tween
                Destroy(gameObject);
            });
        });
    }
}
