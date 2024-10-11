using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprites : MonoBehaviour {

    public static Sprites I;

    public Sprite dummy;

    [Header("----- Portraits: Player -----")]
    public Sprite empty, tommy;
    public Sprite brad, anastasia, seraphine, miguel, anthony, beatrice;

    [Header("----- Sprites for Player Chars -----")]
    public Sprite tommySprite;
    public Sprite bradSprite, anastasiaSprite, seraphineSprite;


    [Header("----- Portraits: NPC / Bosses -----")]
    public Sprite vic;
    public Sprite npcMan1, npcMan2, npcMan3, npcMan4, npcWoman1, npcWoman2, npcWoman3, vincenzo;
    public Sprite giantSlime, assassinCaptain, hobgoblin, mafiaCaptain;

    [Header("----- Icons: Other -----")]
    public Sprite icnHighway;
    public Sprite icnItem;

    [Header("----- Items -----")]
    public Sprite itmGold;
    public Sprite itmBasicSword, itmHomemadeGun, itmLeatherJacket, itmFakeCrossovers, itmMagicStick, itmPendantOfBurningScourge, 
        itmGlovesOfProDriving;

    [Header("----- UI -----")]
    public Sprite btnLocked;
    public Sprite hpBar, hpBarBase, staBar, staBarBase;

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
            { "captain-beatrice", beatrice },
            { "vic", vic },
            { "icn-highway", icnHighway },
            { "icn-item", icnItem },
            { "btn-locked", btnLocked },
            { "empty-icon", empty },

            { "giant-slime", giantSlime },
            { "assassin-captain", assassinCaptain },
            { "hobgoblin", hobgoblin },
            { "mafia-captain", mafiaCaptain },

            { "hp-bar", hpBar },
            { "hp-bar-base", hpBarBase },
            { "sta-bar", staBar },
            { "sta-bar-base", staBarBase },

            { "itm-basic-sword", itmBasicSword },
            { "itm-homemade-gun", itmHomemadeGun },
            { "itm-leather-jacket", itmLeatherJacket },
            { "itm-fake-crossovers", itmFakeCrossovers },
            { "itm-magic-stick", itmMagicStick },
            { "itm-pendant-of-burning-scourge", itmPendantOfBurningScourge },
            { "itm-gloves-of-pro-driving", itmGlovesOfProDriving },
            { "itm-gold", itmGold },
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