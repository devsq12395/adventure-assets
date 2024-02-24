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

    public mapDetails get_map_details (string _name) {
        mapDetails _new = new mapDetails (_name);
        
        switch (_name) {
            case "testMap": _new = TestMap1.I.get_map_details (_new); break;
        }

        return _new;
    }
}
