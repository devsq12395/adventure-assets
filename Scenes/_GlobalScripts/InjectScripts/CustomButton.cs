using UnityEngine;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public void play_click() {
        SoundHandler.I.play_sfx("click");
    }
}