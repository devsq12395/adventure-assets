
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContEnemies : MonoBehaviour {

    public static ContEnemies I;
	public void Awake(){ I = this; }

	public Dictionary<string, int> rewardChance;

	public string enemiesType;

	public void setup (string _enemiesType){
		enemiesType = _enemiesType;

		rewardChance = DB_Enemies.I.get_reward_chance (_enemiesType);
	}

	public void spawn_enemies_per_map_piece (){
		string _mission = ZPlayerPrefs.GetString("missionCur");
		List<Dictionary<string, int>> _groups = DB_Enemies.I.get_enemy_group (_mission);

		List<GameObject> _maps = ContMap.I.maps;
		foreach (var _map in _maps) {
			Dictionary<string, int> _randGroup = _groups [UnityEngine.Random.Range (0, _groups.Count)];

			foreach (var _waveData in _randGroup){
				if (!DB_Enemies.I.check_special_spawn (_waveData.Key)) {
					for (int i = 0; i < _waveData.Value; i++) {
						Vector2 _rand = get_spawn_point_from_center_of_input (_map.transform.position);
			            ContObj.I.create_obj_spawner (_waveData.Key, _rand, 2);
					}
				}
	        }
		}
	}

	private Vector2 get_spawn_point_from_center_of_input(Vector3 _pos) {
	    Vector2 randomPoint;
	    float maxDistance = 7f;

	    do {
	        randomPoint = new Vector2(
	            _pos.x + Random.Range(-25, 25),
	            _pos.y + Random.Range(-25, 25)
	        );
	    } while (Vector2.Distance(randomPoint, ContPlayer.I.player.gameObject.transform.position) <= maxDistance);

	    return randomPoint;
	}

	public Dictionary<string, int> generate_and_give_rewards() {
		string _curMission = ZPlayerPrefs.GetString("missionCur");
		DB_Missions.MissionData _misData = DB_Missions.I.get_mission_data (_curMission);

	    Dictionary<string, int> rewards = new Dictionary<string, int>();

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

	public void trigger_boss_kill (){
		GameUI_GameOver.I.show (
            "success",
            generate_and_give_rewards ()
        );
        GameUI_GameOver.I.on_victory ();
	}

	public void trigger_next_area (){
		// Check if there are more maps for this mission
		int curMapLvl = PlayerPrefs.GetInt ("cur-map-lvl");
		string _curMission = ZPlayerPrefs.GetString("missionCur");
		DB_Missions.MissionData _misData = DB_Missions.I.get_mission_data (_curMission);

		curMapLvl++;
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