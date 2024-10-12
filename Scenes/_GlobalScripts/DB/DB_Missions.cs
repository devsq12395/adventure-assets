using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DB_Missions : MonoBehaviour {
	public static DB_Missions I;
    public void Awake() { I = this;}

    public struct MissionData {
    	public string name, speaker, desc, sprite, rewards;
    	public List<string> maps, unlocksArea, missionsSet, activitySet, enemies;
        public string gameOverSpk_name, gameOverSpk_text, gameOverSpk_img;
        public string gameOverSpk_nameFail, gameOverSpk_textFail, gameOverSpk_imgFail;

        public int goldReward;

    	public MissionData (string _name){
    		name = _name; speaker = ""; desc = ""; sprite = ""; rewards = "";
            gameOverSpk_name = ""; gameOverSpk_text = ""; gameOverSpk_img = "";
            gameOverSpk_nameFail = ""; gameOverSpk_textFail = ""; gameOverSpk_imgFail = "";
    		
            goldReward = 0;

            enemies = new List<string> ();
    		maps = new List<string> ();
    		unlocksArea = new List<string> ();
    		missionsSet = new List<string> ();
    		activitySet = new List<string> ();
    	}
    }

    public MissionData get_mission_data (string _name){
    	MissionData _new = new MissionData (_name);

    	switch (_name) {
            case "training-grounds": 
                _new.speaker = "";
                _new.desc = "";
                _new.sprite = "";
                _new.rewards = "";
                _new.goldReward = 0;

                _new.gameOverSpk_name = "";
                _new.gameOverSpk_text = "";
                _new.gameOverSpk_img = "";

                _new.gameOverSpk_nameFail = "Alfred";
                _new.gameOverSpk_textFail = "They're too strong! Let's retreat for now and fight another day.";
                _new.gameOverSpk_imgFail = "kazuya";

                _new.enemies.AddRange (new string[]{"training-grounds"});
                _new.maps.AddRange (new string[]{"training-grounds"});
                _new.unlocksArea.AddRange (new string[]{});
                _new.missionsSet.AddRange (new string[]{});
                _new.activitySet.AddRange (new string[]{});

                break;

    		case "vic-1": 
    			_new.speaker = "Vic";
    			_new.desc = "Some slimes are spotted here in Wooster Square. Word is a Giant Slime is with them too.\n\nShould be easy for you to deal with. This is a chance for you to gain respect around here.";
    			_new.sprite = "vic";
    			_new.rewards = "200 Gold";
                _new.goldReward = 200;

                _new.gameOverSpk_name = "Vic";
                _new.gameOverSpk_text = "Well done! Boss Vincenzo will be pleased. You should meet with him at Luppino Family Cafe.";
                _new.gameOverSpk_img = "vic";

                _new.gameOverSpk_nameFail = "Alfred";
                _new.gameOverSpk_textFail = "They're too strong! Let's retreat for now and fight another day.";
                _new.gameOverSpk_imgFail = "kazuya";

    			_new.enemies.AddRange (new string[]{"tutorial", "slime-2"});
    			_new.maps.AddRange (new string[]{"map-tutorial", "map-wooster-square-3"});
    			_new.unlocksArea.AddRange (new string[]{"to-wooster-square-2"});
    			_new.missionsSet.AddRange (new string[]{"vic->vic-2"});
    			_new.activitySet.AddRange (new string[]{"dialog-with-vic->2"});

    			break;

            case "vincenzo-1": 
                _new.speaker = "Boss Vincenzo";
                _new.desc = "Rossi Family members are spotted selling narcotics on our turf. Knock out their attack leader, and they will run back to where they came from!";
                _new.sprite = "vincenzo";
                _new.rewards = "300 Gold";
                _new.goldReward = 300;

                _new.gameOverSpk_name = "Boss Vincenzo";
                _new.gameOverSpk_text = "Nice work! That will show those Rossi thugs who they're messing with. I think you should meet Captain Beatrice. Tell her I sent you.";
                _new.gameOverSpk_img = "vincenzo";

                _new.gameOverSpk_nameFail = "Alfred";
                _new.gameOverSpk_textFail = "They're too strong! Let's retreat for now and fight another day.";
                _new.gameOverSpk_imgFail = "kazuya";

                _new.enemies.AddRange (new string[]{"mafia-1", "mafia-2"});
                _new.maps.AddRange (new string[]{"map-wooster-square-4", "map-wooster-square-3"});
                _new.unlocksArea.AddRange (new string[]{"to-wooster-square-3"});
                _new.missionsSet.AddRange (new string[]{"vincenzo->vincenzo-2"});
                _new.activitySet.AddRange (new string[]{"dialog-with-vincenzo->2"});

                break;

            case "field-1": 
                _new.speaker = "Anastasia";
                _new.desc = "We're up against Gun-Arm Azar, leader of the BloodAxe Gang. I suspect he is behind a terrorist attack here in New Haven. Let's knock him our and get some answers.";
                _new.sprite = "anastasia";
                _new.rewards = "500 Gold";
                _new.goldReward = 500;

                _new.gameOverSpk_name = "Anastasia";
                _new.gameOverSpk_text = "Tsk, they got away. No matter, they won't get that far with their injuries.";
                _new.gameOverSpk_img = "anastasia";

                _new.gameOverSpk_nameFail = "Alfred";
                _new.gameOverSpk_textFail = "They're too strong! Let's retreat for now and fight another day.";
                _new.gameOverSpk_imgFail = "kazuya";

                _new.enemies.AddRange (new string[]{"goblin-1", "orc-1"});
                _new.maps.AddRange (new string[]{"woosterSquare_rand", "woosterSquare_rand"});
                _new.unlocksArea.AddRange (new string[]{});
                _new.missionsSet.AddRange (new string[]{"field-1->field-1-2"});
                _new.activitySet.AddRange (new string[]{});

                break;

            case "beatrice-1": 
                _new.speaker = "Captain Beatrice";
                _new.desc = "Let's spar and see if you have what it takes to survive the upcoming war of the families here in New Haven.";
                _new.sprite = "beatrice";
                _new.rewards = "700 Gold";
                _new.goldReward = 700;

                _new.gameOverSpk_name = "Captain Beatrice";
                _new.gameOverSpk_text = "Excellent fight! New Haven could use more fighters like you who will fight on the side of justice.";
                _new.gameOverSpk_img = "beatrice";

                _new.gameOverSpk_nameFail = "Captain Beatrice";
                _new.gameOverSpk_textFail = "Not bad, but you could use some more training. Come back when you are stronger.";
                _new.gameOverSpk_imgFail = "beatrice";

                _new.enemies.AddRange (new string[]{"beatrice-1"});
                _new.maps.AddRange (new string[]{"map-wooster-square-1"});
                _new.unlocksArea.AddRange (new string[]{});
                _new.missionsSet.AddRange (new string[]{"beatrice->beatrice-2"});
                _new.activitySet.AddRange (new string[]{"dialog-with-beatrice->8"});

                break;

            case "cursed-forest": 
                _new.speaker = "Tommy";
                _new.desc = "I heard there are a lot of monsters in this forest. Sounds like a good place to train.";
                _new.sprite = "tommy";
                _new.rewards = "200 Gold";
                _new.goldReward = 200;

                _new.gameOverSpk_name = "Tommy";
                _new.gameOverSpk_text = "Excellent fighting, everyone!";
                _new.gameOverSpk_img = "tommy";

                _new.gameOverSpk_nameFail = "Alfred";
                _new.gameOverSpk_textFail = "They're too strong! Let's retreat for now and fight another day.";
                _new.gameOverSpk_imgFail = "kazuya";

                _new.enemies.AddRange (new string[]{"cursed-forest", "cursed-forest", "cursed-forest"});
                _new.maps.AddRange (new string[]{"woosterSquare_rand", "woosterSquare_rand", "woosterSquare_rand"});
                _new.unlocksArea.AddRange (new string[]{});
                _new.missionsSet.AddRange (new string[]{});
                _new.activitySet.AddRange (new string[]{});

                break;

            case "anthony-1": 
                _new.speaker = "Anthony";
                _new.desc = "War Shredders are spotted nearby. Help me get rid of them and I'll pay you nicely.";
                _new.sprite = "npc-man-1";
                _new.rewards = "500 Gold";
                _new.goldReward = 500;

                _new.gameOverSpk_name = "Anthony";
                _new.gameOverSpk_text = "Nice work! I hope those war shredders don't come back.";
                _new.gameOverSpk_img = "npc-man-1";

                _new.gameOverSpk_nameFail = "Alfred";
                _new.gameOverSpk_textFail = "They're too strong! Let's retreat for now and fight another day.";
                _new.gameOverSpk_imgFail = "kazuya";

                _new.enemies.AddRange (new string[]{"war-shredder-1"});
                _new.maps.AddRange (new string[]{"woosterSquare_rand"});
                _new.unlocksArea.AddRange (new string[]{});
                _new.missionsSet.AddRange (new string[]{"anthony->anthony-2"});
                _new.activitySet.AddRange (new string[]{});

                break;

    		// case "vic-2": 
    		// 	_new.speaker = "Vic";
    		// 	_new.desc = "Rossi Family members are spotted selling narcotics on our turf. Knock out their leader, Luca the Terror, and they will run back to where they came from!";
    		// 	_new.sprite = "vic";
    		// 	_new.rewards = "300 Gold";

    		// 	_new.enemies = "vic-1";
    		// 	_new.maps.AddRange (new string[]{"woosterSquare_rand"});
    		// 	_new.unlocksArea.AddRange (new string[]{"squaredrive-repairs", "mill-river-ives"});
    		// 	_new.missionsSet.AddRange (new string[]{"vic->vic-3"});
    		// 	_new.activitySet.AddRange (new string[]{"dialog-with-vic->4"});

    		// 	break;

    		// case "vic-3": 
    		// 	_new.speaker = "Captain Beatrice";
    		// 	_new.desc = "Let's spar and see if you have what it takes to survive the war of the families here in New Haven.";
    		// 	_new.sprite = "beatrice";
    		// 	_new.rewards = "500 Gold";

    		// 	_new.enemies = "vic-1";
    		// 	_new.maps.AddRange (new string[]{"woosterSquare_rand"});
    		// 	_new.unlocksArea.AddRange (new string[]{});
    		// 	_new.missionsSet.AddRange (new string[]{"vic->vic-4"});
    		// 	_new.activitySet.AddRange (new string[]{"dialog-with-vic->6"});

    		// 	break;

    		// 	break;

    		// case "mill-river-ives": 
    		// 	_new.speaker = "Mill River";
    		// 	_new.desc = "This part of the Mill River has been overtaken by monsters.\n\nTo aspiring adventurers, it's a perfect place to train and find treasures.";
    		// 	_new.sprite = "icn-highway";
    		// 	_new.rewards = "200-500 Gold\n\nChange to get:\nWooden Block, Leather, Blue Glowvine, Electric Shard, Demon Fang";

    		// 	_new.enemies = "mill-river-1";
    		// 	_new.maps.AddRange (new string[]{"woosterSquare_rand"});
    		// 	_new.unlocksArea.AddRange (new string[]{});
    		// 	_new.missionsSet.AddRange (new string[]{});
    		// 	_new.activitySet.AddRange (new string[]{});

    		// 	break;
    	}

        return _new;
    }
}