using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DB_Enemies : MonoBehaviour {
    public static DB_Enemies I;
	public void Awake(){ I = this; }

	public List<Dictionary<string, int>> get_list_of_main_waves (string _enemiesType){
		List<Dictionary<string, int>> _ret = new List<Dictionary<string, int>> ();

		switch (_enemiesType) {
			case "1-zombies": _ret = zombies_1_main_wave (_ret); break;
		}

		return _ret;
	}

	public Dictionary<string, int> get_reward_chance (string _enemiesType){
		Dictionary<string, int> _ret = new Dictionary<string, int> ();

		switch (_enemiesType) {
			case "1-zombies": _ret = zombies_1_reward_chance (_ret); break;
		}

		return _ret;
	}

	public List<int> get_possible_gold_rewards (string _enemiesType) {
		List<int> _ret = new List<int>();

		switch (_enemiesType) {
			case "1-zombies": _ret.AddRange (new int[]{300, 500, 600}); break;
		}

		return _ret;
	}

	private List<Dictionary<string, int>> zombies_1_main_wave (List<Dictionary<string, int>> _ret){
		int chance = Random.Range (0, 3);
		List<Dictionary<string, int>> _waves = new List<Dictionary<string, int>> ();


		switch (chance) {
			case 0:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("kitsune", 8);
				_waves[0].Add ("orc-shaman", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("kitsune-boss", 1);

				break;
			case 1:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("kitsune", 8);
				_waves[0].Add ("orc-shaman", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("kitsune-boss", 1);

				break;
			case 2:
				_waves.Add (new Dictionary<string, int>());
				_waves[0].Add ("kitsune", 8);
				_waves[0].Add ("orc-shaman", 3);

				_waves.Add (new Dictionary<string, int>());
				_waves[1].Add ("kitsune-boss", 1);

				break;
		}
		
		return _waves;
	}

	private Dictionary<string, int> zombies_1_reward_chance (Dictionary<string, int> _ret){
		_ret.Add ("zomflsh", 20);
		_ret.Add ("plank", 35);
		_ret.Add ("scrap", 50);
		_ret.Add ("irnore", 60);
		_ret.Add ("bluvin", 70);
		_ret.Add ("", 100);

		return _ret;
	}
}