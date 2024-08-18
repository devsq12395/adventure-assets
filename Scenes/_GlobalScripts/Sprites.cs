using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour {

    public static Sprites I;

    public Sprite dummy;

    [Header("----- Portraits for Player Chars -----")]
    public Sprite empty, tommy;
    public Sprite brad, anastasia, seraphine, miguel, anthony, beatrice;

    [Header("----- Sprites for Player Chars -----")]
    public Sprite tommySprite;
    public Sprite bradSprite, anastasiaSprite, seraphineSprite;


    [Header("----- Portraits: NPC -----")]
    public Sprite vic;
    public Sprite npcMan1, npcMan2, npcMan3, npcMan4, npcWoman1, npcWoman2, npcWoman3, vincenzo;

    [Header("----- Icons: Other -----")]
    public Sprite icnHighway;
    public Sprite icnItem;

    [Header("----- Items -----")]
    public Sprite itmBasicSword;

    [Header("----- UI -----")]
    public Sprite btnLocked, emptyIcon;

    [Header("----- Cursor -----")]
    public Sprite cursor;

    private Dictionary<string, Sprite> spriteMap;

    public void Awake(){ 
        I = this; 
        spriteMap = new Dictionary<string, Sprite> {
            { "empty", empty },
            { "tommy", tommy },
            { "brad", brad },
            { "kazuma", brad }, // Shared sprite
            { "anastasia", anastasia },
            { "seraphine", seraphine },
            { "sylphine", seraphine }, // Shared sprite
            { "miguel", miguel },
            { "anthony", anthony },
            { "tommy-sprite", tommySprite },
            { "kazuma-sprite", bradSprite },
            { "anastasia-sprite", anastasiaSprite },
            { "sylphine-sprite", seraphineSprite },
            { "npc-man-1", npcMan1 },
            { "npc-man-2", npcMan2 },
            { "npc-man-3", npcMan3 },
            { "npc-man-4", npcMan4 },
            { "npc-woman-1", npcWoman1 },
            { "npc-woman-2", npcWoman2 },
            { "npc-woman-3", npcWoman3 },
            { "vincenzo", vincenzo },
            { "beatrice", beatrice },
            { "vic", vic },
            { "icn-highway", icnHighway },
            { "icn-item", icnItem },
            { "btn-locked", btnLocked },
            { "itm-basic-sword", itmBasicSword },
            { "empty-icon", emptyIcon }
        };
    }

    void Start (){
        Vector2 hotspot = new Vector2(cursor.texture.width / 2, cursor.texture.height / 2);
        Cursor.SetCursor(cursor.texture, hotspot, CursorMode.ForceSoftware);
    }

    public Sprite get_sprite (string _name) {
        return spriteMap.TryGetValue(_name, out var sprite) ? sprite : dummy;
    }
}