
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContEnemies : MonoBehaviour {

    public static ContEnemies I;
	public void Awake(){ I = this; }

	public int enemyCount, curWave;

	public List<Dictionary<string, int>> mainWaves;
	public Dictionary<string, int> rewardChance;

	public void setup (string _enemiesType){
		mainWaves = DB_Enemies.I.get_list_of_main_waves (_enemiesType);
		rewardChance = DB_Enemies.I.get_reward_chance (_enemiesType);
	}

	public void spawn_enemies (){
		Dictionary<string, int> _wave = mainWaves [0];

		foreach (var _waveData in _wave){
			for (int i = 0; i < _waveData.Value; i++) {
				Vector2 _rand = new Vector2 (
	                Random.Range (-ContMap.I.details.size.x, ContMap.I.details.size.x),
	                Random.Range (-ContMap.I.details.size.y, ContMap.I.details.size.y)
	            );

	            ContObj.I.create_obj (_waveData.Key, _rand, 2);
	            enemyCount++;
			}
        }
	}

	public string generate_and_give_rewards (){
		int _rewardsAmountToGive = Random.Range (5, 10);
		string _ret = JsonReading.I.get_str ("UI-in-game.rewards");

		for (int i = 0; i < _rewardsAmountToGive; i++) {

		}

		return "";
	}

	public void start_next_wave (){ Debug.Log (mainWaves.Count);
		mainWaves.RemoveAt (0);

		if (mainWaves.Count > 0) {
			spawn_enemies ();
		} else {
			GameUI_GameOver.I.show (
                JsonReading.I.get_str ("UI-in-game.mission-success"),
                $"{JsonReading.I.get_str ("UI-in-game.all-enemies-dead")}\n\n{generate_and_give_rewards ()}"
            );
		}
	}
}