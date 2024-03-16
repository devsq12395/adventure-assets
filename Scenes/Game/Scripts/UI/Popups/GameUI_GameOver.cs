using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
	}

	public void btn_go_to_menu (){
		Transition_Game.I.change_state ("toMenu");
	}

	private void on_victory (){
		string _curMission = JsonSaving.I.load ("missionCur");

		string[] 	_missionCurPool = JsonSaving.I.load ("missionCurPool").Split (','),
					_missionsToUnlock = JsonReading.I.read ("missions", $"missions.{_curMission}.missions-set").Split (',');

		// Unlocks all missions marked to be unlocked on missions.json
		for (int i = 0; i < _missionsToUnlock.Length; i++) {
			string[] _unlockDetails = _missionsToUnlock [i].Split ("->");
			JsonSaving.I.save ($"missionCurPool.{_unlockDetails [0]}", _unlockDetails[1]);
		}

		// Unlocks all areas marked to be unlocked on missions.json
		string[] _areasToUnlock = JsonSaving.I.load ("missionCurPool").Split (',');
		for (int i = 0; i < _areasToUnlock.Length; i++) {
			JsonSaving.I.save ($"areasUnlocked.{_areasToUnlock [i]}", "1");
		}
	}
}