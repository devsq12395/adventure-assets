using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Map : MonoBehaviour {
    public static MM_Map I;
	public void Awake(){ I = this; }

    public GameObject go;
    public List<string> mapNames;
    public List<GameObject> mapObjs;

    public Dictionary<string, GameObject> maps;

    public GameObject map;
    public string mapID;

    public void setup (){
        maps = new Dictionary<string, GameObject>();
        for (int i = 0; i < mapNames.Count; i++) {
            maps.Add(mapNames[i], mapObjs[i]);
        }
    }

    public void generate_map (string _mapID){
        GameObject _map = Instantiate (maps [_mapID], go.transform);
        foreach (Transform child in _map.transform) {
            MapNode _comp = child.gameObject.GetComponent<MapNode>();
            if (_comp && _comp.type != "to-menu" ) {
                child.gameObject.SetActive (JsonSaving.I.load ($"areasUnlocked.{_comp.ID}") == "1");
            }
        }

        map = _map;
    }

    public void show (string _mapID){
        go.SetActive (true);
        generate_map (_mapID);
    }

    public void hide (){
        Destroy (map);
        go.SetActive (false);
    }

    public void select_node (string _type, string _val){
        switch (_type) {
            case "mission": MM_Mission.I.show (JsonSaving.I.load ($"missionCurPool.{_val}")); break;
            case "dialog": MMCont_Dialog.I.create_dialog (_val); break;
            case "map": 
                Destroy (map);
                generate_map (_val);
                break;
            case "to-menu":
                hide ();
                break;
        }
    }
}