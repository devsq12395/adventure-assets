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