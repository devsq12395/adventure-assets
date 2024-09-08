using UnityEngine;
using System.Collections;

public class UIBounce : MonoBehaviour
{
    private Vector3 originalPosition;
    public float bounceDuration = 1.0f; // Duration for one bounce cycle (up and down)
    public float bounceHeight = 10.0f; // Height for the vertical bounce
    public float bounceWidth = 0f;  // Width for the horizontal bounce
    private Coroutine bounceCoroutine;

    private void OnEnable()
    {
        // Update the original position when the object is enabled
        originalPosition = transform.localPosition;

        // Start the bounce animation when the object is enabled
        if (bounceCoroutine == null)
        {
            bounceCoroutine = StartCoroutine(BounceAnimation());
        }
    }

    private void OnDisable()
    {
        // Stop the bounce animation when the object is disabled
        if (bounceCoroutine != null)
        {
            StopCoroutine(bounceCoroutine);
            bounceCoroutine = null;
        }

        // Optionally, reset the position to the original position
        transform.localPosition = originalPosition;
    }

    private IEnumerator BounceAnimation()
    {
        Vector3 targetPosition = originalPosition + new Vector3(bounceWidth, bounceHeight, 0);

        while (true)
        {
            // Move up and to the right
            float timer = 0f;
            while (timer < bounceDuration / 2)
            {
                transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, timer / (bounceDuration / 2));
                timer += Time.deltaTime;
                yield return null;
            }

            // Ensure it reaches the target position
            transform.localPosition = targetPosition;

            // Move down and to the left
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
