using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Party : MonoBehaviour {
    public static MM_Party I;
	public void Awake(){ I = this; }

    public GameObject go;
    public List<GameObject> buttons;

    public struct PartyBtn {
        public Image port;
        public TextMeshProUGUI name, lvl;

        public PartyBtn (Image _port, TextMeshProUGUI _name, TextMeshProUGUI _lvl){ 
            port = _port; name = _name; lvl = _lvl; 
        }
    };

    public List<PartyBtn> partyBtns;
    public List<string> lineup;

    public bool isShow;

    public void setup (){
        partyBtns = new List<PartyBtn> ();
        lineup = new List<string> ();

        go.SetActive (true); isShow = true;

        for (int i = 0; i < buttons.Count; i++) {
            Image _img = buttons [i].GetComponent<Image> ();
            TextMeshProUGUI _txt1 = buttons [i].transform.Find ("BTN_Hero_Name").GetComponent<TextMeshProUGUI> (),
                            _txt2 = buttons [i].transform.Find ("BTN_Hero_LVL").GetComponent<TextMeshProUGUI> ();
            PartyBtn _new = new PartyBtn (_img, _txt1, _txt2);

            partyBtns.Add (_new);
        }

        refresh_list ();

        go.SetActive (false); isShow = false;
    }

    public void refresh_list (){
        lineup.Clear ();
        string[] _lineup = MM_Save.I.load ("lineup").Split (',');
        lineup.AddRange (_lineup);

        setup_buttons ();
    }

    public void setup_buttons (){
        if (!isShow) return;

        string _name;
        for (int i = 0; i < partyBtns.Count; i++) {
            _name = lineup [i];

            if (_name != "") {
                partyBtns [i].port.sprite = MM_Sprites.I.get_sprite (_name);
                partyBtns [i].name.text = MM_Strings.I.get_str ($"{_name}-name");
                partyBtns [i].lvl.text = $"{MM_Strings.I.get_str ("lvl")} {MM_Save.I.load ($"chars.{_name}.level")}";
            } else {
                partyBtns [i].port.sprite = MM_Sprites.I.get_sprite ("empty");
                partyBtns [i].name.text = "";
                partyBtns [i].lvl.text = "";
            }
        }
    }

    public void toggle_show (bool _isShow){
        isShow = _isShow;
        go.SetActive (_isShow);

        if (_isShow) {
            setup_buttons ();
        }
    }
}
