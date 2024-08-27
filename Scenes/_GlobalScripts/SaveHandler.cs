using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public class SaveHandler : MonoBehaviour {
    public static SaveHandler I;
    public void Awake() { 
        I = this; 

        ZPlayerPrefs.Initialize("new-horizons", "sqgames789");
        Inv2.I.load_items ();

        check_save ();

        DEV_AUTO_RESET = true;
    }

    public bool DEV_AUTO_RESET;

    public void reset_all_saves (){
        ZPlayerPrefs.SetInt("0.3", 0);
    }

    public void check_save (){
        if (DEV_AUTO_RESET) reset_all_saves ();

        if (ZPlayerPrefs.GetInt("0.3") == 0) {
            ZPlayerPrefs.SetInt("gold", 0);
            ZPlayerPrefs.SetString("date", "jan-2225");

            ZPlayerPrefs.SetString("missionCur", "");
            ZPlayerPrefs.SetString("missionCurPool", "");

            PlayerPrefs.SetString("missionCurPool.vic", "vic-1");
            PlayerPrefs.SetString("missionCurPool.anthony", "anthony-1");
            PlayerPrefs.SetString("missionCurPool.mill-river-ives", "mill-river-ives");

            PlayerPrefs.SetInt("areasUnlocked.new-haven", 1);
            PlayerPrefs.SetInt("areasUnlocked.wooster-square", 1);
            PlayerPrefs.SetInt("areasUnlocked.squaredrive-repairs", 0);
            PlayerPrefs.SetInt("areasUnlocked.rosas-diner", 1);
            PlayerPrefs.SetInt("areasUnlocked.bella-vita", 0);
            PlayerPrefs.SetInt("areasUnlocked.strega", 0);
            PlayerPrefs.SetInt("areasUnlocked.marcos-tavern", 0);
            PlayerPrefs.SetInt("areasUnlocked.i91-highway-ives", 0);
            PlayerPrefs.SetInt("areasUnlocked.east-st-ives", 0);
            PlayerPrefs.SetInt("areasUnlocked.mill-river-ives", 0);
            PlayerPrefs.SetInt("areasUnlocked.fair-haven-ives", 0);
            PlayerPrefs.SetInt("areasUnlocked.east-st-2-ives", 0);

            PlayerPrefs.SetInt("activity.dialog-with-vic", 0);
            PlayerPrefs.SetInt("activity.dialog-with-anthony", 0);
            PlayerPrefs.SetString("start-node", "1");

            ZPlayerPrefs.SetString("main-menu-start-callback", "show-intro-1");
            ZPlayerPrefs.SetString("main-menu-map", "wooster-square");

            ZPlayerPrefs.SetString("lineup-1", "tommy");
            ZPlayerPrefs.SetString("lineup-2", "kazuma");
            ZPlayerPrefs.SetString("lineup-3", "");
            ZPlayerPrefs.SetString("lineup-4", "");

            PlayerPrefs.SetInt("cur-map-lvl", 0);
            PlayerPrefs.SetInt("char-1-hp-perc", 100);
            PlayerPrefs.SetInt("char-2-hp-perc", 100);
            PlayerPrefs.SetInt("char-3-hp-perc", 100);
            PlayerPrefs.SetInt("char-4-hp-perc", 100);

            ZPlayerPrefs.SetInt("charUnlocked.tommy", 1);
            ZPlayerPrefs.SetInt("charUnlocked.kazuma", 1);
            ZPlayerPrefs.SetInt("charUnlocked.anastasia", 0);
            ZPlayerPrefs.SetInt("charUnlocked.sylphine", 0);


            int itemCount = ZPlayerPrefs.GetInt("ItemCount", 0);
            if (itemCount > 0) {
                for (int i = 0; i < itemCount; i++) {
                    ZPlayerPrefs.SetString($"Item_{i}_name", "");
                    ZPlayerPrefs.SetInt($"Item_{i}_stack", 0);
                    ZPlayerPrefs.SetInt($"Item_{i}_ID", 0);
                }
            }
            ZPlayerPrefs.SetInt("ItemCount", 0);
        }
    }

    public int gain_gold (int _inc){
        int gold = ZPlayerPrefs.GetInt("gold");
        gold += _inc;
        ZPlayerPrefs.SetInt("gold", gold);
        return gold;
    }
}