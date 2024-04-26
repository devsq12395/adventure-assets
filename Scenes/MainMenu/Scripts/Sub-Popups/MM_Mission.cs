using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using SimpleJSON;
using DG.Tweening;

public class MM_Mission : MonoBehaviour {
    public static MM_Mission I;

    public GameObject go;
    public List<Image> imgWindows;
    public TextMeshProUGUI title, desc, desc2;
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
        imgWindows.ForEach ((_window) => {
            _window.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
            _window.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        });
        
        setup_ui ();
    }

    public void hide (){
        imgWindows.ForEach ((_window) => {
            _window.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
        });
    }

    private void setup_ui (){
        title.text = get_mission_val ("name");
        desc.text = get_mission_val ("desc");
        port.sprite = Sprites.I.get_sprite (get_mission_val ("sprite"));

        string  _json = $"missions.{missionID}",
                _faction = JsonReading.I.read ("missions", $"{_json}.faction"),
                _difficulty = JsonReading.I.read ("missions", $"{_json}.difficulty"),
                _rewards = JsonReading.I.read ("missions", $"{_json}.rewards");
        desc2.text = $"Faction: {_faction}\nDifficulty: {_difficulty}\n\nRewards: {_rewards}";
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
        MainMenu.I.move_curtain ("toGame");
    }
}