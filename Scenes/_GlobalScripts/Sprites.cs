using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour {

    public static Sprites I;
	public void Awake(){ I = this; }

    public Sprite dummy;

    [Header("----- Characters for Player -----")]
    public Sprite tommy;
    public Sprite brad, anastasia, seraphine, miguel, anthony;

    [Header("----- Characters: NPC -----")]
    public Sprite vic;

    public Sprite get_sprite (string _name) {
        Sprite _ret = dummy;

        switch (_name) {
            case "tommy": return tommy; break;
            case "brad": return brad; break;
            case "anastasia": return anastasia; break;
            case "seraphine": return seraphine; break;
            case "miguel": return miguel; break;
            case "anthony": return anthony; break;

            case "vic": return vic; break;
        }

        return _ret;
    }
}