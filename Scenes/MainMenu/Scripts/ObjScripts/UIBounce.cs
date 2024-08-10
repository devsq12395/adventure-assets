using UnityEngine;
using System.Collections;

public class UIBounce : MonoBehaviour
{
    private Vector3 originalPosition;
    public float bounceDuration = 1.0f; // Duration for one bounce cycle (up and down)
    public float bounceHeight = 10.0f; // Height for the bounce

    private void Start()
    {
        originalPosition = transform.localPosition;
        StartCoroutine(BounceAnimation());
    }

    private IEnumerator BounceAnimation()
    {
        Vector3 targetPosition = originalPosition + new Vector3(0, bounceHeight, 0);

        while (true)
        {
            // Move up
            float timer = 0f;
            while (timer < bounceDuration / 2)
            {
                transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, timer / (bounceDuration / 2));
                timer += Time.deltaTime;
                yield return null;
            }

            // Ensure it reaches the target position
            transform.localPosition = targetPosition;

            // Move down
            timer = 0f;
            while (timer < bounceDuration / 2)
            {
                transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, timer / (bounceDuration / 2));
                timer += Time.deltaTime;
                yield return null;
            }

            // Ensure it reaches the original position
            transform.localPosition = originalPosition;
        }
    }
}
