using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc : MonoBehaviour {

    public static StatCalc I;
    public void Awake(){ I = this; }

    public int get_stat (string _charName, string _stat) {
        List<Inv2.Item> itemsEquipped = Inv2.I.get_equipped_items (_charName);
        int _ret = 0;

        itemsEquipped.ForEach ((_item) => {
            Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_item.name);

            switch (_stat) {
                case "hp": _ret += _data.bonusHP; break;
                case "atk": _ret += _data.bonusATK; break;
                case "range": _ret += _data.bonusRange; break;
                case "skill": _ret += _data.bonusSkill; break;
                case "speed": _ret += _data.bonusSpeed; break;
                case "armor": _ret += _data.bonusArmor; break;
                case "crit-rate": _ret += _data.bonusCritRate; break;
                case "crit-dam": _ret += _data.bonusCritDam; break;
            }
        });

        return _ret;
    }
}