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

    	public MissionData (string _name){
    		name = _name; speaker = ""; desc = ""; sprite = ""; rewards = "";
    		
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
    		case "vic-1": 
    			_new.speaker = "Vic";
    			_new.desc = "Some slimes are spotted here in Wooster Square. Word is a Giant Slime is with them too.\n\nShould be easy for you to deal with. This is a chance for you to gain respect around here.";
    			_new.sprite = "vic";
    			_new.rewards = "200 Gold";

    			_new.enemies.AddRange (new string[]{"enem-vic-1", "enem-vic-2", "enem-vic-3"});
    			_new.maps.AddRange (new string[]{"map-vic-1", "map-vic-2", "map-vic-3"});
    			_new.unlocksArea.AddRange (new string[]{"bella-vita", "strega", "marcos-tavern", "wooster-square-house-1", "wooster-square-house-3", "wooster-square-house-5"});
    			_new.missionsSet.AddRange (new string[]{"vic->vic-2"});
    			_new.activitySet.AddRange (new string[]{"dialog-with-vic->2"});

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

    		// case "anthony-1": 
    		// 	_new.speaker = "Anthony";
    		// 	_new.desc = "Let's spar and see if you have what it takes to survive the war of the families here in New Haven.";
    		// 	_new.sprite = "beatrice";
    		// 	_new.rewards = "500 Gold";

    		// 	_new.enemies = "vic-1";
    		// 	_new.maps.AddRange (new string[]{"woosterSquare_rand"});
    		// 	_new.unlocksArea.AddRange (new string[]{});
    		// 	_new.missionsSet.AddRange (new string[]{"anthony->anthony-2"});
    		// 	_new.activitySet.AddRange (new string[]{"dialog-with-vic->2"});

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