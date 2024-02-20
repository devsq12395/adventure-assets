using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_ChangeParty : MonoBehaviour {
    public static MM_ChangeParty I;
	public void Awake(){ I = this; }

    public GameObject go;

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
        go.SetActive (_isShow);
        isShow = _isShow;

        setup_show ();
    }

    public void setup_show (){
        if (!isShow) return;

        setup_buttons ();
    }

    private void setup_buttons (){
        string _name;

        strList_lineup.Clear ();
        string[] _lineup = MM_Save.I.load ("lineup").Split (',');
        strList_lineup.AddRange (_lineup);

        for (int i = 0; i < mainLineup.Count; i++) {
            _name = strList_lineup [i];

            if (_name != "") {
                setup_button (
                    mainLineup [i], 
                    MM_Sprites.I.get_sprite (_name),
                    MM_Strings.I.get_str ($"{_name}-name"),
                    $"{MM_Strings.I.get_str ("lvl")} {MM_Save.I.load ($"chars.{_name}.level")}"
                );
            } else {
                setup_button (
                    mainLineup [i],
                    MM_Sprites.I.get_sprite ("empty"),
                    "",
                    ""
                );
            }
        }

        strList_heroPool.Clear ();
        string[] _pool = MM_Save.I.load ("pool").Split (',');
        strList_heroPool.AddRange (_pool);

        for (int i = 0; i < heroPool.Count; i++) {
            if (i >= _pool.Length){
                go_heroPool [i].SetActive (false);
                break;
            }

            _name = _pool [i];
            go_heroPool [i].SetActive (true);

            if (_name != "") {
                setup_button (
                    heroPool [i],
                    MM_Sprites.I.get_sprite (_name),
                    "",
                    $"{MM_Strings.I.get_str ("lvl")} {MM_Save.I.load ($"chars.{_name}.level")}"
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
        lineupSel = _ind;
    }

    public void btn_pool (int _ind){
        if (strList_lineup.Contains (strList_heroPool [_ind])) {
            return;
        }

        strList_lineup [lineupSel] = strList_heroPool [_ind];
        string _name = strList_lineup [lineupSel];

        setup_button (
            mainLineup [lineupSel],
            MM_Sprites.I.get_sprite (_name),
            MM_Strings.I.get_str ($"{_name}-name"),
            $"{MM_Strings.I.get_str ("lvl")} {MM_Save.I.load ($"chars.{_name}.level")}"
        );
    }

    public void btn_return (){
        MM_Save.I.save ("lineup", string.Join(",", go_mainLineup));
        toggle_show (false);
    }
}
