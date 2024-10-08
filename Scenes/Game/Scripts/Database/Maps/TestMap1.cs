using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMap1 : MonoBehaviour {

    public static TestMap1 I;
	public void Awake(){ I = this; }

    public GameObject goMap;

    public DB_Maps.mapDetails get_map_details (DB_Maps.mapDetails _new){
        _new.size = new Vector2 (35, 35);

        _new.pointList.Add ("playerSpawn", new Vector2 (0, 0));
        _new.pointList.Add ("playerLounge", new Vector2 (-500, -500));

        _new.mapObj = GameObject.Instantiate (goMap, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
        SceneManager.MoveGameObjectToScene(_new.mapObj, SceneManager.GetSceneByName("Game"));

        return _new;
    }

}