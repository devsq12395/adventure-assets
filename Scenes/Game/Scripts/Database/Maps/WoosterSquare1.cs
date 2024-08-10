using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WoosterSquare1 : MonoBehaviour {

    public static WoosterSquare1 I;
	public void Awake(){ I = this; }

    public GameObject goMap1, goMap2, goMap3;

    public DB_Maps.mapDetails get_map_details (DB_Maps.mapDetails _new){
        _new.size = new Vector2 (18, 18);

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-500, -500));

        switch (Random.Range (0, 3)) {
            case 0: _new.mapObj = GameObject.Instantiate (goMap1, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
            case 1: _new.mapObj = GameObject.Instantiate (goMap2, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
            default: _new.mapObj = GameObject.Instantiate (goMap3, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject; break;
        }
        SceneManager.MoveGameObjectToScene(_new.mapObj, SceneManager.GetSceneByName("Game"));

        ContMap.I.create_map_objs = create_map_objs;

        return _new;
    }

    public void create_map_objs (){
        
    }

}