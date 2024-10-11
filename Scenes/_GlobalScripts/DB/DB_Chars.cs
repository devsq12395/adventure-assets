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
        public string name, nameUI, desc, imgPort, equipWeapon;
        public string bioInfo, bioSkill1, bioSkill2;
        public int statHP, statAtk, statRange, statSkill, statSpeed, statArmor, statCritRate, statCritDam;
        public int statScience, statMagic, statDriving, statEspionage, statComputers, statRepair, statLuck;
        public int goldCost;

        public List<string> tags;

        public CharData (string _name){
            name = _name;
            nameUI="";desc="";imgPort="";equipWeapon="";
            bioInfo="";bioSkill1="";bioSkill2="";
            statHP = 0; statAtk = 0; statRange = 0; statSkill = 0; statSpeed = 0;statArmor = 0; statCritRate = 0; statCritDam = 0;
            statScience = 0; statMagic = 0; statDriving = 0; statEspionage = 0; statComputers = 0; statRepair = 0; statLuck = 0;
            goldCost=0;

            tags = new List<string>();
        }
    }

    public CharData get_char_data (string _name){
        CharData _new = new CharData (_name);
        switch (_name) {
            case "tommy":
                _new.nameUI="Tommy";
                _new.desc = "A skilled gunman from Florida seeking his fortunes on the road. His experiences in the criminal underworld gave Tommy a good skillset, making him adept at shooting, driving and espionage.";
                _new.imgPort="tommy";
                _new.equipWeapon="gun";
                _new.statHP = 80; _new.statAtk = 10; _new.statRange = 4; 
                _new.statSkill = 15; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 10; _new.statCritDam = 120;
                _new.statScience = 4; _new.statMagic = 1; _new.statDriving = 7; 
                _new.statEspionage = 4; _new.statComputers = 5; _new.statRepair = 5; _new.statLuck = 3;
                _new.goldCost = 0;
                _new.tags.AddRange (new string[] {"hero", "human", "gunslinger"});

                _new.bioInfo = "Attack Weapon: Gun\nAttack Tags: Gun, Physical\n\n\nCharacter Tags: Hero, Human, Gunslinger";
                _new.bioSkill1 = "Plasma Shotgun: Fires 3 plasma waves that deals damage in their path.\n\nTags: Electric";
                _new.bioSkill2 = "Overcharge: The next 10 attacks will deal 400% damage and will make Tommy's attacks Electric.";
                break;
            case "kazuma":
                _new.nameUI="Alfred";
                _new.desc = "An adventurer who is very skilled with swords, and Tommy's best buddy.";
                _new.imgPort="kazuma";
                _new.equipWeapon="sword";
                _new.statHP = 100; _new.statAtk = 10; _new.statRange = 2; 
                _new.statSkill = 15; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 140;
                _new.statScience = 3; _new.statMagic = 2; _new.statDriving = 5; 
                _new.statEspionage = 2; _new.statComputers = 4; _new.statRepair = 4; _new.statLuck = 5;
                _new.goldCost = 0;
                _new.tags.AddRange (new string[] {"hero", "human", "swordsman"});

                _new.bioInfo = "Attack Weapon: Sword\nAttack Tags: Sword, Physical\n\n\nCharacter Tags: Hero, Human, Swordsman";
                _new.bioSkill1 = "Burning Slash: Slashes forward, dealing skill damage to enemies hit then unleashes a ring of attacks in the end.\n\nTags: fire, magic";
                _new.bioSkill2 = "Destroyer Slash: Unleashes a flame wave. Dealing skill * 3 damage.\n\nTags: fire, magic";
                break;
            case "anastasia":
                _new.nameUI="Anastasia";
                _new.desc = "";
                _new.imgPort="anastasia";
                _new.equipWeapon="sword";
                _new.statHP = 80; _new.statAtk = 12; _new.statRange = 2; 
                _new.statSkill = 12; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 150;
                _new.statScience = 5; _new.statMagic = 5; _new.statDriving = 2; 
                _new.statEspionage = 2; _new.statComputers = 3; _new.statRepair = 3; _new.statLuck = 3;
                _new.goldCost = 500;
                _new.tags.AddRange (new string[] {"hero", "human", "gunslinger", "vampire"});

                _new.bioInfo = "Attack Weapon: Sword\nAttack Tags: sword, magic\n\n\nCharacter Tags: Hero, Human, Vampire, Swordsman";
                _new.bioSkill1 = "Binding Chains: Binds nearby enemies in chains. Dealing skill damage and making enemies immobile.\n\nTags: Magic";
                _new.bioSkill2 = "Void Sphere: Unleashes a powerful void orb. Dealing skill * 3 damage.\n\nTags: Void, Magic";
                break;
            case "sylphine":
                _new.nameUI="Sylphine";
                _new.desc = "";
                _new.imgPort="sylphine";
                _new.equipWeapon="staff";
                _new.statHP = 70; _new.statAtk = 8; _new.statRange = 6; 
                _new.statSkill = 20; _new.statSpeed = 1; _new.statArmor = 0; _new.statCritRate = 15; _new.statCritDam = 140;
                _new.statScience = 7; _new.statMagic = 8; _new.statDriving = 2; 
                _new.statEspionage = 2; _new.statComputers = 5; _new.statRepair = 5; _new.statLuck = 2;
                _new.goldCost = 500;
                _new.tags.AddRange (new string[] {"hero", "human", "mage", "new yale"});

                _new.bioInfo = "Attack Weapon: Staff\nAttaack Tags: ice, magic\n\n\nCharacter Tags: hero, human, mage, new yale";
                _new.bioSkill1 = "Torrent: Unleashes a sequence of strong torrents, dealing skill damage over time.\n\nTags: magic, water";
                _new.bioSkill2 = "Frost Orbs: Creates 4 Frost Orbs that shoots ice shards, dealing damage per hit.\n\nTags: ice, magic";
                break;
        }

        return _new;
    }
}