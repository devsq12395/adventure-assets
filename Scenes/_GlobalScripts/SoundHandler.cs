using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour {

    public static SoundHandler I;
    public void Awake(){ I = this; }

    public AudioSource audioSource;

    public AudioClip yesPing, noPing;
    public AudioClip bgmMenu, bgmGame;

    public void play_sfx (string _sound) {
        switch (_sound) {
            case "yes-ping": audioSource.PlayOneShot(yesPing); break;
            case "no-ping": audioSource.PlayOneShot(noPing); break;
        }
    }

    public void play_bgm (string _music) {
        AudioClip bgmClip = null;

        switch (_music) {
            case "menu": bgmClip = bgmMenu; break;
            case "game": bgmClip = bgmGame; break;
        }

        if (bgmClip != null) {
            audioSource.clip = bgmClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void stop_bgm (){
        audioSource.Stop ();
    }
}