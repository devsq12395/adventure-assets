using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private InGameObject unitStats;
    private Image healthBarImage;
    private Image healthBarBaseImage;
    private Canvas healthBarCanvas;

    public bool isReady;
    public string mode;

    private float baseScaleMultiplier = 1.25f; // Increase the size by 25%

    private void Awake() {
        unitStats = GetComponentInParent<InGameObject>();
    }

    public void Setup(string _mode) {
        if (unitStats == null) return;
        if (unitStats.type != "unit") return;

        // Ensure the health bar has a canvas
        mode = _mode;
        CreateCanvasIfNeeded();

        // X-axis scale multiplier based on unitStats.hpBarScaleX
        float scaleMultiplierX = unitStats.hpBarScaleX > 0 ? unitStats.hpBarScaleX : 1f;

        // Create the health bar and its base
        switch (_mode) {
            case "health":
                healthBarBaseImage = CreateBar("hp-bar-base", 
                    new Vector2(1.5f * baseScaleMultiplier, 1.75f * baseScaleMultiplier), 
                    new Vector2(0, 1.75f), 
                    scaleMultiplierX);
                healthBarImage = CreateBar("hp-bar", 
                    new Vector2(1.5f * baseScaleMultiplier, 1.75f * baseScaleMultiplier), 
                    new Vector2(0, 1.75f), 
                    scaleMultiplierX);
                break;

            case "stamina":
                healthBarBaseImage = CreateBar("sta-bar-base", 
                    new Vector2(1.5f * baseScaleMultiplier, 1.25f * baseScaleMultiplier), 
                    new Vector2(0, 1.25f), 
                    scaleMultiplierX); // Positioned below health bar
                healthBarImage = CreateBar("sta-bar", 
                    new Vector2(1.5f * baseScaleMultiplier, 1.25f * baseScaleMultiplier), 
                    new Vector2(0, 1.25f), 
                    scaleMultiplierX); // Positioned below health bar
                break;
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
        if (!isReady) return;

        if (unitStats != null && healthBarImage != null) {
            switch (mode) {
                case "health":
                    healthBarImage.fillAmount = (float)unitStats.hp / unitStats.hpMax;
                    break;

                case "stamina":
                    healthBarImage.fillAmount = (float)ContPlayer.I.sta / ContPlayer.I.staMax;
                    break;
            }

            // Adjust the scale based on the facing direction
            float scaleX = (unitStats.facing == "left") ? 1f : -1f;
            healthBarCanvas.transform.localScale = new Vector3(scaleX, healthBarCanvas.transform.localScale.y, healthBarCanvas.transform.localScale.z);
        }
    }
}
