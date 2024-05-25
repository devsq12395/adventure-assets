using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Missions missionsData;

    public void Awake () {
        I = this;
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
        Mission foundMission = missionsData.missionList.FirstOrDefault(mission => mission.missionId == missionID);
        if (foundMission != null) {
            switch (key)
            {
                case "name":
                    return foundMission.name;
                case "desc":
                    return foundMission.desc;
                case "sprite":
                    return foundMission.sprite;
                case "faction":
                    return foundMission.faction;
                case "difficulty":
                    return foundMission.difficulty;
                case "rewards":
                    return foundMission.rewards;
                case "enemies":
                    return foundMission.enemies;
                case "maps":
                    return string.Join(", ", foundMission.maps);
                case "unlocksArea":
                    return string.Join(", ", foundMission.unlocksArea);
                case "missionsSet":
                    return string.Join(", ", foundMission.missionsSet);
                case "activitySet":
                    return string.Join(", ", foundMission.activitySet);
                default:
                    Debug.LogWarning($"Key not found: {key}");
                    return "";
            }
        } else {
            Debug.Log ($"Mission not found: {missionID}");
            return "";
        }
    }

    public void btn_accept (){
        JsonSaving.I.save ("missionCur", missionID);
        JsonSaving.I.save ("missionLvl", "1");
        JsonSaving.I.save ("missionMap", "0");
        MainMenu.I.move_curtain ("toGame");
    }
}