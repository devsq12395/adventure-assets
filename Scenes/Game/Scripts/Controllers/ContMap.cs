using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContMap : MonoBehaviour {

    public static ContMap I;
	public void Awake(){ I = this; }

    public GameObject map;
    public DB_Maps.mapDetails details;
    public Dictionary<string, Vector2> pointList;

    public delegate void create_map();
    public create_map create_map_objs {get; set;}

    public void setup_map (){
        string  _mission = JsonSaving.I.load ("missionCur"),
                _curMap = JsonReading.I.read ("missions", $"missions.{_mission}.maps").Split (",")[0];

        details = DB_Maps.I.get_map_details (_curMap);

        map = details.mapObj;
        pointList = details.pointList;

        create_map_objs ();
    }

    public void change_map (string _map){
        PlayerPrefs.SetString ("map", _map);
        Game.I.change_scene ("game");
    }
}