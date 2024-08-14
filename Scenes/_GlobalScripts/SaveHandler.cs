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

            ZPlayerPrefs.SetString("missionCurPool.vic", "vic-1");
            ZPlayerPrefs.SetString("missionCurPool.anthony", "anthony-1");
            ZPlayerPrefs.SetString("missionCurPool.mill-river-ives", "mill-river-ives");

            ZPlayerPrefs.SetInt("areasUnlocked.new-haven", 1);
            ZPlayerPrefs.SetInt("areasUnlocked.wooster-square", 1);
            ZPlayerPrefs.SetInt("areasUnlocked.squaredrive-repairs", 1);
            ZPlayerPrefs.SetInt("areasUnlocked.rosas-diner", 1);
            ZPlayerPrefs.SetInt("areasUnlocked.bella-vita", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.strega", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.marcos-tavern", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.i91-highway-ives", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.east-st-ives", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.mill-river-ives", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.fair-haven-ives", 0);
            ZPlayerPrefs.SetInt("areasUnlocked.east-st-2-ives", 0);

            ZPlayerPrefs.SetInt("activity.dialog-with-vic", 0);
            ZPlayerPrefs.SetInt("activity.dialog-with-anthony", 0);

            ZPlayerPrefs.SetString("main-menu-start-callback", "");
            ZPlayerPrefs.SetString("main-menu-map", "wooster-square");

            ZPlayerPrefs.SetString("lineup-1", "tommy");
            ZPlayerPrefs.SetString("lineup-2", "kazuma");
            ZPlayerPrefs.SetString("lineup-3", "");
            ZPlayerPrefs.SetString("lineup-4", "");

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