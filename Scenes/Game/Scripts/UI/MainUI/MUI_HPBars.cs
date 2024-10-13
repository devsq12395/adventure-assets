using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MUI_HPBars : MonoBehaviour
{
    public static MUI_HPBars I;
    public void Awake() { I = this; }

    public GameObject go_bossHP, go_arrowSkill, go_arrowUlt;
    public Image i_port, i_portShadow, i_portBoss, i_portBossShadow, i_HPMain, i_StaMain, i_UltMain, iBossMain, i_CDPanel1, i_CDPanel2;
    public TextMeshProUGUI t_name, t_hp, t_mp, t_bossName, t_cdSkill1, t_cdSkill2;
    public Sprite i_skillNotReady, i_skillReady;

    public InGameObject boss;

    public void setup() {
        set_char(ContPlayer.I.players[0].name);
    }

    public void set_char(string _charName) {
        DB_Chars.CharData _data = DB_Chars.I.get_char_data(_charName);
        t_name.text = _data.nameUI;
        i_port.sprite = Sprites.I.get_sprite(_data.imgPort);
        i_portShadow.sprite = Sprites.I.get_sprite(_data.imgPort);
    }

    public void update_bars() {
        InGameObject _pla = ContPlayer.I.player;

        if (!_pla) return;

        float   hpScale = (float)_pla.hp / (float)_pla.hpMax,
                staScale = (float)ContPlayer.I.sta / (float)ContPlayer.I.staMax,
                ultScale = (float)_pla.ultPerc / 100;

        // Update the HP and MP bars using fillAmount
        set_bar_fill(i_HPMain, hpScale);
        set_bar_fill(i_StaMain, staScale);
        set_bar_fill(i_UltMain, ultScale);

        t_hp.text = $"{_pla.hp} / {_pla.hpMax}";

        t_cdSkill1.text = $"SKILL - {((_pla.skill1.cd > 0) ? $"{(int)(_pla.skill1.cd + 1)} SEC." : "READY")}";
        t_cdSkill2.text = $"ULTIMATE - {((_pla.ultPerc < 100) ? $"{_pla.ultPerc}%" : "READY")}";

        bool skillReady = _pla.skill1.cd > 0;
        i_CDPanel1.color = (skillReady) ? Color.red : Color.green;
        go_arrowSkill.SetActive (!skillReady);

        bool ultReady = _pla.ultPerc >= 100;
        i_CDPanel2.color = (ultReady) ? Color.green : Color.red;
        go_arrowUlt.SetActive (ultReady);

        if (boss != null) {
            float hpScaleBoss = (float)boss.hp / (float)boss.hpMax;
            set_bar_fill(iBossMain, hpScaleBoss);
        }
    }

    public void set_boss(InGameObject _boss) {
        go_bossHP.SetActive(true);
        boss = _boss;
        t_bossName.text = _boss.nameUI;
        i_portBoss.sprite = Sprites.I.get_sprite(_boss.name);
        i_portBossShadow.sprite = Sprites.I.get_sprite(_boss.name);
    }

    // Helper function to set the fill amount of an Image
    private void set_bar_fill(Image _bar, float _fillAmount) {
        _bar.fillAmount = _fillAmount;
    }
}
