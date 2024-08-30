using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MUI_HPBars : MonoBehaviour
{
    public static MUI_HPBars I;
    public void Awake() { I = this; }

    public GameObject go_bossHP;
    public Image i_port, i_HPMain, i_MPMain, iBossMain, i_CDPanel1, i_CDPanel2;
    public TextMeshProUGUI t_name, t_hp, t_mp, t_bossName, t_cdSkill1, t_cdSkill2;

    public InGameObject boss;

    public void setup() {
        set_char(ContPlayer.I.players[0].name);
    }

    public void set_char(string _charName) {
        DB_Chars.CharData _data = DB_Chars.I.get_char_data(_charName);
        t_name.text = _data.nameUI;
        i_port.sprite = Sprites.I.get_sprite(_data.imgPort);
    }

    public void update_bars() {
        InGameObject _pla = ContPlayer.I.player;

        if (!_pla) return;

        float hpScale = (float)_pla.hp / (float)_pla.hpMax;

        // Update the HP and MP bars using fillAmount
        set_bar_fill(i_HPMain, hpScale);

        t_hp.text = $"{_pla.hp} / {_pla.hpMax}";

        t_cdSkill1.text = $"Q - {((_pla.skill1.cd > 0) ? $"{(int)(_pla.skill1.cd + 1)} sec." : "READY")}";
        // t_cdSkill2.text = $"E - {((_pla.skill2.cd > 0) ? $"{(int)(_pla.skill2.cd + 1)} sec." : "READY")}";

        i_CDPanel1.color = (_pla.skill1.cd > 0) ? Color.red : Color.green;
        // i_CDPanel2.color = (_pla.skill2.cd > 0) ? Color.red : Color.green;

        if (boss != null) {
            float hpScaleBoss = (float)boss.hp / (float)boss.hpMax;
            set_bar_fill(iBossMain, hpScaleBoss);
        }
    }

    public void set_boss(InGameObject _boss) {
        go_bossHP.SetActive(true);
        boss = _boss;
        t_bossName.text = _boss.nameUI;
    }

    // Helper function to set the fill amount of an Image
    private void set_bar_fill(Image _bar, float _fillAmount) {
        _bar.fillAmount = _fillAmount;
    }
}
