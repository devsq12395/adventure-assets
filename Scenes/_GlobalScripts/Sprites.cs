using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour {

    public static Sprites I;
	public void Awake(){ I = this; }

    public Sprite dummy;

    [Header("----- Portraits for Player -----")]
    public Sprite tommy;
    public Sprite brad, anastasia, seraphine, miguel, anthony;

    [Header("----- Portraits: NPC -----")]
    public Sprite vic;
    public Sprite npcMan1, npcMan2, npcMan3, npcMan4, npcWoman1, npcWoman2, vincenzo;

    [Header("----- Icons: Other -----")]
    public Sprite icnHighway;
    public Sprite icnItem;

    [Header("----- UI -----")]
    public Sprite btnLocked;

    [Header("----- Cursor -----")]
    public Sprite cursor;

    void Start (){
        Vector2 hotspot = new Vector2(cursor.texture.width / 2, cursor.texture.height / 2);
        Cursor.SetCursor(cursor.texture, hotspot, CursorMode.ForceSoftware);
    }

    public Sprite get_sprite (string _name) {
        Sprite _ret = dummy;

        switch (_name) {
            case "tommy": return tommy; break;
            case "brad":case "kazuma": return brad; break;
            case "anastasia": return anastasia; break;
            case "seraphine": case "sylphine": return seraphine; break;
            case "miguel": return miguel; break;
            case "anthony": return anthony; break;

            case "npc-man-1": return npcMan1; break;
            case "npc-man-2": return npcMan2; break;
            case "npc-man-3": return npcMan3; break;
            case "npc-man-4": return npcMan4; break;
            case "npc-woman-1": return npcWoman1; break;
            case "npc-woman-2": return npcWoman1; break;

            case "vincenzo": return vincenzo; break;

            case "vic": return vic; break;

            case "icn-highway": return icnHighway; break;
            case "icn-item": return icnItem; break;

            case "btn-locked": return btnLocked; break;
        }

        return _ret;
    }
}