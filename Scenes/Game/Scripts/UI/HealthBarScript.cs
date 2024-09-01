using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private InGameObject unitStats;
    private Image healthBarImage;
    private Image healthBarBaseImage;
    private Canvas healthBarCanvas;

    private void Awake()
    {
        unitStats = GetComponentInParent<InGameObject>();

        if (unitStats == null) return;
        if (unitStats.type != "unit") return;

        // Ensure the health bar has a canvas
        CreateCanvasIfNeeded();

        // Create the health bar and its base
        healthBarBaseImage = CreateHpBar("hp-bar-base", new Vector2(1.5f, 1.75f), new Vector2(0, 1f));
        healthBarImage = CreateHpBar("hp-bar", new Vector2(1.5f, 1.75f), new Vector2(0, 1f));
    }

    private void CreateCanvasIfNeeded()
    {
        // Check if there's already a Canvas in the parent hierarchy
        healthBarCanvas = GetComponentInParent<Canvas>();

        if (healthBarCanvas == null)
        {
            // Create a new Canvas if none exists
            GameObject canvasObj = new GameObject("HealthBarCanvas");
            canvasObj.transform.SetParent(transform);
            healthBarCanvas = canvasObj.AddComponent<Canvas>();
            healthBarCanvas.renderMode = RenderMode.WorldSpace; // Set to World Space if you want it to stay with the unit

            // Add a CanvasScaler and a GraphicRaycaster (optional)
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();

            // Adjust the size and position of the Canvas
            RectTransform canvasRect = canvasObj.GetComponent<RectTransform>();
            canvasRect.sizeDelta = new Vector2(1.25f, 1.25f); // Adjust size as needed
            canvasRect.localPosition = Vector3.zero;
        }
    }

    private Image CreateHpBar(string spriteName, Vector2 size, Vector2 anchorPos)
    {
        GameObject healthBarObj = new GameObject(spriteName);
        healthBarObj.transform.SetParent(healthBarCanvas.transform);

        RectTransform rectTransform = healthBarObj.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 1.5f);
        rectTransform.pivot = new Vector2(0.5f, 1);
        rectTransform.anchoredPosition = anchorPos;

        Image hpBarImage = healthBarObj.AddComponent<Image>();
        hpBarImage.sprite = Sprites.I.get_sprite(spriteName);

        rectTransform.sizeDelta = size;
        hpBarImage.type = Image.Type.Filled;
        hpBarImage.fillMethod = Image.FillMethod.Horizontal;
        hpBarImage.fillOrigin = (int)Image.OriginHorizontal.Right;

        return hpBarImage;
    }

    private void Update()
    {
        if (unitStats != null && healthBarImage != null)
        {
            healthBarImage.fillAmount = (float)unitStats.hp / unitStats.hpMax;
            float scaleX = ((unitStats.facing == "left") ? 1.5f : -1.75f);
            healthBarCanvas.transform.localScale = new Vector3(scaleX, 1.25f, 1);

        }
    }
}
