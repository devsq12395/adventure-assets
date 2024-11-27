using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Inv2_DB : MonoBehaviour {
    public static Inv2_DB I;
    public void Awake() { 
        I = this;
    }

    private Dictionary<string, ItemData> itemDataCache = new Dictionary<string, ItemData>();

    public struct ItemData {
        public string nameUI, desc, details, equipTo;
        public int cost, stackMax;
        public bool stackable;
        public List<string> tags;
        public Sprite sprite;

        public int bonusHP, bonusATK, bonusRange, bonusSkill, bonusSpeed, bonusArmor, bonusCritRate, bonusCritDam;
        public int bonusScience, bonusMagic, bonusDriving, bonusEspionage, bonusComputers, bonusRepair, bonusLuck;

        public ItemData (string _name){
            nameUI="";desc="";details="";equipTo="";
            cost=0;stackMax=9999;
            stackable=false;
            tags=new List<string>();

            bonusHP=0;bonusATK=0;bonusRange=0;bonusSkill=0;bonusSpeed=0;bonusArmor=0;bonusCritRate=0;bonusCritDam=0;
            bonusScience=0;bonusMagic=0;bonusDriving=0;bonusEspionage=0;bonusComputers=0;bonusRepair=0;bonusLuck=0;
            sprite = Sprites.I.get_sprite ("empty");
        }
    }

    public ItemData get_item_data (string _name){
        if (!itemDataCache.TryGetValue(_name, out var _existingItemData)) {
            ItemData _new = new ItemData (_name);
            switch (_name) {
                case "iron-sword":
                    _new.nameUI="Iron Sword";
                    _new.desc="+10 Damage\nA cheaply forged sword. Enough for basic self-defense.";
                    _new.details="Cost: 100 Gold\nTags: weapon, sword";
                    _new.equipTo="weapon";
                    _new.tags.AddRange(new List<string> { "weapon", "sword" });
                    _new.sprite = Sprites.I.get_sprite ("itm-basic-sword");
                    _new.cost = 100;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=10;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;
                case "homemade-gun":
                    _new.nameUI="Homemade Gun";
                    _new.desc="+10 Damage\nCrudely made pistol. Recommended for travelers with no money.";
                    _new.details="Cost: 100 Gold\nTags: weapon, gun";
                    _new.equipTo="weapon";
                    _new.tags.AddRange(new List<string> { "weapon", "gun" });
                    _new.sprite = Sprites.I.get_sprite ("itm-homemade-gun");
                    _new.cost = 100;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=10;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;
                case "leather-jacket":
                    _new.nameUI="Leather Jacket";
                    _new.desc="+20 HP\nA jacket thick enough to block most attacks.";
                    _new.details="Cost: 150 Gold\nTags: armor, jacket";
                    _new.equipTo="armor";
                    _new.tags.AddRange(new List<string> { "armor", "jacket" });
                    _new.sprite = Sprites.I.get_sprite ("itm-leather-jacket");
                    _new.cost = 150;
                    _new.stackable = false;

                    _new.bonusHP=20;_new.bonusATK=0;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;
                case "fake-crossovers":
                    _new.nameUI="Fake Crossovers";
                    _new.desc="+10 skill\nAn imitation of a popular shoe model.";
                    _new.details="Cost: 250 Gold\nTags: boots, shoes, sports";
                    _new.equipTo="boots";
                    _new.tags.AddRange(new List<string> { "boots", "shoes", "sports" });
                    _new.sprite = Sprites.I.get_sprite ("itm-fake-crossovers");
                    _new.cost = 250;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=0;_new.bonusRange=0;_new.bonusSkill=10;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;

                case "magic-stick":
                    _new.nameUI="Magic Stick";
                    _new.desc="+5 Damage\n+15 skill\nA long stick imbued with mana.";
                    _new.details="Cost: 300 Gold\nTags: weapon, staff";
                    _new.equipTo="weapon";
                    _new.tags.AddRange(new List<string> { "weapon", "staff"});
                    _new.sprite = Sprites.I.get_sprite ("itm-magic-stick");
                    _new.cost = 300;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=5;_new.bonusRange=0;_new.bonusSkill=15;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;
                case "pendant-of-burning-scourge": case "ring-of-burning-scourge":
                    _new.nameUI="Pendant of Burning Scourge";
                    _new.desc="+15% damage to damages tagged fire\n+30% damage to enemies tagged plant\n\nSpecifically designed to make forest clearing easy.";
                    _new.details="Cost: 400 Gold\nTags: pendant, fire, enchanted";
                    _new.equipTo="pendant";
                    _new.tags.AddRange(new List<string> { "pendant", "fire", "enchanted"});
                    _new.sprite = Sprites.I.get_sprite ("itm-pendant-of-burning-scourge");
                    _new.cost = 400;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=0;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;
                case "gloves-of-pro-driving":
                    _new.nameUI="Gloves of Pro Driving";
                    _new.desc="+1 to Driving\n\nThey say it improves your driving skills, but it's just a pair of gloves.";
                    _new.details="Cost: 50 Gold\nTags: gloves, armlet, driving";
                    _new.equipTo="armlet";
                    _new.tags.AddRange(new List<string> { "gloves", "armlet", "driving"});
                    _new.sprite = Sprites.I.get_sprite ("itm-gloves-of-pro-driving");
                    _new.cost = 50;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=0;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=1;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;

                case "bb-gun":
                    _new.nameUI="BB Gun";
                    _new.desc="A gun that shoots small plastic pellets.";
                    _new.details="";
                    _new.equipTo="weapon";
                    _new.tags.AddRange(new List<string> { "weapon", "gun" });
                    _new.sprite = Sprites.I.get_sprite ("itm-basic-sword");
                    _new.cost = 50;
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=0;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;

                case "gold":
                    _new.nameUI="Gold";
                    _new.desc="";
                    _new.details="";
                    _new.equipTo="";
                    _new.tags.AddRange(new List<string> { "weapon", "gun" });
                    _new.cost=0;
                    _new.sprite = Sprites.I.get_sprite ("itm-gold");
                    _new.stackable = false;

                    _new.bonusHP=0;_new.bonusATK=1;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                    _new.bonusScience=0;_new.bonusMagic=0;_new.bonusDriving=0;_new.bonusEspionage=0;_new.bonusComputers=0;_new.bonusRepair=0;_new.bonusLuck=0;
                    break;
            }
            itemDataCache[_name] = _new;
            return _new;
        }

        return _existingItemData;
    }

    public List<string> get_shop_items_sold (string _shopName){
        List<string> _new = new List<string>();

        switch (_shopName) {
            case "bryans-armory": 
                _new.AddRange (new string[] { "iron-sword", "homemade-gun", "leather-jacket", "fake-crossovers" });
                break;
            case "michelle-shop": 
                _new.AddRange (new string[] { "magic-stick", "pendant-of-burning-scourge", "gloves-of-pro-driving" });
                break;
        }

        return _new;
    }
}