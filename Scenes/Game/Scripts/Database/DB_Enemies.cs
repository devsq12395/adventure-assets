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

	public List<Dictionary<string, int>> get_list_of_main_waves (string _enemiesType){
		List<Dictionary<string, int>> _ret = new List<Dictionary<string, int>> ();

		switch (_enemiesType) {
			case "vic-1": _ret = vic_1_main_wave (_ret); break;
			case "vic-2": _ret = vic_2_main_wave (_ret); break;
			case "vic-3": _ret = vic_3_main_wave (_ret); break;

			case "anthony-1": _ret = anthony_1_main_wave (_ret); break;

			case "mill-river-1": _ret = mill_river_1_main_wave (_ret); break;
		}

		return _ret;
	}

	public Dictionary<string, int> get_reward_chance (string _enemiesType){
		Dictionary<string, int> _ret = new Dictionary<string, int> ();

		switch (_enemiesType) {
			case "vic-1": _ret = vic_1_reward_chance (_ret); break;
			case "vic-2": _ret = vic_2_reward_chance (_ret); break;
			case "vic-3": _ret = vic_3_reward_chance (_ret); break;

			case "anthony-1": _ret = anthony_1_reward_chance (_ret); break;

			case "mill-river-1": _ret = mill_river_1_reward_chance (_ret); break;
		}

		return _ret;
	}

	public List<int> get_possible_gold_rewards (string _enemiesType) {
		List<int> _ret = new List<int>();

		switch (_enemiesType) {
			case "vic-1": _ret.AddRange (new int[]{200}); break;
			case "vic-2": _ret.AddRange (new int[]{300}); break;
			case "vic-3": _ret.AddRange (new int[]{500}); break;

			case "anthony-1": _ret.AddRange (new int[]{500}); break;

			case "mill-river-1":_ret.AddRange (new int[]{0}); break;
		}

		return _ret;
	}

	private List<Dictionary<string, int>> vic_1_main_wave (List<Dictionary<string, int>> _ret){
		//int chance = Random.Range (0, 3);
		int chance = 0;
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-red", 4);
				_waves[0].Add ("slime-blue", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("slime-red", 7);
				_waves[1].Add ("slime-blue", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("slime-red", 8);
				_waves[2].Add ("giant-slime", 1);

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
				_waves[0].Add ("slime-red", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("mobster", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("mobster", 2);
				_waves[2].Add ("luca-the-terror", 1);

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
				_waves[0].Add ("centurion", 1);
				_waves[0].Add ("victorian-soldier", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("cap-beatrice", 1);

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
				_waves[0].Add ("war-shredder", 7);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("alpha-war-shredder", 1);

				break;
		}
		
		return _waves;
	}
	private List<Dictionary<string, int>> mill_river_1_main_wave (List<Dictionary<string, int>> _ret){
		int chance = Random.Range (0, 3);
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();

		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("slime-red", 4);
				_waves[0].Add ("slime-blue", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("slime-red", 7);
				_waves[1].Add ("slime-blue", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("alpha-war-shredder", 1);

				break;
			case 1:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("war-shredder", 4);
				_waves[0].Add ("slime-blue", 1);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("war-shredder", 7);
				_waves[1].Add ("mobster", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("giant-slime", 1);

				break;
			case 2:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("war-shredder", 5);
				_waves[0].Add ("embraced-infantry", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("war-shredder", 6);
				_waves[1].Add ("embraced-mage", 4);

				_waves.Add (new Dictionary<string, int>());
				_waves[2].Add ("luca-the-terror", 1);

				break;
		}
		
		return _waves;
	}

	private Dictionary<string, int> vic_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("cprwr", 40);	// Copper Wire
		_ret.Add ("plsbtl", 70); // Plastic Bottle
		_ret.Add ("wldbry", 80); // Wild Berry
		_ret.Add ("coal", 90); // Coal
		_ret.Add ("scrap", 100); // Scrap Metal

		return _ret;
	}
	private Dictionary<string, int> vic_2_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("cprwr", 40);	// Copper Wire
		_ret.Add ("plsbtl", 70); // Plastic Bottle
		_ret.Add ("wldbry", 80); // Wild Berry
		_ret.Add ("coal", 90); // Coal
		_ret.Add ("scrap", 100); // Scrap Metal
		
		return _ret;
	}
	private Dictionary<string, int> vic_3_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("cprwr", 40);	// Copper Wire
		_ret.Add ("plsbtl", 70); // Plastic Bottle
		_ret.Add ("wldbry", 80); // Wild Berry
		_ret.Add ("coal", 90); // Coal
		_ret.Add ("scrap", 100); // Scrap Metal

		return _ret;
	}

	private Dictionary<string, int> anthony_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("cprwr", 40);	// Copper Wire
		_ret.Add ("plsbtl", 70); // Plastic Bottle
		_ret.Add ("wldbry", 80); // Wild Berry
		_ret.Add ("coal", 90); // Coal
		_ret.Add ("scrap", 100); // Scrap Metal

		return _ret;
	}

	private Dictionary<string, int> mill_river_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("wdblk", 40);	// Wooden Block
		_ret.Add ("lthr", 70); // Leather
		_ret.Add ("bluvin", 80); // Blue Glowvine
		_ret.Add ("elcshd", 90); // Electric Shard
		_ret.Add ("demfng", 100); // Demon Fang

		return _ret;
	}
}