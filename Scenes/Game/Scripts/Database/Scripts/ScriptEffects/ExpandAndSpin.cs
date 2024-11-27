using UnityEngine;
using DG.Tweening;

public class ExpandAndSpin : MonoBehaviour
{
    // Tween parameters
    public float expansionDuration = 2f; // Duration for expansion
    
    public float spinDuration = 3f; // Duration for spinning
    public float spinSpeed = 360f; // Rotation in degrees per second
    
    public float fadeDuration = 1.5f; // Duration for alpha fade

    private void Start()
    {
        // Store the original scale
        Vector3 originalScale = transform.localScale;

        // Set the scale to 0 initially
        transform.localScale = Vector3.zero;

        // Create a sequence to chain tweens
        Sequence sequence = DOTween.Sequence();

        // Expansion tween: expand from scale 0 to the original scale
        if (expansionDuration > 0)
        {
            sequence.Join(transform.DOScale(originalScale, expansionDuration).SetEase(Ease.OutQuad));
        }

        // Spin tween (infinite rotation for specified duration)
        if (spinDuration > 0)
        {
            sequence.Join(transform.DORotate(new Vector3(0f, 0f, spinSpeed), spinDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
        }

        // Fade out tween (adjust alpha)
        if (fadeDuration > 0)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                sequence.Join(spriteRenderer.DOFade(0, fadeDuration));
            }
        }

        // Destroy the object when the entire tween sequence is finished
        sequence.OnComplete(() => Destroy(gameObject));
    }
}
