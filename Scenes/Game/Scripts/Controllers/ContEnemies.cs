
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContEnemies : MonoBehaviour {



	/*
		CONTAINS:
		1. Code to control enemy waves
		2. Locate enemy with arrows
	*/


	/*
		1. Code to control enemy waves
	*/
    public static ContEnemies I;
	public void Awake(){ I = this; }

	public int enemyCount, curWave;

	public List<Dictionary<string, int>> mainWaves;
	public Dictionary<string, int> rewardChance;

	public string enemiesType;

	public void setup (string _enemiesType){
		enemiesType = _enemiesType;

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
		string _ret = "";

		// Gold Reward
		List<int> _goldRewards = DB_Enemies.I.get_possible_gold_rewards (enemiesType);
		int _goldReward = _goldRewards [Random.Range (0, _goldRewards.Count)];

		_ret += $"{JsonReading.I.get_str ("UI-in-game.rewards-gold")}: {_goldReward}";

		JsonSaving.I.gain_gold (_goldReward);

		// Item Rewards
		int _rewardsAmountToGive = Random.Range (5, 10);
		//_ret += $"{JsonReading.I.get_str ("UI-in-game.rewards")}:";
		for (int i = 0; i < _rewardsAmountToGive; i++) {
			int _rewardToGive = Random.Range (0, 100);
			foreach (var entry in rewardChance) {

			}
		}

		return _ret;
	}

	public void start_next_wave (){
		mainWaves.RemoveAt (0);

		if (mainWaves.Count > 0) {
			spawn_enemies ();
		} else {
			GameUI_GameOver.I.show (
                JsonReading.I.get_str ("UI-in-game.mission-success"),
                $"{JsonReading.I.get_str ("UI-in-game.all-enemies-dead")}\n\n{generate_and_give_rewards ()}"
            );
            GameUI_GameOver.I.on_victory ();
		}
	}

	/*
		2. Locate enemy with arrows
	*/
	public GameObject arrowPrefab;
    public Camera mainCamera;
    public float arrowOffset = 20f;

    private List<GameObject> arrows = new List<GameObject>();

    public void update_arrows() {
        InGameObject[] _objects = FindObjectsOfType<InGameObject>();

        foreach (InGameObject _object in _objects) {
        	if (_object.owner != 2) continue;

            Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);

            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1) {
                show_arrow (enemy.transform.position);
            }
            else  {
                destroy_arrow (enemy);
            }
        }
    }

    private void show_arrow(Vector3 enemyPosition) {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(enemyPosition);
        Vector3 arrowPosition = new Vector3(Screen.width * screenPoint.x, Screen.height * screenPoint.y, 0);

        GameObject arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        arrows.Add(arrow);
    }

    private void destroy_arrow(GameObject enemy) {
        GameObject arrowToRemove = arrows.Find(a => a.transform.position == enemy.transform.position);
        if (arrowToRemove != null)
        {
            arrows.Remove(arrowToRemove);
            Destroy(arrowToRemove);
        }
    }
}