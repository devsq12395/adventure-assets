using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc : MonoBehaviour {

    public static StatCalc I;
    public void Awake() { I = this; }

    // Cache dictionary for character stats
    private Dictionary<string, Dictionary<string, int>> characterStatsCache = new Dictionary<string, Dictionary<string, int>>();

    private float cacheClearInterval = 2f; // Clear cache every 2 seconds
    private float lastCacheClearTime;

    // Get the stat of a specific character
    public int get_stat(string _charName, string _stat) {
        if (characterStatsCache.ContainsKey(_charName) && characterStatsCache[_charName].ContainsKey(_stat)) {
            return characterStatsCache[_charName][_stat];
        }

        // If not in cache, calculate all stats and store them
        Dictionary<string, int> allStats = get_all_stats_of_char(_charName);
        if (allStats.ContainsKey(_stat)) {
            return allStats[_stat];
        }

        return 0; // Default value if the stat is not found
    }

    void Update() {
        // Check if 2 seconds have passed since the last cache clear
        if (Time.time - lastCacheClearTime > cacheClearInterval) {
            ClearCache();
            lastCacheClearTime = Time.time; // Update the last cache clear time
        }
    }

    private void ClearCache() {
        characterStatsCache.Clear();
    }

    // Method to calculate and cache all stats of a character
    public Dictionary<string, int> get_all_stats_of_char(string _charName) {
        if (characterStatsCache.ContainsKey(_charName)) {
            return characterStatsCache[_charName]; // Return cached stats if they already exist
        }

        Dictionary<string, int> stats = new Dictionary<string, int>();

        DB_Chars.CharData _charData = DB_Chars.I.get_char_data(_charName);
        List<Inv2.Item> itemsEquipped = Inv2.I.get_equipped_items(_charName);

        foreach (Inv2.Item _itemLoop in itemsEquipped) {
            Debug.Log ($"{_itemLoop.name}, equipped to {_itemLoop.equippedBy}");
        }

        // Calculate base stats and add to the dictionary
        stats["hp"] = _charData.statHP;
        stats["attack"] = _charData.statAtk;
        stats["range"] = _charData.statRange;
        stats["skill"] = _charData.statSkill;
        stats["speed"] = _charData.statSpeed;
        stats["armor"] = _charData.statArmor;
        stats["crit-rate"] = _charData.statCritRate;
        stats["crit-dam"] = _charData.statCritDam;
        stats["science"] = _charData.statScience;
        stats["magic"] = _charData.statMagic;
        stats["driving"] = _charData.statDriving;
        stats["espionage"] = _charData.statEspionage;
        stats["computers"] = _charData.statComputers;
        stats["repair"] = _charData.statRepair;
        stats["luck"] = _charData.statLuck;

        // Add bonuses from equipped items
        for (int i = 0; i < itemsEquipped.Count; i++) {
            Inv2.Item _item = itemsEquipped[i];
            Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data(_item.name);

            stats["hp"] += _data.bonusHP;
            stats["attack"] += _data.bonusATK;
            stats["range"] += _data.bonusRange;
            stats["skill"] += _data.bonusSkill;
            stats["speed"] += _data.bonusSpeed;
            stats["armor"] += _data.bonusArmor;
            stats["crit-rate"] += _data.bonusCritRate;
            stats["crit-dam"] += _data.bonusCritDam;
            stats["science"] += _data.bonusScience;
            stats["magic"] += _data.bonusMagic;
            stats["driving"] += _data.bonusDriving;
            stats["espionage"] += _data.bonusEspionage;
            stats["computers"] += _data.bonusComputers;
            stats["repair"] += _data.bonusRepair;
            stats["luck"] += _data.bonusLuck;
        }

        // Cache the calculated stats for future use
        characterStatsCache[_charName] = stats;

        return stats;
    }
}
