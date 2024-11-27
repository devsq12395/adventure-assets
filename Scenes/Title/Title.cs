using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class Title : MonoBehaviour {
    public static Title I;
	public void Awake(){ I = this; }

    public GameObject curtainGo;
    public RectTransform curtainRect;
    public TextMeshProUGUI tSound, tMusic;
    
    private string curtainState;
    public float targetX;

    public string login;

    public TextMeshProUGUI headerTxt_Gold;

    void Start() {
        curtainRect.anchoredPosition = new Vector2(-Screen.width - curtainRect.rect.width / 2, curtainRect.anchoredPosition.y);
    }

    void Update() {
        
    }

    public void btn_sound (){
        SoundHandler.I.soundOn = !SoundHandler.I.soundOn;
        tSound.text = $"Sound: {(SoundHandler.I.soundOn ? "ON" : "OFF")}";
    }

    public void btn_music (){
        SoundHandler.I.musicOn = !SoundHandler.I.musicOn;
        if (SoundHandler.I.musicOn) {
            SoundHandler.I.resume_bgm ();
        } else {
            SoundHandler.I.stop_bgm ();
        }
        tMusic.text = $"Music: {(SoundHandler.I.musicOn ? "ON" : "OFF")}";
    }

    public void move_curtain() {
        curtainGo.SetActive(true);

        Vector2 _curtainPos = new Vector2(-Screen.width - curtainRect.rect.width / 2, curtainRect.anchoredPosition.y);
        targetX = 0;

        float speed = 1f;
        curtainRect.anchoredPosition = _curtainPos;

        curtainRect.DOAnchorPosX(targetX, speed).OnComplete(() => curtain_move_end());
    }

    private void curtain_move_end() {
        MasterScene.I.change_main_scene ("MainMenu");
    }

    public void btn_reset (){
        SaveHandler.I.reset_all_saves ();
    }
}
