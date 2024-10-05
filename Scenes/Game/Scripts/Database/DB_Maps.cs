using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DB_Maps : MonoBehaviour {

    public static DB_Maps I;
	public void Awake(){ I = this; }

    public GameObject goMap_woosterSquare1, goMap_woosterSquare2, goMap_woosterSquare3, goMap_woosterSquare4, goMap_trainingGrounds;

    public struct mapDetails {
        public string name;
        public Dictionary<string, Vector2> pointList;
        public GameObject mapObj;
        public Vector2 size;

        public mapDetails (string _name){
            pointList = new Dictionary<string, Vector2> ();
            name = _name;
            mapObj = null;
            size = new Vector2 (0, 0);
        }
    }

    public mapDetails get_map_details (string _name) {
        mapDetails _new = new mapDetails (_name);

        switch (_name) {
            // Wooster Square 1
            case "map-tutorial": 
                _new = get_map_details_generic (_new);
                _new.mapObj = GameObject.Instantiate (goMap_woosterSquare1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;
            case "map-wooster-square-1": 
                _new = get_map_details_generic (_new);
                _new.mapObj = GameObject.Instantiate (goMap_woosterSquare1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;
            case "map-wooster-square-2": 
                _new = get_map_details_generic (_new);
                _new.mapObj = GameObject.Instantiate (goMap_woosterSquare2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;
            case "map-wooster-square-3": 
                _new = get_map_details_generic (_new);
                _new.mapObj = GameObject.Instantiate (goMap_woosterSquare3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;
            case "map-wooster-square-4": 
                _new = get_map_details_generic (_new);
                _new.mapObj = GameObject.Instantiate (goMap_woosterSquare4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;
            
            case "woosterSquare_rand":
                _new = get_map_details_generic (_new);
                switch (Random.Range (0, 3)) {
                    case 0: _new.mapObj = GameObject.Instantiate (goMap_woosterSquare1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
                    case 1: _new.mapObj = GameObject.Instantiate (goMap_woosterSquare2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
                    case 2: _new.mapObj = GameObject.Instantiate (goMap_woosterSquare3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
                    default:_new.mapObj = GameObject.Instantiate (goMap_woosterSquare4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
                }
                SceneManager.MoveGameObjectToScene(_new.mapObj, SceneManager.GetSceneByName("Game"));
                break;

            case "training-grounds":
                _new = get_map_details_training_grounds (_new);
                break;

        }

        return _new;
    }

    public mapDetails get_map_details_generic (mapDetails _new){
        _new.size = new Vector2 (18, 18);

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-500, -500));

        return _new;
    }
    public mapDetails get_map_details_training_grounds (mapDetails _new){
        _new.size = new Vector2 (18, 18);

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-500, -500));

        _new.mapObj = GameObject.Instantiate (goMap_trainingGrounds, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        SceneManager.MoveGameObjectToScene(_new.mapObj, SceneManager.GetSceneByName("Game"));

        return _new;
    }
}
