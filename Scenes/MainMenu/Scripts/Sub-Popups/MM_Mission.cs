using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using SimpleJSON;

public class MM_Mission : MonoBehaviour {
    public static MM_Mission I;

    public GameObject go;
    public TextMeshProUGUI title, desc;
    public Image port;

    public string missionID;

    private string jsonString, jsonStringPath;

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

    public void setup (){
        
    }

    public void show (string _missionID){
        missionID = _missionID;

        go.SetActive (true);
        setup_ui ();
    }

    public void hide (){
        go.SetActive (false);
    }

    private void setup_ui (){
        title.text = get_mission_val ("name");
        desc.text = get_mission_val ("desc");
        port.sprite = MM_Sprites.I.get_sprite (get_mission_val ("sprite"));
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

    public void btn_accept (){
        JsonSaving.I.save ("missionCur", missionID);
        JsonSaving.I.save ("missionLvl", "1");
        JsonSaving.I.save ("missionMap", "0");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}