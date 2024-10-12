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

	public string get_random_wave(string _enemiesType) {
	    switch (_enemiesType) {
	        case "cursed-forest":
	            // Return a random wave from the cursed-forest enemies list
	            string[] cursedForestEnemies = { "slime-1", "slime-2", "orc-1", "goblin-1" };
	            System.Random rand = new System.Random();
	            return cursedForestEnemies[rand.Next(cursedForestEnemies.Length)];
	    }
	    return _enemiesType;
	}

	public List<Dictionary<string, int>> get_list_of_main_waves(string _enemiesType) {
	    _enemiesType = get_random_wave(_enemiesType);
	    List<Dictionary<string, int>> _ret = new List<Dictionary<string, int>>();

	    switch (_enemiesType) {
	        case "training-grounds": _ret = training_grounds(_ret); break;
	        case "tutorial": _ret = tutorial_waves(_ret); break;
	        case "slime-1": _ret = slime_1_waves(_ret); break;
	        case "slime-2": _ret = slime_2_waves(_ret); break;
	        case "mafia-1": _ret = mafia_1_waves(_ret); break;
	        case "mafia-2": _ret = mafia_2_waves(_ret); break;
	        case "assassin-1": _ret = assassin_1_waves(_ret); break;
	        case "goblin-1": _ret = goblin_1_waves(_ret); break;
	        case "orc-1": _ret = orc_1_waves(_ret); break;
	        case "beatrice-1": _ret = beatrice_1_waves(_ret); break;
	        case "war-shredder-1": _ret = war_shredder_1_waves(_ret); break;
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
		switch (_waveName) {
			case "slime-orange":
	            int[] offsets = { -6, 6 };

	            foreach (int x in offsets) {
	                ContObj.I.create_obj_spawner(_waveName, new Vector2(x, 0 + 3), 2);
	                ContObj.I.create_obj_spawner(_waveName, new Vector2(0, x + 3), 2);
	            }
	            ContEnemies.I.enemyCount += 4;
	            
	            _isSpecialSpawn = true;
	            break;
		}

		return _isSpecialSpawn;
	}

	private List<Dictionary<string, int>> training_grounds (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mafia-captain", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> slime_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-blue", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("slime-blue", 2);
				_waves[1].Add ("goblin", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("slime-blue", 3);
				_waves[2].Add ("goblin-mage", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("giant-slime", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> slime_2_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-blue", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("slime-blue", 2);
				_waves[1].Add ("goblin", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("goblin-mage", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("hobgoblin", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> mafia_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mobster", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("mobster", 2);
				_waves[1].Add ("assassin", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("slime-blue", 2);
				_waves[2].Add ("mobster", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("mafia-captain", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> mafia_2_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mobster", 3);
				_waves[0].Add ("slime-blue", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("orc", 3);
				_waves[1].Add ("assassin", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("assassin-captain", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> assassin_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("assassin", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("assassin-captain", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> goblin_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("goblin", 5);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("goblin", 3);
				_waves[1].Add ("goblin-mage", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("hobgoblin", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> orc_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("orc", 2);
				_waves[0].Add ("goblin", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("orc", 5);
				_waves[1].Add ("goblin-mage", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("axe-arm-azar", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> beatrice_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("victorian-soldier", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("mobster", 2);
				_waves[1].Add ("victorian-soldier", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("mobster", 2);
				_waves[2].Add ("victorian-soldier", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("cap-beatrice", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> war_shredder_1_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("war-shredder", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("goblin", 4);
				_waves[1].Add ("war-shredder", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("alpha-war-shredder", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> tutorial_waves (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-orange", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("slime-blue", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("slime-blue", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("giant-slime", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> vic_2_main_wave (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("mobster", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("mobster", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("slime-blue", 2);
				_waves[2].Add ("mobster", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("mobster-leader", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> vic_3_main_wave (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("victorian-soldier", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("mobster", 2);
				_waves[1].Add ("victorian-soldier", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("mobster", 2);
				_waves[2].Add ("victorian-soldier", 2);

				_waves.Add (new Dictionary<string, int>());
				_waves[3].Add ("cap-beatrice", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> anthony_1_main_wave (List<Dictionary<string, int>> _ret){
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("war-shredder", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("alpha-war-shredder", 1);

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