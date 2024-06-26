using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Sprites : MonoBehaviour {
    public static MM_Sprites I;
	public void Awake(){ I = this; }

    public Sprite dummy;

    [Header("------ CHAR PORTRAITS ------")]
    public Sprite portEmpty;
    public Sprite portTommy, portAnastasia, portSeraphine, portMiguel, portAnthony;

    [Header("------ MISSION PORTRAITS ------")]
    public Sprite misCaleb01;

    public Sprite get_sprite (string _name){
        switch (_name) {
            case "tommy":           return portTommy; break;
            case "anastasia":           return portAnastasia; break;
            case "seraphine":case "sylphine":           return portSeraphine; break;
            case "miguel":           return portMiguel; break;
            case "anthony":           return portAnthony; break;

            case "caleb01":           return misCaleb01; break;

            case "empty":           return portEmpty; break;
            
            default: return dummy;
        }
    }
}
