using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameUI_Popup : MonoBehaviour {
    public static GameUI_Popup I;
	public void Awake(){ I = this; }

	public GameObject go, goImg;

	public bool check_if_show (){
		bool _isShow = false;

		if (ZPlayerPrefs.GetString("missionCur") != "vic-1") {
			go.SetActive (false);
			return false;
		}

		Game.I.pause_game ();
		go.SetActive (true);

		go.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        return true;
	}

	public void btn_start (){
		go.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack)
			.OnComplete(() => {
				Game.I.resume_game ();
				FightCountdown.I.start_count ("start");
			});
	}
}