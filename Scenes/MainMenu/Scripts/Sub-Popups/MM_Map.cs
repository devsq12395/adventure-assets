using System.Collections;
using System.Collections.Generic;
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
        mapID = MM_Save.I.load ("area");
        
        maps = new Dictionary<string, GameObject>();
        for (int i = 0; i < mapNames.Count; i++) {
            maps.Add(mapNames[i], mapObjs[i]);
        }
    }

    public void generate_map (){
        GameObject _map = Instantiate (maps [mapID], go.transform);

        go.SetActive (false);
    }

    public void show (){
        go.SetActive (true);
    }

    public void hide (){
        go.SetActive (false);
    }

    public void select_node (string _missionID){
        MM_Mission.I.show (_missionID);
    }
}