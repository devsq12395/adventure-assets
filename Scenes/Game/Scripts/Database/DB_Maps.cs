using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DB_Maps : MonoBehaviour {

    public static DB_Maps I;
	public void Awake(){ I = this; }

    public GameObject goMap_woosterSquare1, goMap_woosterSquare2, goMap_woosterSquare3, goMap_woosterSquare4, goMap_trainingGrounds;

    public float SIZE_PER_PIECE; // Assuming each map piece is a square

    public struct mapDetails {
        SIZE_PER_PIECE = 50;

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
        public int sizeX, sizeY;
        public List<List<bool>> sizeMatrix;

        public mapObject (GameObject _go){
            go = _go;
            sizeX = 1; sizeY = 1;

            sizeMatrix = new List<List<bool>>();
        }
    }

    public mapDetails get_map_details (string _name) {
        mapDetails _new = new mapDetails (_name);

        switch (_name) {
            // Wooster Square 1
            case "map-tutorial": case "map-wooster-square-1": case "map-wooster-square-2": case "map-wooster-square-3": case "map-wooster-square-4": 
                _new = get_map_details_generic (_new);
                break;
            
            case "woosterSquare_rand":
                _new = get_map_details_generic (_new);
                break;

            case "training-grounds":
                _new = get_map_details_training_grounds (_new);
                break;

        }

        return _new;
    }

    public List<string> get_map_lists (string _biome){
        List<string> _new = new List<string>();
        switch (_biome){
            case "wooster-square":
                _new.AddRange (new string []{"map-wooster-square-1","map-wooster-square-2","map-wooster-square-3","map-wooster-square-4"});
                break;
        }
        return _new;
    }

    public mapObject get_map_game_object (string _name) {
        mapObject _new = new mapObject(null);

        mapDetails details = ContMap.I.details;
        for(int i = 0; i < details.mapObjMatrix_sizeX; i++) {
            List<bool> toAdd = new List<bool>();

            for(int i = 0; i < details.mapObjMatrix_sizeY; i++) {
                toAdd.Add (false);
            }

            _new.sizeMatrix.Add (toAdd);
        }

        switch (_name) {
            case "map-wooster-square-1": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                _new.sizeX = 1; _new.sizeY = 3;

                _new[0][0] = true;
                _new[0][0] = true;
                break;
            case "map-wooster-square-2": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                _new.sizeX = 1; _new.sizeY = 1;
                break;
            case "map-wooster-square-3": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                _new.sizeX = 1; _new.sizeY = 1;
                break;
            case "map-wooster-square-4": 
                _new.go = GameObject.Instantiate (goMap_woosterSquare4, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; 
                _new.sizeX = 1; _new.sizeY = 1;
                break;

        }

        SceneManager.MoveGameObjectToScene(_new, SceneManager.GetSceneByName("Game"));
        return _new;
    }

    public mapDetails get_map_details_generic (mapDetails _new){
        _new.size = new Vector2 (18, 18);
        
        _new.biome = "wooster-square";
        _new.mapObjMatrix_sizeX = 5; _new.mapObjMatrix_sizeY = 5;

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-5000, -5000));

        return _new;
    }
    public mapDetails get_map_details_training_grounds (mapDetails _new){
        _new.size = new Vector2 (18, 18);

        _new.biome = "wooster-square";
        _new.mapObjMatrix_sizeX = 5; _new.mapObjMatrix_sizeY = 5;

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-5000, -5000));

        return _new;
    }
}
