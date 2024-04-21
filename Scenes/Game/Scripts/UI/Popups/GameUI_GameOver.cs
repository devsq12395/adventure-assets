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

	public GameObject go;

	public TextMeshProUGUI tTitle, tDesc;

	public void setup (){
		go.SetActive (true);

		go.SetActive (false);
	}

	public void show (string _title, string _desc){
		Game.I.pause_game ();
		go.SetActive (true);

		tTitle.text = _title;
		tDesc.text = _desc;

		go.transform.localScale = Vector3.zero;
        go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
	}

	public void btn_go_to_menu (){
		Transition_Game.I.change_state ("toMenu");
	}

	public void on_victory (){
		string _curMission = JsonSaving.I.load ("missionCur");

		string[] 	_missionCurPool = JsonSaving.I.load ("missionCurPool").Split (','),
					_missionsToUnlock = JsonReading.I.read ("missions", $"missions.{_curMission}.missions-set").Split (',');

		// Unlocks all missions marked to be unlocked on missions.json
		for (int i = 0; i < _missionsToUnlock.Length; i++) {
			string[] _unlockDetails = _missionsToUnlock [i].Split ("->");
			JsonSaving.I.save ($"missionCurPool.{_unlockDetails [0]}", _unlockDetails[1]);
		}

		// Unlocks all areas marked to be unlocked on missions.json
		string[] _areasToUnlock = JsonReading.I.read ("missions", $"missions.{_curMission}.unlocks-area").Split (',');
		for (int i = 0; i < _areasToUnlock.Length; i++) {
			JsonSaving.I.save ($"areasUnlocked.{_areasToUnlock [i]}", "1");
		}

		// Sets activity
		string[] _activity = JsonReading.I.read ("missions", $"missions.{_curMission}.activity-set").Split (',');
		for (int i = 0; i < _activity.Length; i++) {
			string[] _activityToSet = _activity [i].Split ("->");
			JsonSaving.I.save ($"activity.{_activityToSet [0]}", _activityToSet [1]);
		}

		on_victory_callbacks (_curMission);
	}

	public void on_victory_callbacks (string _curMission){
		switch (_curMission) {
			case "vic-1": 
				JsonSaving.I.save ("main-menu-start-callback", "finished-mission-vic-1");
				break;
		}
	}
}