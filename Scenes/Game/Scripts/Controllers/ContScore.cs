using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using DG.Tweening;

public class ContScore : MonoBehaviour {

    public static ContScore I;
    public void Awake(){ I = this; }

    public TextMeshProUGUI tScore, tCombo, tComboRating;
    public TextMeshProUGUI tScoreExpand, tComboExpand, tComboRatingExpand;

    public Image I_ComboRating, I_ComboRating2;
    public Sprite iGood, iNice, iExcellent, iSuper, iExtreme, iUnstoppable;

    public int combo, comboRating, score, level;
    public Dictionary<int, string> comboRatings;
    public Dictionary<int, int> levels;

    private int previousMaxComboKey = 0;

    public void setup() {
        define_combo_ratings();
        define_levels();

        tCombo.text = "";
        tComboRating.text = "";

        // Duplicate existing TextMeshProUGUI components for expanding effects
        tScoreExpand = Instantiate(tScore, tScore.transform.parent);
        tComboExpand = Instantiate(tCombo, tCombo.transform.parent);
        tComboRatingExpand = Instantiate(tComboRating, tComboRating.transform.parent);

        // Set initial alpha for expanding texts
        tScoreExpand.alpha = 0.5f;
        tComboExpand.alpha = 0.5f;
        tComboRatingExpand.alpha = 0.5f;

        // Instantiate I_ComboRating2 as a copy of I_ComboRating
        I_ComboRating2 = Instantiate(I_ComboRating, I_ComboRating.transform.parent);

        // Set initial properties for I_ComboRating2
        I_ComboRating2.transform.localScale = Vector3.one * 1.2f;

        // Set initial alpha for I_ComboRating and I_ComboRating2
        I_ComboRating.color = new Color(I_ComboRating.color.r, I_ComboRating.color.g, I_ComboRating.color.b, 0f);
        I_ComboRating2.color = new Color(I_ComboRating2.color.r, I_ComboRating2.color.g, I_ComboRating2.color.b, 0f);
    }

    public void define_combo_ratings() {
        comboRatings = new Dictionary<int, string>(){
            { 5, "Good!" },
            { 10, "Nice!" },
            { 15, "Excellent!" },
            { 20, "Superb!" },
            { 25, "Extreme!" },
            { 30, "Unstoppable!" }
        };
    }

    public void define_levels() {
        levels = new Dictionary<int, int>(){
            { 1, 100 },
            { 2, 300 },
            { 3, 600 },
            { 4, 1000 }
        };
    }

    public void add_score(int _points) {
        score += _points;
        int maxLevel = levels.Keys.Where(key => levels[key] <= score).DefaultIfEmpty(0).Max();
        if (maxLevel > level) {
            level = maxLevel;
            // Logic to handle level up, e.g., notify player, increase difficulty, etc.
        }
        int nextLevelScore = levels.ContainsKey(level + 1) ? levels[level + 1] : score;
        tScore.text = $"Score: {score}/{nextLevelScore}";
        tScoreExpand.text = tScore.text;

        // Reset scale and alpha for expanding score text
        tScoreExpand.transform.localScale = Vector3.one;
        tScoreExpand.alpha = 0.6f;

        // Add a slow expand animation and alpha tween to the expanding score text
        tScoreExpand.transform.DOScale(1.2f, 1f).SetEase(Ease.OutQuad);
        tScoreExpand.DOFade(0, 1f).SetEase(Ease.OutQuad);
    }

    public void add_combo(int _increment) {
        combo += _increment;
        if (combo >= 2) {
            string colorHex = "#FFFFFF"; // Default white
            foreach (var key in comboRatings.Keys.OrderByDescending(k => k)) {
                if (combo >= key) {
                    switch (key) {
                        case 5:
                            colorHex = "#0000FF"; // Blue for Good
                            break;
                        case 10:
                            colorHex = "#0000FF"; // Blue for Nice
                            break;
                        case 15:
                            colorHex = "#FFFF00"; // Yellow for Excellent
                            break;
                        case 20:
                            colorHex = "#FFD700"; // Gold for Superb
                            break;
                        case 25:
                            colorHex = "#FFA500"; // Orange for Extreme
                            break;
                        case 30:
                            colorHex = "#FF0000"; // Red for Unstoppable
                            break;
                    }
                    break;
                }
            }

            tCombo.text = $"Combo <color={colorHex}>x {combo}</color>";
            tComboExpand.text = tCombo.text;

            // Set color for tComboRating.text
            tComboRating.color = ColorUtility.TryParseHtmlString(colorHex, out Color newColor) ? newColor : Color.white;
            tComboRatingExpand.text = tComboRating.text;

            // Reset scale and alpha for expanding combo text
            tComboExpand.transform.localScale = Vector3.one;
            tComboExpand.alpha = 0.6f;

            // Add a slow expand effect and alpha tween to the expanding combo text
            tComboExpand.transform.DOScale(1.1f, 1f).SetEase(Ease.OutQuad);
            tComboExpand.DOFade(0, 1f).SetEase(Ease.OutQuad);

            int maxKey = comboRatings.Keys.Where(key => key <= combo).DefaultIfEmpty(0).Max();
            tComboRating.text = comboRatings.ContainsKey(maxKey) ? comboRatings[maxKey] : "";
            tComboRatingExpand.text = tComboRating.text;

            // Add a fade-in effect to the expanding combo rating text
            tComboRatingExpand.DOFade(1, 0.5f);

            // Trigger I_ComboRating and I_ComboRating2 only if a new combo rating is achieved\
            if (maxKey > previousMaxComboKey) {
                previousMaxComboKey = maxKey;

                // Show I_ComboRating with scale and alpha tween
                I_ComboRating.sprite = GetComboRatingSprite(maxKey); // Assume this method gets the correct sprite
                I_ComboRating.transform.localScale = Vector3.one;
                I_ComboRating.color = new Color(I_ComboRating.color.r, I_ComboRating.color.g, I_ComboRating.color.b, 0.1f);
                I_ComboRating.transform.DOScale(1.2f, 1.5f).SetEase(Ease.OutQuad);
                I_ComboRating.DOFade(0.7f, 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
                    I_ComboRating.DOFade(0, 1.25f).SetEase(Ease.OutQuad));

                // Show I_ComboRating2 with scale and alpha tween
                I_ComboRating2.sprite = GetComboRatingSprite(maxKey); // Assume this method gets the correct sprite
                I_ComboRating2.transform.localScale = Vector3.one * 1.2f;
                I_ComboRating2.color = new Color(I_ComboRating2.color.r, I_ComboRating2.color.g, I_ComboRating2.color.b, 0.1f);
                I_ComboRating2.transform.DOScale(1.5f, 1f).SetEase(Ease.OutQuad);
                I_ComboRating2.DOFade(0.4f, 0.1f).SetEase(Ease.OutQuad).OnComplete(() =>
                    I_ComboRating2.DOFade(0, 0.2f).SetEase(Ease.OutQuad));
            }
        }
    }

    private Sprite GetComboRatingSprite(int comboKey) {
        foreach (var key in comboRatings.Keys.OrderByDescending(k => k)) {
            if (comboKey >= key) {
                switch (key) {
                    case 5:
                        return iGood;
                    case 10:
                        return iNice;
                    case 15:
                        return iExcellent;
                    case 20:
                        return iSuper;
                    case 25:
                        return iExtreme;
                    case 30:
                        return iUnstoppable;
                }
            }
        }
        return null;
    }

    public void remove_combo() {
        combo = 0;
        previousMaxComboKey = 0;
        tCombo.text = "";
        tCombo.text = "";
        tComboRating.text = "";
        tComboRatingExpand.text = "";
    }
}