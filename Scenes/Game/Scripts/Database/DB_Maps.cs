using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DB_Maps : MonoBehaviour {

    public static DB_Maps I;
	public void Awake(){ I = this; }

    public GameObject goMap_woosterSquare1, goMap_woosterSquare2, goMap_woosterSquare3, goMap_woosterSquare4, goMap_trainingGrounds,
        goMap_mainTrainingGrounds, goMap_mainVic1, goMap_mainVincenzo1, goMap_mainAnastasia1, goMap_mainBeatrice1, goMap_mainAnthony1,
        goMap_mainCursedForest;

    public float SIZE_PER_PIECE; // Assuming each map piece is a square

    public struct mapDetails {
        public string name, biome;
        public Dictionary<string, Vector2> pointList;
        public GameObject mapObj;
        public Vector2 size;
        public int mapObjMatrix_sizeX, mapObjMatrix_sizeY; // How many pieces per map in a coordinate

        public mapDetails (string _name){
            pointList = new Dictionary<string, Vector2> ();
            name = _name;
            biome = "";
            mapObj = null;
            size = new Vector2 (0, 0);
            mapObjMatrix_sizeX = 0; mapObjMatrix_sizeY = 0;
        }
    }

    public struct mapObject {
        public GameObject go;
        public string nextMap_down, nextMap_right;

        public mapObject (GameObject _go){
            go = _go;
            nextMap_down = "";
            nextMap_right = "";
        }
    }

    void Start (){
        SIZE_PER_PIECE = 100; // Also set on ContMap.cs for now
    }

    public mapDetails get_map_details (string _mapName) {
        mapDetails _new = new mapDetails (_mapName);

        switch (_mapName) {
            // Wooster Square 1
            case "map-tutorial": case "map-wooster-square-1": case "map-wooster-square-2": case "map-wooster-square-3": case "map-wooster-square-4": 
                _new = get_map_details_generic (_new);
                break;
            
            case "woosterSquare_rand":
                _new = get_map_details_generic (_new);
                break;

            case "training-grounds": _new = get_map_details_training_grounds (_new); break;

        }

        return _new;
    }

    public List<string> get_map_lists (string _biome){
        List<string> _new = new List<string>();
        switch (_biome){
            case "wooster-square":
                _new.AddRange (new string []{"map-wooster-square-1", "map-wooster-square-2"});
                break;
            case "training-grounds":
                _new.AddRange (new string []{"map-wooster-square-1", "map-wooster-square-2"});
                break;
        }
        return _new;
    }

    public mapObject get_map_game_object (string _name) {
        mapObject _new = new mapObject(null);

        mapDetails details = ContMap.I.details;

        switch (_name) {
            // Boss pieces
            case "map-main-training-grounds": _new.go = GameObject.Instantiate (goMap_mainTrainingGrounds, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
            case "map-main-vic-1": _new.go = GameObject.Instantiate (goMap_mainVic1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
            case "map-main-vincenzo-1": _new.go = GameObject.Instantiate (goMap_mainVincenzo1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
            case "map-main-anastasia-1": _new.go = GameObject.Instantiate (goMap_mainAnastasia1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
            case "map-main-beatrice-1": _new.go = GameObject.Instantiate (goMap_mainBeatrice1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
            case "map-main-anthony-1": _new.go = GameObject.Instantiate (goMap_mainAnthony1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;
            case "map-main-cursed-forest": _new.go = GameObject.Instantiate (goMap_mainCursedForest, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;  break;

            // Main pieces
            case "map-wooster-square-1": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;
            case "map-wooster-square-2": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                _new.nextMap_right = "map-wooster-square-3";
                break;
            case "map-wooster-square-3": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                break;

        }

        SceneManager.MoveGameObjectToScene(_new.go, SceneManager.GetSceneByName("Game"));
        return _new;
    }

    public mapDetails get_map_details_generic (mapDetails _new){
        _new.size = new Vector2 (250, 250);
        
        _new.biome = "wooster-square";
        _new.mapObjMatrix_sizeX = 5; _new.mapObjMatrix_sizeY = 5;

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-10000, -10000));

        return _new;
    }
    public mapDetails get_map_details_training_grounds (mapDetails _new){ 
        _new.size = new Vector2 (250, 250);

        _new.biome = "wooster-square";
        _new.mapObjMatrix_sizeX = 5; _new.mapObjMatrix_sizeY = 5;

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-10000, -10000));

        return _new;
    }
}
