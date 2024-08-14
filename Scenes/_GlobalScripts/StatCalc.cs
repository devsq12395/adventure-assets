using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatCalc : MonoBehaviour {

    public static StatCalc I;
    public void Awake(){ I = this; }

    public int get_stat (string _charName, string _stat) { Debug.Log (_charName);
        List<Inv2.Item> itemsEquipped = Inv2.I.get_equipped_items (_charName);
        int _ret = 0;

        DB_Chars.CharData _charData = DB_Chars.I.get_char_data (_charName);

        switch (_stat) {
            case "hp":                      _ret += _charData.statHP; break;
            case "attack":                  _ret += _charData.statAtk; break;
            case "range":                   _ret += _charData.statRange; break;
            case "skill":                   _ret += _charData.statSkill; break;
            case "speed":                   _ret += _charData.statSpeed; break;
            case "armor":                   _ret += _charData.statArmor; break;
            case "crit-rate":               _ret += _charData.statCritRate; break;
            case "crit-dam":                _ret += _charData.statCritDam; break;
        }

        itemsEquipped.ForEach ((_item) => {
            Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_item.name);

            switch (_stat) {
                case "hp":                      _ret += _data.bonusHP; break;
                case "attack":                  _ret += _data.bonusATK; break;
                case "range":                   _ret += _data.bonusRange; break;
                case "skill":                   _ret += _data.bonusSkill; break;
                case "speed":                   _ret += _data.bonusSpeed; break;
                case "armor":                   _ret += _data.bonusArmor; break;
                case "crit-rate":               _ret += _data.bonusCritRate; break;
                case "crit-dam":                _ret += _data.bonusCritDam; break;
            }
        });

        return _ret;
    }
}