using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compass : MonoBehaviour
{
    public static Compass I;
    public void Awake() { I = this; }
    
    public Image iCompass;
    public TextMeshProUGUI tDistance;
    public GameObject goBoss;

    public void setup_boss(GameObject _boss)
    {
        goBoss = _boss;
    }

    void Update()
    {
        if (goBoss != null)
        {
            GameObject player = ContPlayer.I.player.gameObject;

            // Calculate direction and distance
            Vector3 direction = goBoss.transform.position - player.transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            bool _isClose = (distance <= 15f);
            iCompass.enabled = !_isClose;
            tDistance.enabled = !_isClose;
            if (_isClose) return;

            // Calculate angle (for 2D, use direction.y instead of direction.z)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Position the compass image
            float radius = 350f; // Adjust based on your UI design
            Vector2 position = new Vector2(radius * Mathf.Cos(angle * Mathf.Deg2Rad), radius * Mathf.Sin(angle * Mathf.Deg2Rad));
            iCompass.rectTransform.anchoredPosition = position;

            // Position the distance text below the compass image
            tDistance.rectTransform.anchoredPosition = position + new Vector2(0, -50);

            // Rotate the compass image
            iCompass.rectTransform.rotation = Quaternion.Euler(0, 0, angle);

            // Update distance text
            tDistance.text = distance.ToString("F1") + " m";
        }
    }
}
