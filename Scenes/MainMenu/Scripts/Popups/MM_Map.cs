using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Map : MonoBehaviour {
    public static MM_Map I;
	public void Awake(){ I = this; }

    public GameObject go, goMapParent;

    public Dictionary<string, GameObject> maps;

    public GameObject map;
    public string mapID;

    public void setup (){
        mapID = MM_Save.I.load ("area");
    }

    public void generate_map (string _mapID){
        mapID = _mapID;
        GameObject _map = GameObject.Instantiate (maps [mapID]);
        _map.transform.SetParent (goMapParent);
        _map.transform.position = Vector3.zero;
    }

    public void select_node (string _missionID){
        MM_Mission.I.show (_missionID);
    }
}