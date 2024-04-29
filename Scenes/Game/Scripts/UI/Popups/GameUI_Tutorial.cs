using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameUI_Tutorial : MonoBehaviour {
    public static GameUI_Tutorial I;
	public void Awake(){ I = this; }

	public GameObject go, goImg;

	public void check_if_show (){
		if (JsonSaving.I.load ("missionCur") != "vic-1") {
			go.SetActive (false);
			return;
		}

		Game.I.pause_game ();
		go.SetActive (true);

		go.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
	}

	public void btn_start (){
		go.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack)
			.OnComplete(() => {
				Game.I.resume_game ();
			});
	}
}