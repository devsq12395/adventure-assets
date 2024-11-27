using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Settings : MonoBehaviour {

    public TextMeshProUGUI tSound, tMusic;

    public void show (){
        tSound.text = $"Sound: {(SoundHandler.I.soundOn ? "ON" : "OFF")}";
        tMusic.text = $"Music: {(SoundHandler.I.musicOn ? "ON" : "OFF")}";
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
}