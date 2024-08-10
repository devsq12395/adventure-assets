using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DB_Chars : MonoBehaviour {
    public static DB_Chars I;
    public void Awake() { I = this;}

    public struct CharData {
        public string name, nameUI, imgPort, equipWeapon;
        public string bioInfo, bioSkill1, bioSkill2;
        public int statHP, statAtk, statRange, statSkill, statSpeed, statArmor, statCritRate, statCritDam;
        public int goldCost;

        public CharData (string _name){
            name = _name;
            nameUI="";imgPort="";equipWeapon="";
            bioInfo="";bioSkill1="";bioSkill2="";
            statHP = 0; statAtk = 0; statRange = 0; statSkill = 0; statSpeed = 0;statArmor = 0; statCritRate = 0; statCritDam = 0;
            goldCost=0;
        }
    }

    public CharData get_char_data (string _name){
        CharData _new = new CharData (_name);
        switch (_name) {
            case "tommy":
                _new.nameUI="Tommy";
                _new.imgPort="tommy.";
                _new.equipWeapon="gun";
                _new.statHP = 8; _new.statAtk = 1; _new.statRange = 4; _new.statSkill = 1; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 10; _new.statCritDam = 120;
                _new.goldCost = 0;
                break;
            case "kazuma":
                _new.nameUI="Alfred";
                _new.imgPort="kazuma.";
                _new.equipWeapon="sword";
                _new.statHP = 10; _new.statAtk = 2; _new.statRange = 3; _new.statSkill = 1; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 140;
                _new.goldCost = 0;
                break;
            case "anastasia":
                _new.nameUI="Anastasia";
                _new.imgPort="anastasia.";
                _new.equipWeapon="sword";
                _new.statHP = 8; _new.statAtk = 2; _new.statRange = 3; _new.statSkill = 1; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 150;
                _new.goldCost = 500;
                break;
            case "sylphine":
                _new.nameUI="Sylphine";
                _new.imgPort="sylphine.";
                _new.equipWeapon="staff";
                _new.statHP = 6; _new.statAtk = 1; _new.statRange = 6; _new.statSkill = 2; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 140;
                _new.goldCost = 500;
                break;
        }

        return _new;
    }
}