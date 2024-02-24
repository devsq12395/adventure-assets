using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap1 : MonoBehaviour {

    public static TestMap1 I;
	public void Awake(){ I = this; }

    public GameObject goMap;

    public DB_Maps.mapDetails get_map_details (DB_Maps.mapDetails _new){
        _new.size = new Vector2 (35, 35);

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-500, -500));

        _new.mapObj = GameObject.Instantiate (goMap, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;

        ContMap.I.create_map_objs = create_map_objs;

        return _new;
    }

    public void create_map_objs (){
        for (var l = 0; l < 1; l++) {
            Vector2 _rand = new Vector2 (
                Random.Range (-ContMap.I.details.size.x, ContMap.I.details.size.x),
                Random.Range (-ContMap.I.details.size.y, ContMap.I.details.size.y)
            );

            ContObj.I.create_obj ("kitsune", _rand, 2);
        }
    }

}