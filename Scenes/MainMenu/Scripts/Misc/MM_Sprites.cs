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
    public Sprite portEmpty, portTommy;

    public Sprite get_sprite (string _name){
        switch (_name) {
            case "tommy":           return portTommy; break;
            case "anastasia":           return portTommy; break;
            case "seraphine":           return portTommy; break;
            case "miguel":           return portTommy; break;
            case "anthony":           return portTommy; break;

            case "empty":           return portEmpty; break;
            
            default: return dummy;
        }
    }
}
