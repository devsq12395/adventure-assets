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

	public GameObject go, goImg, goReward;
	public Image imgWindow, imgSpkPort;

	public Sprite imgWin, imgLose;

	public TextMeshProUGUI tSpkName, tSpkTxt;

	public List<GameObject> goRewards;
	public List<Image> imgRewards;
	public List<TextMeshProUGUI> txtRewards;

	public void setup (){
		go.SetActive (true);

		go.SetActive (false);
	}

	public void show (string _title, Dictionary<string, int> _rewards){
		Game.I.pause_game ();
		go.SetActive (true);

		bool _isSuccess = _title == "success";

		imgWindow.sprite = _isSuccess ? imgWin : imgLose;

		go.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        string _missionCur = ZPlayerPrefs.GetString("missionCur");
        DB_Missions.MissionData _missionData = DB_Missions.I.get_mission_data (_missionCur);

        goReward.SetActive (_isSuccess);
        if (_isSuccess) {
        	SoundHandler.I.play_sfx ("win");

        	goRewards.ForEach ((_go)=>_go.SetActive(false));

        	int _curItemIndex = 0;
        	foreach (KeyValuePair<string, int> reward in _rewards) {
        		Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data(reward.Key);

        		goRewards [_curItemIndex].SetActive (true);
        		imgRewards [_curItemIndex].sprite = _data.sprite; Debug.Log (_data.nameUI);
        		txtRewards [_curItemIndex].text = $"{_data.nameUI}{(reward.Value > 1 ? $" x{reward.Value}" : "")}";

        		Inv2_DB.ItemData _data2 = Inv2_DB.I.get_item_data("basic-sword");
        		Debug.Log (_data2.nameUI);

        		_curItemIndex++;
        	}

        	imgSpkPort.sprite = Sprites.I.get_sprite (_missionData.gameOverSpk_img);
	        tSpkName.text = _missionData.gameOverSpk_name;
	        tSpkTxt.text = _missionData.gameOverSpk_text;
        } else {
        	imgSpkPort.sprite = Sprites.I.get_sprite (_missionData.gameOverSpk_imgFail);
	        tSpkName.text = _missionData.gameOverSpk_nameFail;
	        tSpkTxt.text = _missionData.gameOverSpk_textFail;
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
			ZPlayerPrefs.SetString ($"missionCurPool.{_unlockDetails [0]}", _unlockDetails[1]);
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