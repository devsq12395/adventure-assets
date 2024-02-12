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
    public Sprite portTommy;

    public Sprite get_sprite (string _name){
        switch (_name) {
            case "tommy":           return portTommy; break;
            
            default: return dummy;
        }
    }
}
