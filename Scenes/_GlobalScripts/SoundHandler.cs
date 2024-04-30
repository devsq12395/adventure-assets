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

    public AudioClip bubble, buying, chat, click, explosion, gain, ice, laser, magic, plasmaShotgun, swish, swoosh, torrent, win, zap, 
        bigHit, dash, dashSmoke;

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

            case "bubble": audioSource.PlayOneShot(bubble); break;
            case "buying": audioSource.PlayOneShot(buying); break;
            case "chat": audioSource.PlayOneShot(chat); break;
            case "click": audioSource.PlayOneShot(click); break;
            case "explosion": audioSource.PlayOneShot(explosion); break;
            case "big-hit": audioSource.PlayOneShot(bigHit); break;
            case "gain": audioSource.PlayOneShot(gain); break;
            case "ice": audioSource.PlayOneShot(ice); break;
            case "laser": audioSource.PlayOneShot(laser); break;
            case "magic": audioSource.PlayOneShot(magic); break;
            case "plasma-shotgun": audioSource.PlayOneShot(plasmaShotgun); break;
            case "swish": audioSource.PlayOneShot(swish); break;
            case "swoosh": audioSource.PlayOneShot(swoosh); break;
            case "torrent": audioSource.PlayOneShot(torrent); break;
            case "win": audioSource.PlayOneShot(win); break;
            case "zap": audioSource.PlayOneShot(zap); break;
            case "dash": audioSource.PlayOneShot(dash); break;
            case "dash-smoke": audioSource.PlayOneShot(dashSmoke); break;
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