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
        DB_Missions.MissionData _data = DB_Missions.I.get_mission_data (missionID);

        title.text = _data.speaker;
        desc.text = _data.desc;
        port.sprite = Sprites.I.get_sprite (_data.sprite);

        string _rewards = _data.rewards;
        desc2.text = $"Rewards: {_rewards}";
    }

    public void btn_accept (){
        ZPlayerPrefs.SetString ("missionCur", missionID);
        MainMenu.I.move_curtain ("toGame");
    }
}