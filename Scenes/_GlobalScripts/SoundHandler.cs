using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

    public static SoundHandler I;
    public void Awake(){ I = this; }

    public AudioSource audioSource;

    public AudioClip yesPing, noPing;
    public AudioClip bgmMenu;
    public AudioClip[] bgmGame;

    private int curBgm;
    private bool isPlayingBGM;
    private string bgmType;

    void Start (){
        audioSource.loop = false; 
        play_bgm ("menu");
    }

    void Update (){
        if (isPlayingBGM && !audioSource.isPlaying) {
            isPlayingBGM = false;
            play_bgm (bgmType);
        }
    }

    public void play_sfx (string _sound) {
        switch (_sound) {
            case "yes-ping": audioSource.PlayOneShot(yesPing); break;
            case "no-ping": audioSource.PlayOneShot(noPing); break;
        }
    }

    public void play_bgm (string _music) {
        if (isPlayingBGM && _music == bgmType) return;

        AudioClip bgmClip = null;

        switch (_music) {
            case "menu": bgmClip = bgmMenu; break;
            case "game": bgmClip = set_bgm_game (); break;
        }

        audioSource.clip = bgmClip;
        audioSource.Play();

        isPlayingBGM = true;
        bgmType = _music;
    }

    public AudioClip set_bgm_game (){
        int _newBgm = Random.Range (0, 3);
        while (_newBgm == curBgm) _newBgm = Random.Range (0, 3);
        curBgm = _newBgm;
        return bgmGame [_newBgm];
    }

    public void stop_bgm (){
        audioSource.Stop ();
        isPlayingBGM = false;
    }
}