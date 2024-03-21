using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc : MonoBehaviour {

    public static StatCalc I;
    public void Awake(){ I = this; }

    private readonly string[] equipStrs = {"weapon", "armor", "boots", "accessory1", "accessory2"};

    public int get_stat (string _charName, string _stat) {
        int _val = int.Parse (JsonReading.I.read ("chars", $"chars.{_charName}.stats.{_stat}"));

        foreach (string _equipSlot in equipStrs) {
            string _equipped = JsonSaving.I.load ($"chars.{_charName}.equip.{_equipSlot}");
            if (_equipped != "") {
                int _itemBonus = int.Parse (JsonReading.I.read ("items", $"items.{_equipped}.bonuses.{_stat}"));
                _val += _itemBonus;
            }
        }

        return _val;
    }
}