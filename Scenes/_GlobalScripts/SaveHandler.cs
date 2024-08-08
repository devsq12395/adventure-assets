using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public class SaveHandler : MonoBehaviour {
    public static SaveHandler I;
    public void Awake() { 
        I = this; 
        check_save ();
    }

    public void check_save (){
        if (ZPlayerPrefs.GetInt("0.3") == 0) {
            ZPlayerPrefs.SetInt("gold", 0);
        }
    }

    public int gain_gold (int _inc){
        int gold = ZPlayerPrefs.GetInt("Gold");
        gold += _inc;
        ZPlayerPrefs.SetInt("Gold", gold);
        return gold;
    }
}