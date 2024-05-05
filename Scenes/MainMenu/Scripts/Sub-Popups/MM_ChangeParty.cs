using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_ChangeParty : MonoBehaviour {
    public static MM_ChangeParty I;
	public void Awake(){ I = this; }

    public GameObject go;
    public Image imgWindow;

    public struct CharBtn {
        public Image port;
        public TextMeshProUGUI name, lvl;
        public bool isLineup;

        public CharBtn (Image _port, TextMeshProUGUI _name, TextMeshProUGUI _lvl, bool _isLineup){ 
            port = _port; name = _name; lvl = _lvl; 
            isLineup = _isLineup;
        }
    };

    public List<GameObject> go_mainLineup, go_heroPool;
    public List<CharBtn> mainLineup, heroPool;

    public bool isShow;
    public int lineupSel;

    public List<string> strList_lineup, strList_heroPool;

    private Sequence twn_btnLineup_color;

    public void setup (){
        mainLineup = new List<CharBtn> ();
        heroPool = new List<CharBtn> ();

        strList_lineup = new List<string> ();
        strList_heroPool = new List<string> ();

        go.SetActive (true); isShow = true;

        for (int i = 0; i < go_mainLineup.Count; i++) {
            mainLineup.Add (get_char_btn (go_mainLineup [i], true));
        }
        for (int i = 0; i < go_heroPool.Count; i++) {
            heroPool.Add (get_char_btn (go_heroPool [i], false));
        }

        go.SetActive (false); isShow = false;

        lineupSel = -1;
    }

    private CharBtn get_char_btn (GameObject _go, bool _isLineup){
        Image _img = _go.GetComponent<Image> ();
        Transform   _txt1Go = _go.transform.Find ("BTN_Hero_Name"),
                    _txt2Go = _go.transform.Find ("BTN_Hero_LVL");

        TextMeshProUGUI _txt1 = (_txt1Go) ? _txt1Go.GetComponent<TextMeshProUGUI> () : null,
                        _txt2 = (_txt2Go) ? _txt2Go.GetComponent<TextMeshProUGUI> () : null;
        CharBtn _new = new CharBtn (_img, _txt1, _txt2, _isLineup);

        return _new;
    }

    public void toggle_show (bool _isShow){
        isShow = _isShow;

        if (isShow) {
            setup_show ();

            go.SetActive (true);
            imgWindow.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
            imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        } else {
            imgWindow.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
            MM_Party.I.refresh_list ();
        }
    }

    public void setup_show (){
        if (!isShow) return;

        setup_buttons ();
        change_hero_pool_btns_pool_color (true);
        lineupSel = -1;
    }

    private void setup_buttons (){
        string _name;

        strList_lineup.Clear ();
        string[] _lineup = JsonSaving.I.load ("lineup").Split (',');
        strList_lineup.AddRange (_lineup);

        for (int i = 0; i < mainLineup.Count; i++) {
            _name = strList_lineup [i];

            if (_name != "") {
                setup_button (
                    mainLineup [i], 
                    Sprites.I.get_sprite (_name),
                    MM_Strings.I.get_str ($"{_name}-name"),
                    $"{MM_Strings.I.get_str ("lvl")} {JsonSaving.I.load ($"chars.{_name}.level")}"
                );
            } else {
                setup_button (
                    mainLineup [i],
                    Sprites.I.get_sprite ("empty-icon"),
                    "",
                    ""
                );
            }
        }

        strList_heroPool.Clear ();
        string[] _pool = JsonSaving.I.load ("pool").Split (',');
        strList_heroPool.AddRange (_pool);

        for (int i = 0; i < heroPool.Count; i++) {
            if (i >= _pool.Length){
                go_heroPool [i].SetActive (false);
                continue;
            }

            _name = _pool [i];
            go_heroPool [i].SetActive (true);

            if (_name != "") {
                setup_button (
                    heroPool [i],
                    Sprites.I.get_sprite (_name),
                    "",
                    $"{MM_Strings.I.get_str ("lvl")} {JsonSaving.I.load ($"chars.{_name}.level")}"
                );
            }
        }
    }

    private void setup_button (CharBtn _btn, Sprite _sprite, string _name, string _lvl){
        _btn.port.sprite = _sprite;
        if (_btn.name) _btn.name.text = _name;
        _btn.lvl.text = _lvl;
    }

    public void btn_lineup (int _ind){
        if (lineupSel != -1) {
            stop_btn_color_tween (go_mainLineup [lineupSel].GetComponent<Button>(), twn_btnLineup_color);
        }
        lineupSel = _ind;

        change_all_btns_color (go_mainLineup, Color.gray);
        go_mainLineup [lineupSel].GetComponent<Button>().image.color = Color.white;

        twn_btnLineup_color = start_btn_color_tween (go_mainLineup [_ind].GetComponent<Button>());

        change_hero_pool_btns_pool_color (false);
    }

    public void btn_pool (int _ind){
        if (strList_lineup.Contains (strList_heroPool [_ind])) {
            SoundHandler.I.play_sfx ("no-ping");
            return;
        }

        strList_lineup [lineupSel] = strList_heroPool [_ind];
        string _name = strList_lineup [lineupSel];

        setup_button (
            mainLineup [lineupSel],
            Sprites.I.get_sprite (_name),
            MM_Strings.I.get_str ($"{_name}-name"),
            $"{MM_Strings.I.get_str ("lvl")} {JsonSaving.I.load ($"chars.{_name}.level")}"
        );

        stop_btn_color_tween (go_mainLineup [lineupSel].GetComponent<Button>(), twn_btnLineup_color);

        change_all_btns_color (go_mainLineup, Color.white);
        change_hero_pool_btns_pool_color (true);
        lineupSel = -1;

        SoundHandler.I.play_sfx ("yes-ping");
    }

    public void btn_return (){
        JsonSaving.I.save ("lineup", string.Join(",", strList_lineup)); 
        if (lineupSel != -1) {
            stop_btn_color_tween (go_mainLineup [lineupSel].GetComponent<Button>(), twn_btnLineup_color);
            lineupSel = -1;
        }
        toggle_show (false);
    }

    private Sequence start_btn_color_tween (Button _btn) {
        float _dur = 0.25f;
        Sequence _tween = DOTween.Sequence ();
        _tween.Append (_btn.image.DOColor (Color.white, _dur))
            .Append (_btn.image.DOColor (new Color (1f, 1f, 0.4f), _dur));
        _tween.SetLoops (-1, LoopType.Yoyo);
        return _tween;
    }

    private void stop_btn_color_tween (Button _btn, Sequence _tween) {
        _btn.image.color = Color.white;
        _tween.Kill ();
    }

    private void change_all_btns_color (List<GameObject> _btns, Color _color){
        foreach (GameObject _go in _btns) {
            _go.GetComponent <Button> ().image.color = _color;
        }
    }

    private void change_hero_pool_btns_pool_color (bool _grayAll){
        for (int i = 0; i < strList_heroPool.Count; i++) {
            go_heroPool[i].GetComponent<Button>().image.color = ((strList_lineup.Contains (strList_heroPool [i]) || _grayAll) ? Color.gray : Color.white);
        }
    }
}
