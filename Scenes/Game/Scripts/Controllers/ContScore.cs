using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ContScore : MonoBehaviour {

    public static ContScore I;
    public void Awake(){ I = this; }

    public TextMeshProUGUI tScore, tCombo, tComboRating;

    public int combo, comboRating, score, level;
    public Dictionary<int, string> comboRatings;
    public Dictionary<int, int> levels;

    public void setup (){
        define_combo_ratings();
        define_levels();

        tCombo.text = "";
        tComboRating.text = "";
    }

    public void define_combo_ratings (){
        comboRatings = new Dictionary<int, string>(){
            { 5, "Nice!" },
            { 10, "Excellent!" },
            { 15, "Super!" },
            { 20, "Extreme!" },
            { 25, "Unstoppable!" }
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
    }

    public void add_combo(int _increment) {
        combo += _increment;
        if (combo >= 2) {
            tCombo.text = "Combo: " + combo;
            tComboRating.text = "Combo: " + combo;

            int maxKey = comboRatings.Keys.Where(key => key <= combo).DefaultIfEmpty(0).Max();
            tComboRating.text = comboRatings.ContainsKey(maxKey) ? comboRatings[maxKey] : "";
        }
    }

    public void remove_combo() {
        combo = 0;
        tCombo.text = "";
        tComboRating.text = "";
    }
}