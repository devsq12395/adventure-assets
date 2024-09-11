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
            ZPlayerPrefs.SetString("missionCurPool.vincenzo", "vincenzo-1");
            ZPlayerPrefs.SetString("missionCurPool.anthony", "anthony-1");
            ZPlayerPrefs.SetString("missionCurPool.mill-river-ives", "mill-river-ives");
            ZPlayerPrefs.SetString("missionCurPool.field-1", "field-1-1");

            PlayerPrefs.SetInt("combat-tut-state", 0);

            PlayerPrefs.SetInt("areasState.new-haven", 1);
            PlayerPrefs.SetInt("areasState.wooster-square-2", 1);
            PlayerPrefs.SetInt("areasState.squaredrive-repairs", 0);
            PlayerPrefs.SetInt("areasState.rosas-diner", 1);
            PlayerPrefs.SetInt("areasState.bella-vita", 0);
            PlayerPrefs.SetInt("areasState.strega", 0);
            PlayerPrefs.SetInt("areasState.marcos-tavern", 0);
            PlayerPrefs.SetInt("areasState.i91-highway-ives", 0);
            PlayerPrefs.SetInt("areasState.east-st-ives", 0);
            PlayerPrefs.SetInt("areasState.mill-river-ives", 0);
            PlayerPrefs.SetInt("areasState.fair-haven-ives", 0);
            PlayerPrefs.SetInt("areasState.east-st-2-ives", 0);
            PlayerPrefs.SetInt("areasState.to-wooster-square-2", 0);
            PlayerPrefs.SetInt("areasState.to-wooster-square-3", 0);

            PlayerPrefs.SetInt("activity.dialog-with-vic", 0);
            PlayerPrefs.SetInt("activity.dialog-with-vincenzo", 0);
            PlayerPrefs.SetInt("activity.dialog-with-anthony", 0);
            PlayerPrefs.SetString("start-node", "1");

            ZPlayerPrefs.SetString("main-menu-start-callback", "show-intro-1");
            ZPlayerPrefs.SetString("main-menu-map", "wooster-square-1");

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