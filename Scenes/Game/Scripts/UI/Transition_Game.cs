using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition_Game : MonoBehaviour {
    public static Transition_Game I;
	public void Awake(){ I = this; }

    public GameObject curtainGo;
    public RectTransform curtainRect;

    private string state;
    private float speed = 1000f;

    void Start() {
        curtainGo.SetActive (true);
        state = "gameStart";
    }

    void Update() {
        switch (state){
            case "gameStart":
                curtainRect.anchoredPosition += Vector2.right * speed * Time.deltaTime;

                if (curtainRect.anchoredPosition.x - curtainRect.rect.width / 2 > Screen.width) {
                    state = "";
                }
                break;
            case "gameEnd":
                curtainRect.anchoredPosition += Vector2.right * speed * Time.deltaTime;

                if (curtainRect.anchoredPosition.x - curtainRect.rect.width / 2 > 0) {
                    change_scene ("game");
                }
                break;
        }
    }

    // Transition to a new scene
    public void start_transition_to_scene (string _scene){
        switch (_scene) {
            case "game":
                state = "gameEnd";
                break;
        }
    }

    public void change_scene (string _scene){
        ContPlayer.I.save_items ();

        switch (_scene) {
            case "game":

                break;
        }
    }
}