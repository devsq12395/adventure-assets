using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour
{
    private InGameObject unitStats;
    private Image healthBarImage;
    private Image healthBarBaseImage;
    private Canvas healthBarCanvas;

    public bool isReady;
    public string mode;

    private float baseScaleMultiplier = 1.25f; // Increase the size by 25%

    private float targetHealth;
    private Vector3 originalLocalPosition;

    private float previousHealth;

    private float lerpSpeed = 5f; // Cache lerp speed
    private Color currentColor = Color.white; // Cache current color

    private void Awake() {
        unitStats = GetComponentInParent<InGameObject>();
    }

    public void Setup(string _mode) {
        if (unitStats == null) return;
        if (unitStats.type != "unit") return;

        // Ensure the health bar has a canvas
        mode = _mode;
        CreateCanvasIfNeeded();

        // Initialize targetHealth to the current health percentage
        if (_mode == "health") {
            targetHealth = (float)unitStats.hp / unitStats.hpMax;
        } else if (_mode == "stamina") {
            targetHealth = (float)ContPlayer.I.sta / ContPlayer.I.staMax;
        }

        // X-axis scale multiplier based on unitStats.hpBarScaleX
        float scaleMultiplierX = unitStats.hpBarScaleX > 0 ? unitStats.hpBarScaleX : 1f;

        // Create the health bar and its base
        switch (_mode) {
            case "health":
                healthBarBaseImage = CreateBar("hp-bar-base", 
                    new Vector2(1.5f * baseScaleMultiplier, 1.25f * baseScaleMultiplier), 
                    new Vector2(0, 1.75f), 
                    scaleMultiplierX);
                healthBarImage = CreateBar("hp-bar", 
                    new Vector2(1.5f * baseScaleMultiplier, 1.25f * baseScaleMultiplier), 
                    new Vector2(0, 1.75f), 
                    scaleMultiplierX);
                break;

            case "stamina":
                healthBarBaseImage = CreateBar("sta-bar-base", 
                    new Vector2(1.5f * baseScaleMultiplier, 1f * baseScaleMultiplier), 
                    new Vector2(0, 1.25f), 
                    scaleMultiplierX); // Positioned below health bar
                healthBarImage = CreateBar("sta-bar", 
                    new Vector2(1.5f * baseScaleMultiplier, 1f * baseScaleMultiplier), 
                    new Vector2(0, 1.25f), 
                    scaleMultiplierX);
                break;
        }

        if (healthBarImage != null) {
            originalLocalPosition = healthBarImage.transform.localPosition;
        }

        isReady = true;
    }

    private void CreateCanvasIfNeeded() {
        // Check if there's already a Canvas in the parent hierarchy
        healthBarCanvas = GetComponentInParent<Canvas>();

        if (healthBarCanvas == null) {
            // Create a new Canvas if none exists
            GameObject canvasObj = new GameObject("HealthBarCanvas");
            canvasObj.transform.SetParent(transform);
            healthBarCanvas = canvasObj.AddComponent<Canvas>();
            healthBarCanvas.renderMode = RenderMode.WorldSpace; // Set to World Space to stay with the unit
            healthBarCanvas.sortingLayerName = "hp-bars";
            healthBarCanvas.sortingOrder = 2; 

            // Add a CanvasScaler and a GraphicRaycaster (optional)
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();

            // Adjust the size and position of the Canvas
            RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
            canvasRect.sizeDelta = new Vector2(1.25f, 1.25f); // Adjust size as needed
            canvasRect.localPosition = Vector3.zero;
        }
    }

    private Image CreateBar(string spriteName, Vector2 size, Vector2 anchorPos, float scaleMultiplierX) {
        GameObject barObj = new GameObject(spriteName);
        barObj.transform.SetParent(healthBarCanvas.transform);

        RectTransform rectTransform = barObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.pivot = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = anchorPos;

        Image barImage = barObj.AddComponent<Image>();
        barImage.sprite = Sprites.I.get_sprite(spriteName);

        rectTransform.sizeDelta = size * new Vector2(scaleMultiplierX, 1); // Only apply scaleMultiplierX to the X axis
        barImage.type = Image.Type.Filled;
        barImage.fillMethod = Image.FillMethod.Horizontal;
        barImage.fillOrigin = (int)Image.OriginHorizontal.Right;

        return barImage;
    }

    private void Update() {
        if (!isReady || unitStats == null || healthBarImage == null || healthBarBaseImage == null) return;

        float currentPercentage;
        if (mode == "health") {
            currentPercentage = (float)unitStats.hp / unitStats.hpMax;
        } else if (mode == "stamina") {
            currentPercentage = (float)ContPlayer.I.sta / ContPlayer.I.staMax;
        } else {
            return;
        }

        if (Mathf.Abs(currentPercentage - previousHealth) > Mathf.Epsilon) {
            targetHealth = currentPercentage;
            previousHealth = currentPercentage;
        }

        // Smooth transition
        float currentFill = healthBarImage.fillAmount;
        float lerpFactor = Time.deltaTime * lerpSpeed;
        healthBarImage.fillAmount = Mathf.Lerp(currentFill, targetHealth, lerpFactor);

        // Color pulsing effect for health bar
        if (mode == "health" && targetHealth < 0.3f) {
            float t = Mathf.Sin(Time.time * 5f) * 0.5f + 0.5f; // Oscillates between 0 and 1
            Color targetColor = Color.Lerp(Color.white, new Color(1f, 0.6f, 0.6f, 1f), t);
            if (currentColor != targetColor) {
                healthBarImage.color = targetColor;
                currentColor = targetColor;
            }
        } else if (currentColor != Color.white) {
            healthBarImage.color = Color.white;
            currentColor = Color.white;
        }

        // Adjust the scale based on the facing direction
        float scaleX = (unitStats.facing == "left") ? 1f : -1f;
        healthBarCanvas.transform.localScale = new Vector3(scaleX, healthBarCanvas.transform.localScale.y, healthBarCanvas.transform.localScale.z);
    }
}
