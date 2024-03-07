using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MUI_HPBars : MonoBehaviour
{
    public static MUI_HPBars I;
    public void Awake() { I = this; }

    public Image i_HPMain, i_MPMain;
    public TextMeshProUGUI t_hp, t_mp;

    public void update_bars() {
        InGameObject _pla = ContPlayer.I.player;

        if (!_pla) return;

        float hpScale = (float)_pla.hp / (float)_pla.hpMax;
        float mpScale = (float)_pla.mp / (float)_pla.mpMax;

        i_HPMain.rectTransform.anchorMin = new Vector2(0f, 0.5f);
        i_HPMain.rectTransform.anchorMax = new Vector2(0f, 0.5f);
        i_HPMain.rectTransform.pivot = new Vector2(0f, 0.5f);

        i_HPMain.rectTransform.localScale = new Vector3(hpScale, 1f, 1f);

        i_MPMain.rectTransform.anchorMin = new Vector2(0f, 0.5f);
        i_MPMain.rectTransform.anchorMax = new Vector2(0f, 0.5f);
        i_MPMain.rectTransform.pivot = new Vector2(0f, 0.5f);
        
        i_MPMain.rectTransform.localScale = new Vector3(mpScale, 1f, 1f);

        t_hp.text = $"{_pla.hp} / {_pla.hpMax}";
        t_mp.text = $"{_pla.mp} / {_pla.mpMax}";
    }
}
