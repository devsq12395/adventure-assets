using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Transition_Game : MonoBehaviour {
    public static Transition_Game I;
    void Awake(){I = this;}

    public GameObject curtainGo;
    public RectTransform curtainRect;

    private string state; // gameStart, toMenu, toNextMap

    public float targetX;

    void Start() {
        curtainGo.SetActive(true);
        change_state("gameStart", 0, curtainRect.anchoredPosition.y);
        
    }

    public void change_state(string _state, float _curtainPosX = 0, float _curtainPosY = 0) {
        curtainGo.SetActive(true);

        Vector2 _curtainPos;
        switch (_state) {
            case "gameStart":
                _curtainPos = new Vector2(0, curtainRect.anchoredPosition.y);
                targetX = Screen.width + curtainRect.rect.width / 2;
                break;
            default:
                _curtainPos = new Vector2(-Screen.width - curtainRect.rect.width / 2, curtainRect.anchoredPosition.y);
                targetX = 0;
                break;
        }

        curtainRect.anchoredPosition = _curtainPos;
        state = _state;
        move_curtain ();
    }

    private void move_curtain() {
        float speed = 1.75f;

        curtainRect.DOAnchorPosX(targetX, speed).OnComplete(() => curtain_move_end());
    }

    private void curtain_move_end() {
        switch (state) {
            case "gameStart":
                curtainGo.SetActive(false);
                break;
            case "toMenu":
                MasterScene.I.change_main_scene ("MainMenu");
                break;
            case "toNextMap":
                MasterScene.I.change_main_scene ("Game");
                break;
        }
    }
}