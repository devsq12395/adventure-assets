using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public void play_click() {
        if (!SoundHandler.I.soundOn) return;
        SoundHandler.I.play_sfx("click");
    }
}