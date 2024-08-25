using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Maps : MonoBehaviour {

    public static DB_Maps I;
	public void Awake(){ I = this; }

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

    public string is_map_random_and_get_rand_map (string _name){
        List<string> _maps = new List<string>(){_name};
        switch (_name){
            case "woosterSquare_rand": _maps = new List<string> (){
                "woosterSquare_01","woosterSquare_02","woosterSquare_03",
            }; break;
        }

        string _ret = _maps [Random.Range (0, _maps.Count)];
        return _ret;
    }

    public mapDetails get_map_details (string _name) {
        _name = is_map_random_and_get_rand_map (_name);
        mapDetails _new = new mapDetails (_name);
        
        switch (_name) {
            case "map-vic-1": _new = WoosterSquare1.I.get_map_details (_new); break;
            case "map-vic-2": _new = WoosterSquare1.I.get_map_details (_new); break;
            case "map-vic-3": _new = WoosterSquare1.I.get_map_details (_new); break;

            case "woosterSquare_01": _new = WoosterSquare1.I.get_map_details (_new); break;
            case "woosterSquare_02": _new = WoosterSquare1.I.get_map_details (_new); break;
            case "woosterSquare_03": _new = WoosterSquare1.I.get_map_details (_new); break;

            case "newHaven_caleb01": _new = WoosterSquare1.I.get_map_details (_new); break;
            case "newHaven_caleb02": _new = WoosterSquare1.I.get_map_details (_new); break;
        }

        return _new;
    }
}
