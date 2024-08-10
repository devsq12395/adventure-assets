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
    public void Awake() { I = this;}

    public struct ItemData {
        public string nameUI, desc, equipTo;
        public int cost, stackMax;
        public bool stackable;
        public List<string> tags;
        public Sprite sprite;

        public int bonusHP, bonusATK, bonusRange, bonusSkill, bonusSpeed, bonusArmor, bonusCritRate, bonusCritDam;

        public ItemData (string _name){
            nameUI="";desc="";equipTo="";
            cost=0;stackMax=9999;
            stackable=false;
            tags=new List<string>();

            bonusHP=0;bonusATK=0;bonusRange=0;bonusSkill=0;bonusSpeed=0;bonusArmor=0;bonusCritRate=0;bonusCritDam=0;
            sprite = Sprites.I.get_sprite ("empty");
        }
    }

    public ItemData get_item_data (string _name){
        ItemData _new = new ItemData (_name);
        switch (_name) {
            case "basic-sword":
                _new.nameUI="Basic Sword";
                _new.desc="Made of basic iron. Enough for self defense.";
                _new.equipTo="weapon";
                _new.tags.AddRange(new List<string> { "weapon", "sword" });
                _new.sprite = Sprites.I.get_sprite ("itm-basic-sword");

                _new.bonusHP=0;_new.bonusATK=1;_new.bonusRange=0;_new.bonusSkill=0;_new.bonusSpeed=0;_new.bonusArmor=0;_new.bonusCritRate=0;_new.bonusCritDam=0;
                break;
        }

        return _new;
    }

    public List<string> get_shop_items_sold (string _shopName){
        List<string> _new = new List<string>();

        return _new;
    }
}