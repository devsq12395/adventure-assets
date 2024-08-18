using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DB_Chars : MonoBehaviour {
    public static DB_Chars I;
    public void Awake() { 
        I = this;

        charPool = new List<string>(){ "tommy", "kazuma", "anastasia", "sylphine" };
    }

    public List<string> charPool;

    public struct CharData {
        public string name, nameUI, imgPort, equipWeapon;
        public string bioInfo, bioSkill1, bioSkill2;
        public int statHP, statAtk, statRange, statSkill, statSpeed, statArmor, statCritRate, statCritDam;
        public int statScience, statMagic, statDriving, statEspionage, statComputers, statRepair, statLuck;
        public int goldCost;

        public CharData (string _name){
            name = _name;
            nameUI="";imgPort="";equipWeapon="";
            bioInfo="";bioSkill1="";bioSkill2="";
            statHP = 0; statAtk = 0; statRange = 0; statSkill = 0; statSpeed = 0;statArmor = 0; statCritRate = 0; statCritDam = 0;
            statScience = 0; statMagic = 0; statDriving = 0; statEspionage = 0; statComputers = 0; statRepair = 0; statLuck = 0;
            goldCost=0;
        }
    }

    public CharData get_char_data (string _name){
        CharData _new = new CharData (_name);
        switch (_name) {
            case "tommy":
                _new.nameUI="Tommy";
                _new.imgPort="tommy";
                _new.equipWeapon="gun";
                _new.statHP = 8; _new.statAtk = 1; _new.statRange = 4; 
                _new.statSkill = 1; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 10; _new.statCritDam = 120;
                _new.statScience = 4; _new.statMagic = 1; _new.statDriving = 7; 
                _new.statEspionage = 4; _new.statComputers = 5; _new.statRepair = 5; _new.statLuck = 3;
                _new.goldCost = 0;

                _new.bioInfo = "";
                _new.bioSkill1 = "";
                _new.bioSkill2 = "";
                break;
            case "kazuma":
                _new.nameUI="Alfred";
                _new.imgPort="kazuma";
                _new.equipWeapon="sword";
                _new.statHP = 10; _new.statAtk = 2; _new.statRange = 3; _new.statSkill = 1; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 140;
                _new.statScience = 3; _new.statMagic = 2; _new.statDriving = 5; 
                _new.statEspionage = 2; _new.statComputers = 4; _new.statRepair = 4; _new.statLuck = 5;
                _new.goldCost = 0;

                _new.bioInfo = "";
                _new.bioSkill1 = "";
                _new.bioSkill2 = "";
                break;
            case "anastasia":
                _new.nameUI="Anastasia";
                _new.imgPort="anastasia";
                _new.equipWeapon="sword";
                _new.statHP = 8; _new.statAtk = 2; _new.statRange = 3; _new.statSkill = 1; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 150;
                _new.statScience = 5; _new.statMagic = 5; _new.statDriving = 2; 
                _new.statEspionage = 2; _new.statComputers = 3; _new.statRepair = 3; _new.statLuck = 3;
                _new.goldCost = 500;

                _new.bioInfo = "";
                _new.bioSkill1 = "";
                _new.bioSkill2 = "";
                break;
            case "sylphine":
                _new.nameUI="Sylphine";
                _new.imgPort="sylphine";
                _new.equipWeapon="staff";
                _new.statHP = 6; _new.statAtk = 1; _new.statRange = 6; _new.statSkill = 2; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 140;
                _new.statScience = 7; _new.statMagic = 8; _new.statDriving = 2; 
                _new.statEspionage = 2; _new.statComputers = 5; _new.statRepair = 5; _new.statLuck = 2;
                _new.goldCost = 500;

                _new.bioInfo = "";
                _new.bioSkill1 = "";
                _new.bioSkill2 = "";
                break;
        }

        return _new;
    }
}