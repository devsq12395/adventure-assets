
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
			bool _isSwarm = DB_Enemies.I.is_enemy_swarm (_waveData.Key);

			if (!DB_Enemies.I.check_special_spawn (_waveData.Key)) {
				Vector2 _rand = get_spawn_point ();

				for (int i = 0; i < _waveData.Value; i++) {
					_rand = get_spawn_point ();

		            ContObj.I.create_obj_spawner (_waveData.Key, _rand, 2);
		            enemyCount++;
				}
			}
        }
	}

	private Vector2 get_spawn_point() {
	    Vector2 randomPoint;
	    float maxDistance = 7f;

	    do {
	        randomPoint = new Vector2(
	            Random.Range(-ContMap.I.details.size.x, ContMap.I.details.size.x),
	            Random.Range(-ContMap.I.details.size.y, ContMap.I.details.size.y)
	        );
	    } while (Vector2.Distance(randomPoint, ContPlayer.I.player.gameObject.transform.position) <= maxDistance);

	    return randomPoint;
	}

	public Dictionary<string, int> generate_and_give_rewards() {
		string _curMission = ZPlayerPrefs.GetString("missionCur");
		DB_Missions.MissionData _misData = DB_Missions.I.get_mission_data (_curMission);

	    Dictionary<string, int> rewards = new Dictionary<string, int>();

	    List<int> _goldRewards = DB_Missions.I.get_possible_gold_rewards(enemiesType);
	    int _goldReward = _misData.goldReward;

	    rewards.Add("gold", _goldReward);
	    SaveHandler.I.gain_gold(_goldReward);

	    // Item Rewards
	    int _rewardsAmountToGive = 1;
	    for (int i = 0; i < _rewardsAmountToGive; i++) {
	        int _rewardToGive = Random.Range(0, 100);
	        foreach (var entry in rewardChance) {
	            if (_rewardToGive < entry.Value && entry.Key != "") {
	                Inv2.I.add_item(entry.Key);
	                rewards.Add(entry.Key, 1);
	                break;
	            }
	        }
	    }

	    return rewards;
	}


	public void start_next_wave (){
		mainWaves.RemoveAt (0);

		if (mainWaves.Count > 0) {
			// MUI_Announcement.I.show ("More enemies are coming!");
			spawn_enemies ();
		} else {
			// Check if there are more maps for this mission
			int curMapLvl = PlayerPrefs.GetInt ("cur-map-lvl");
			string _curMission = ZPlayerPrefs.GetString("missionCur");
			DB_Missions.MissionData _misData = DB_Missions.I.get_mission_data (_curMission);

			curMapLvl++;

			if (curMapLvl >= _misData.enemies.Count) {
				GameUI_GameOver.I.show (
	                "success",
	                generate_and_give_rewards ()
	            );
	            GameUI_GameOver.I.on_victory ();
			} else {
				PlayerPrefs.SetInt ("cur-map-lvl", curMapLvl);
				FightCountdown.I.start_count ("end");
			}
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
        /*InGameObject[] _objects = FindObjectsOfType<InGameObject>();

        foreach (InGameObject _object in _objects) {
        	if (_object.owner != 2) continue;

            Vector3 screenPoint = mainCamera.WorldToViewportPoint(_object.gameObject.transform.position);

            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1) {
                show_arrow (_object.gameObject.transform.position);
            }
            else  {
                destroy_arrow (_object.gameObject);
            }
        }*/
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