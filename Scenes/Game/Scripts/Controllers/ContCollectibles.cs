
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContCollectibles : MonoBehaviour {

    public static ContCollectibles I;
	public void Awake(){ I = this; }

	private List<string> collectiblesList = new List<string> { "barrel" };

	public void spawn_collectible_per_map_piece (){
		string _mission = ZPlayerPrefs.GetString("missionCur");
		List<Dictionary<string, int>> _groups = DB_Enemies.I.get_enemy_group (_mission);

		int _toSpawnCount = UnityEngine.Random.Range (0, 10);

		List<GameObject> _maps = ContMap.I.maps;
		foreach (var _map in _maps) {
			string _randCollectible = collectiblesList [UnityEngine.Random.Range (0, collectiblesList.Count)];

			for (int i = 0; i <= _toSpawnCount; i++) {
				Vector2 _rand = get_spawn_point_from_center_of_input (_map.transform.position);
				Debug.Log ($"spawning {_randCollectible} at {_rand.x}, {_rand.y}");
	            ContObj.I.create_obj (_randCollectible, _rand, 2);
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
}