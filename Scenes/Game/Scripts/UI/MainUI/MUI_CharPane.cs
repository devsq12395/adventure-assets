using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MUI_CharPane : MonoBehaviour
{
    public static MUI_CharPane I;
    public void Awake() { I = this; }

    public GameObject goUI, goCanvas;

    public struct Char {
        public GameObject go;
        public Image port, hpBar, mpBar;

        public int index;

        public Char(int _ind, GameObject _go, Image _port, Image _hp, Image _mp){
            index = _ind;
            go = _go;
            port = _port;
            hpBar = _hp;
            mpBar = _mp;
        }
    };

    public GameObject go;
    public List<Char> chars;

    public void setup (){
        go.SetActive (true);

        chars = new List<Char>();
    }

    public void update (){
        update_chars_hp_bars ();
    }

    public void create_char (int _num){
        GameObject _newCharUI = Instantiate(goUI, goCanvas.transform);
        RectTransform _transform = _newCharUI.GetComponent<RectTransform>();

        _transform.anchoredPosition = new Vector2(-110f, -118f - 75f * chars.Count);

        Image portImage = _newCharUI.transform.Find("Port").GetComponent<Image>();
        Image hpBarImage = _newCharUI.transform.Find("HPBar").transform.Find("BarValue_HP").GetComponent<Image>();
        Image mpBarImage = _newCharUI.transform.Find("MPBar").transform.Find("BarValue_MP").GetComponent<Image>();

        Char _newChar = new Char(_num, _newCharUI, portImage, hpBarImage, mpBarImage);

        _lineup = Enumerable.Range(1, 4).Select(i => ZPlayerPrefs.GetString($"lineup-{i}")).ToList();
        string _charName = _lineup [_num];
        portImage.sprite = Sprites.I.get_sprite (_charName);
        
        chars.Add(_newChar);
    }

    public void update_chars_hp_bars (){
        for (int _pi = 0; _pi < ContPlayer.I.players.Count; _pi++) {
            float   _cHP = ContPlayer.I.players[_pi].hp,
                    _cHPMax = ContPlayer.I.players[_pi].hpMax;
            float _normHP = Mathf.Clamp01(_cHP / _cHPMax);
            
            RectTransform _tranform = chars[_pi].hpBar.rectTransform;
            float _width = 46f;
            float _newWidth = _width * _normHP;
            _tranform.sizeDelta = new Vector2(_newWidth, _tranform.sizeDelta.y);
        }
    }
}
