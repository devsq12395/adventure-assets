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

    [Header("----- UI -----")]
    public Sprite btnLocked;

    [Header("----- Cursor -----")]
    public Texture2D cursorTexture;

    void Start (){
        Vector2 hotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    public Sprite get_sprite (string _name) {
        Sprite _ret = dummy;

        switch (_name) {
            case "tommy": return tommy; break;
            case "brad":case "kazuma": return brad; break;
            case "anastasia": return anastasia; break;
            case "seraphine": return seraphine; break;
            case "miguel": return miguel; break;
            case "anthony": return anthony; break;

            case "vic": return vic; break;

            case "btn-locked": return btnLocked; break;
        }

        return _ret;
    }
}