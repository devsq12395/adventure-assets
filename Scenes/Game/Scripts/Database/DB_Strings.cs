using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Strings : MonoBehaviour {

    public static DB_Strings I;
	public void Awake(){ I = this; }

    public string get_str (string _str){
        switch (_str) {
            case "Overload!": return "Overload!";
            case "Binded!": return "Binded!";
            default: return "";
        }
    }
}
