using UnityEngine;
using DG.Tweening;

public class HeartbeatScale : MonoBehaviour
{
    public float scaleAmount = 1.1f; // The amount to scale up
    public float duration = 0.5f; // Duration of one heartbeat cycle

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartHeartbeat();
    }

    private void StartHeartbeat()
    {
        // Scale up and down like a heartbeat
        transform.DOScale(originalScale * scaleAmount, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
