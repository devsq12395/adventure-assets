using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public class DB_Strings : MonoBehaviour {
    public static DB_Strings I;
    public void Awake() { I = this;}

    public string get_string(string _str)
    {
        switch (_str)
        {
            case "date": return "Date: ";
            case "jan-2225": return "Jan 2225";

            case "name": return "Name: ";
            case "lvl": return "Lvl";

            case "hp": return "HP";
            case "mp": return "MP";
            case "attack": return "Attack";
            case "skill": return "Skill";
            case "speed": return "Speed";
            case "armor": return "Armor";
            case "crit-dam": return "Crit Dam";
            case "crit-rate": return "Crit Rate";

            case "gold": return "Gold";

            case "requires": return "Requires";

            case "UI-main-menu-equip": return "Equip";
            case "UI-main-menu-buy": return "Buy";
            case "UI-main-menu-sell": return "Sell";
            case "UI-main-menu-check-equipped": return "Change Equipped";

            case "UI-main-menu-reputation": return "Reputation";
            case "UI-main-menu-stranger": return "Stranger";

            case "UI-in-game-mission-failed": return "MISSION FAILED!";
            case "UI-in-game-all-chars-dead": return "Your team is wiped out!";

            case "UI-in-game-mission-success": return "MISSION SUCCESS!";
            case "UI-in-game-all-enemies-dead": return "All enemies are beaten!";

            case "rewards-gold": return "Gold Gained: ";
            case "rewards-item": return "You Found: ";

            case "char-names-kitsune-boss": return "Kitsune Boss";
            case "tommy-name": return "Tommy";
            case "tommy-name-full": return "Tommy Carter";

            case "kazuma-name": return "Alfred";
            case "kazuma-name-full": return "Alfred";

            case "anastasia-name": return "Anastasia";
            case "anastasia-name-full": return "Anastasia";

            case "sylphine-name": return "Sylphine";
            case "sylphine-name-full": return "Sylphine";

            case "miguel-name": return "Miguel";
            case "miguel-name-full": return "Miguel";

            case "anthony-name": return "Anthony";
            case "anthony-name-full": return "Anthony";

            case "new-haven-name": return "New Haven";
            case "new-haven-welcome": return "Welcome to New Haven!";

            default: return "";
        }
    }

}