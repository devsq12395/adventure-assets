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

    // Add fields for the white bars
    [SerializeField] private Image i_HPWhite;
    [SerializeField] private Image i_StaWhite;
    [SerializeField] private Image i_UltWhite;

    public InGameObject boss;

    public void setup() {
        // Instantiate white bars for HP, Stamina, and Ultimate
        i_HPWhite = Instantiate(i_HPMain, i_HPMain.transform.parent);
        i_HPWhite.color = new Color(1f, 1f, 1f, 0.5f); // Semi-transparent white
        i_HPWhite.transform.SetSiblingIndex(0);

        i_StaWhite = Instantiate(i_StaMain, i_StaMain.transform.parent);
        i_StaWhite.color = new Color(1f, 1f, 1f, 0.5f);
        i_StaWhite.transform.SetSiblingIndex(0);

        i_UltWhite = Instantiate(i_UltMain, i_UltMain.transform.parent);
        i_UltWhite.color = new Color(1f, 1f, 1f, 0.5f);
        i_UltWhite.transform.SetSiblingIndex(0);

        set_char(ContPlayer.I.players[0].name);
    }

    public void set_char(string _charName) {
        DB_Chars.CharData _data = DB_Chars.I.get_char_data(_charName);
        t_name.text = _data.nameUI;
        i_port.sprite = Sprites.I.get_sprite(_data.imgPort);
        i_portShadow.sprite = Sprites.I.get_sprite(_data.imgPort);
    }

    private float tweenDuration = 0.5f;
    private float hpTweenDelay = 0.8f;
    private float hpTweenElapsed = 0f;
    private float hpTargetFill;
    private bool hpTweening = false;

    private float prevHpScale = -1f;
    private float prevStaScale = -1f;
    private float prevUltScale = -1f;

    private void Start() {
    }

    public void update_bars() {
        InGameObject _pla = ContPlayer.I.player;

        if (!_pla) return;

        float hpScale = (float)_pla.hp / (float)_pla.hpMax,
                staScale = (float)ContPlayer.I.sta / (float)ContPlayer.I.staMax,
                ultScale = (float)_pla.ultPerc / 100;

        // Trigger tween only if the value has changed
        if (Mathf.Abs(hpScale - prevHpScale) > 0.01f) {
            i_HPMain.fillAmount = hpScale;
            hpTargetFill = hpScale;
            hpTweenElapsed = 0f;
            hpTweening = true;
            prevHpScale = hpScale;
        }

        if (Mathf.Abs(staScale - prevStaScale) > 0.01f) {
            i_StaMain.fillAmount = staScale;
            prevStaScale = staScale;
        }

        if (Mathf.Abs(ultScale - prevUltScale) > 0.01f) {
            i_UltMain.fillAmount = ultScale;
            prevUltScale = ultScale;
        }

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
            iBossMain.transform.SetSiblingIndex(0);  // Set boss HP bar behind all siblings
            show_boss_hp_bar();
        }

        if (hpTweening) {
            hpTweenElapsed += Time.deltaTime;
            if (hpTweenElapsed >= hpTweenDelay) {
                i_HPWhite.fillAmount = Mathf.Lerp(i_HPWhite.fillAmount, hpTargetFill, Time.deltaTime / tweenDuration);
                if (Mathf.Abs(i_HPWhite.fillAmount - hpTargetFill) < 0.01f) {
                    i_HPWhite.fillAmount = hpTargetFill;
                    hpTweening = false;
                }
            }
        }
    }

    public void find_boss_on_start (){
        InGameObject[] allObjects = FindObjectsOfType<InGameObject>();
        foreach (var obj in allObjects) {
            if (obj.tags.Contains("boss")) {
                set_boss(obj);
                break;
            }
        }
    }

    public void show_boss_hp_bar (){
        if (boss != null) {
            InGameObject player = ContPlayer.I.player;
            float distance = Vector2.Distance(boss.transform.position, player.transform.position);
            go_bossHP.SetActive(distance <= 24f);
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
