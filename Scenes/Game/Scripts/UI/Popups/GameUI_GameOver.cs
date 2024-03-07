using System.Collections;
using System.Collections.Generic;
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
}