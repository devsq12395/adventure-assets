using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameUI_GameOver : MonoBehaviour {
    public static GameUI_GameOver I;
	public void Awake(){ I = this; }

	public GameObject go, goImg;
	public Image imgTitle;

	public Sprite imgWin, imgLose, imgSpkPort;

	public TextMeshProUGUI tTitle, tDesc, tSpkName, tSpkTxt;

	public void setup (){
		go.SetActive (true);

		go.SetActive (false);
	}

	public void show (string _title, string _desc){
		Game.I.pause_game ();
		go.SetActive (true);

		imgTitle.sprite = (_title == "success") ? imgWin : imgLose;
		tDesc.text = _desc;

		go.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        if (_title == "success") {
        	SoundHandler.I.play_sfx ("win");
        }

        PlayerPrefs.SetInt("cur-map-lvl", 0);
	}

	public void btn_go_to_menu (){
		Transition_Game.I.change_state ("toMenu");
	}

	public void on_victory (){
		string _curMission = ZPlayerPrefs.GetString("missionCur");
		DB_Missions.MissionData _data = DB_Missions.I.get_mission_data (_curMission);

		List<string> _missionsToUnlock = _data.missionsSet;

		// Unlocks all missions marked to be unlocked on missions.json
		for (int i = 0; i < _missionsToUnlock.Count; i++) {
			string[] _unlockDetails = _missionsToUnlock [i].Split ("->");
			PlayerPrefs.SetString ($"missionCurPool.{_unlockDetails [0]}", _unlockDetails[1]);
		}

		// Unlocks all areas marked to be unlocked on missions.json
		List<string> _areasToUnlock = _data.unlocksArea;
		for (int i = 0; i < _areasToUnlock.Count; i++) {
			PlayerPrefs.SetInt ($"areasState.{_areasToUnlock [i]}", 1);
		}

		// Sets activity
		List<string> _activity = _data.activitySet;
		for (int i = 0; i < _activity.Count; i++) {
			string[] _activityToSet = _activity [i].Split ("->");
			PlayerPrefs.SetInt ($"activity.{_activityToSet [i]}", int.Parse (_activityToSet[1]));
		}

		on_victory_callbacks (_curMission);
	}

	public void on_victory_callbacks (string _curMission){
		switch (_curMission) {
			case "vic-1": 
				ZPlayerPrefs.SetString("main-menu-start-callback", "finished-mission-vic-1");
				break;
		}
	}
}