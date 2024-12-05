using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DB_Enemies : MonoBehaviour {
    public static DB_Enemies I;
	public void Awake(){ I = this; }

	public bool is_enemy_swarm (string _enemyType) {
		switch (_enemyType) {
			case "kitsune": return true; break;
		}

		return false;
	}

	public string get_random_group(string _enemiesType) {
	    switch (_enemiesType) {
	        case "cursed-forest":
	            // Return a random wave from the cursed-forest enemies list
	            string[] cursedForestEnemies = { "slime-1", "slime-2", "orc-1", "goblin-1" };
	            System.Random rand = new System.Random();
	            return cursedForestEnemies[rand.Next(cursedForestEnemies.Length)];
	    }
	    return _enemiesType;
	}

	public List<Dictionary<string, int>> get_enemy_group(string _mission) {
	    List<Dictionary<string, int>> _ret = new List<Dictionary<string, int>>();

	    DB_Missions.MissionData _missionData = DB_Missions.I.get_mission_data (_mission);
	    string _randomEnemyGroup = _missionData.enemies [UnityEngine.Random.Range (0, _missionData.enemies.Count)];

	    switch (_randomEnemyGroup) {
	        case "training-grounds": _ret = training_grounds(_ret); break;
	        case "slime-lvl-1": _ret = slime_lvl_1_waves(_ret); break;
	        case "mafia-lvl-1": _ret = mafia_lvl_1_waves(_ret); break;
			case "orc-lvl-1": _ret = orc_lvl_1_waves(_ret); break;
			case "war-shredder-lvl-1": _ret = war_shredder_lvl_1_waves(_ret); break;
	    }

	    return _ret;
	}


	public Dictionary<string, int> get_reward_chance (string _enemiesType){
		Dictionary<string, int> _ret = new Dictionary<string, int> ();

		switch (_enemiesType) {
			case "enem-vic-1": _ret = vic_1_reward_chance (_ret); break;
			case "enem-vic-2": _ret = vic_2_reward_chance (_ret); break;
			case "enem-vic-3": _ret = vic_3_reward_chance (_ret); break;

			case "anthony-1": _ret = anthony_1_reward_chance (_ret); break;

			case "mill-river-1": _ret = mill_river_1_reward_chance (_ret); break;
		}

		return _ret;
	}

	public bool check_special_spawn (string _waveName){
		bool _isSpecialSpawn = false;
		// switch (_waveName) {
		// 	case "slime-orange":
	    //         int[] offsets = { -6, 6 };

	    //         foreach (int x in offsets) {
	    //             ContObj.I.create_obj_spawner(_waveName, new Vector2(x, 0 + 3), 2);
	    //             ContObj.I.create_obj_spawner(_waveName, new Vector2(0, x + 3), 2);
	    //         }
	            
	    //         _isSpecialSpawn = true;
	    //         break;
		// }

		return _isSpecialSpawn;
	}

	private List<Dictionary<string, int>> training_grounds (List<Dictionary<string, int>> _ret){
		int chance = UnityEngine.Random.Range (0, 3);
		List<Dictionary<string, int>> _levels = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_levels.Add (new Dictionary<string, int>());
				_levels[0].Add ("slime-blue", 3);
				_levels[0].Add ("slime-orange", 5);
				_levels[0].Add ("goblin", 3);
				_levels[0].Add ("skeleton-axethrower", 1);

				break;
			case 1:
				_levels.Add (new Dictionary<string, int>());
				_levels[1].Add ("slime-blue", 5);
				_levels[1].Add ("slime-orange", 10);
				_levels[1].Add ("orc", 3);
				_levels[1].Add ("skeleton-axethrower", 3);

				break;
			default:
				_levels.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-blue", 10);
				_waves[0].Add ("slime-orange", 15);
				_waves[0].Add ("goblin", 6);
				_waves[0].Add ("goblin-mage", 1);
				_waves[0].Add ("war-shredder", 3);

				break;
		}

		return _waves;
	}
	private List<Dictionary<string, int>> slime_lvl_1_waves (List<Dictionary<string, int>> _ret){
		int chance = UnityEngine.Random.Range (0, 3);
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-blue", 7);
				_waves[0].Add ("slime-orange", 15);
				_waves[0].Add ("goblin", 5);
				_waves[0].Add ("goblin-mage", 1);
				_waves[0].Add ("skeleton-axethrower", 3);

				break;
			case 1:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-blue", 10);
				_waves[0].Add ("slime-orange", 5);
				_waves[0].Add ("orc", 3);
				_waves[0].Add ("skeleton-axethrower", 3);

				break;
			default:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-blue", 10);
				_waves[0].Add ("slime-orange", 15);
				_waves[0].Add ("goblin", 6);
				_waves[0].Add ("goblin-mage", 1);
				_waves[0].Add ("war-shredder", 3);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> mafia_lvl_1_waves (List<Dictionary<string, int>> _ret){
		int chance = UnityEngine.Random.Range (0, 3);
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mobster", 7);
				_waves[0].Add ("assassin", 3);
				_waves[0].Add ("slime-orange", 15);

				break;
			case 1:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mobster", 11);
				_waves[0].Add ("skeleton-axethrower", 5);
				_waves[0].Add ("slime-orange", 15);

				break;
			default:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mobster", 11);
				_waves[0].Add ("orc", 4);
				_waves[0].Add ("goblin", 4);
				_waves[0].Add ("slime-orange", 10);
				_waves[0].Add ("war-shredder", 3);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> orc_lvl_1_waves (List<Dictionary<string, int>> _ret){
		int chance = UnityEngine.Random.Range (0, 2);
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("goblin", 9);
				_waves[0].Add ("orc", 5);
				_waves[0].Add ("slime-orange", 5);
				_waves[0].Add ("goblin-mage", 2);

				break;
			default:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("goblin", 9);
				_waves[0].Add ("skeleton-axethrower", 5);
				_waves[0].Add ("slime-orange", 10);
				_waves[0].Add ("war-shredder", 3);
				_waves[0].Add ("goblin-mage", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> war_shredder_lvl_1_waves (List<Dictionary<string, int>> _ret){
		int chance = UnityEngine.Random.Range (0, 2);
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("goblin", 9);
				_waves[0].Add ("orc", 5);
				_waves[0].Add ("slime-orange", 5);
				_waves[0].Add ("war-shredder", 10);

				break;
			default:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("goblin", 9);
				_waves[0].Add ("skeleton-axethrower", 5);
				_waves[0].Add ("war-shredder", 10);
				_waves[0].Add ("goblin-mage", 1);

				break;
		}
		
		return _waves;
	}

	private Dictionary<string, int> vic_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("basic-sword", 50);	// Copper Wire
		_ret.Add ("basic-gun", 50); // Plastic Bottle

		return _ret;
	}
	private Dictionary<string, int> vic_2_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("cprwr", 40);	// Copper Wire
		_ret.Add ("plsbtl", 70); // Plastic Bottle
		_ret.Add ("wldbry", 80); // Wild Berry
		_ret.Add ("scrap", 100); // Scrap Metal
		
		return _ret;
	}
	private Dictionary<string, int> vic_3_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("", 100);	

		return _ret;
	}

	private Dictionary<string, int> anthony_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("", 100);	

		return _ret;
	}

	private Dictionary<string, int> mill_river_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("", 100);	

		return _ret;
	}
}