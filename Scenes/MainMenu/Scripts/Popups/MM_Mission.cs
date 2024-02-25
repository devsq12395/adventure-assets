using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SimpleJSON;

public class MM_Mission : MonoBehaviour {
    public static MM_Mission I;

    public GameObject go;
    public TextMeshProUGUI title, desc;

    public string missionID;

    private string jsonStringPath;

    public void Awake () {
        I = this;
        jsonStringPath = $"{Application.dataPath}/json-db/missions.json";

        if (File.Exists(jsonStringPath)) {
            jsonString = File.ReadAllText(jsonStringPath);
        }
        else {
            Debug.LogError("JSON file not found at: " + jsonStringPath);
        }
    }

    public void show (string _missionID){
        missionID = _missionID;

        go.SetActive (true);
        setup_ui ();
    }

    public void hide (){
        go.SetActive (true);
    }

    private void setup_ui (){
        title.text = get_mission_val ("name");
        desc.text = get_mission_val ("desc");
    }

    public string get_mission_val (string key) {
        JSONNode jsonObject = JSON.Parse(jsonString);
        JSONNode valsObj = jsonObject["missions"][missionID];

        if (valsObj != null) {
            if (valsObj[key] != null) {
                return valsObj[key];
            }
        }

        return null;
    }
}