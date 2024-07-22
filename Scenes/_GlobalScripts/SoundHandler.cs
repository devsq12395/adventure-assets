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

    private Dictionary<string, float> lastPlayedTime = new Dictionary<string, float>();
    private float soundCooldown;

    void Start (){
        soundCooldown = 0.25f;
        audioSource.loop = false; 
    }

    void Update (){
        if (isPlayingBGM && !audioSource.isPlaying) {
            isPlayingBGM = false;
            play_bgm (bgmType);
        }
    }

    public void play_sfx (string _sound) {
        if (lastPlayedTime.ContainsKey(_sound) && Time.time - lastPlayedTime[_sound] < soundCooldown) {
            return;
        }

        AudioClip clipToPlay = null;
        switch (_sound) {
            case "yes-ping": clipToPlay = yesPing; break;
            case "no-ping": clipToPlay = noPing; break;

            case "bubble": clipToPlay = bubble; break;
            case "buying": clipToPlay = buying; break;
            case "chat": clipToPlay = chat; break;
            case "click": clipToPlay = click; break;
            case "explosion": clipToPlay = explosion; break;
            case "big-hit": clipToPlay = bigHit; break;
            case "gain": clipToPlay = gain; break;
            case "ice": clipToPlay = ice; break;
            case "laser": clipToPlay = laser; break;
            case "magic": clipToPlay = magic; break;
            case "plasma-shotgun": clipToPlay = plasmaShotgun; break;
            case "swish": clipToPlay = swish; break;
            case "swoosh": clipToPlay = swoosh; break;
            case "torrent": clipToPlay = torrent; break;
            case "win": clipToPlay = win; break;
            case "zap": clipToPlay = zap; break;
            case "dash": clipToPlay = dash; break;
            case "dash-smoke": clipToPlay = dashSmoke; break;
        }

        if (clipToPlay != null) {
            audioSource.PlayOneShot(clipToPlay);
            lastPlayedTime[_sound] = Time.time;
        }
    }

    public void play_bgm (string _music) {
        //if (isPlayingBGM && _music == bgmType) return;
        audioSource.Stop ();

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
