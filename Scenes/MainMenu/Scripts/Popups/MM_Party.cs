using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Party : MonoBehaviour {
    public static MM_Party I;
	public void Awake(){ I = this; }

    public GameObject go;
    public List<GameObject> buttons;

    public Image imgWindow;

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
        lineup = Enumerable.Range(1, 4).Select(i => ZPlayerPrefs.GetString($"lineup-{i}")).ToList();

        setup_buttons ();
    }

    public void toggle_show (bool _isShow){ Debug.Log (_isShow);
        isShow = _isShow;

        if (_isShow) {
            go.SetActive (true);
            refresh_list ();
            imgWindow.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
            imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
        } else {
            imgWindow.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
        } 
    }

    public void setup_buttons (){
        if (!isShow) return;

        string _name;
        for (int i = 0; i < partyBtns.Count; i++) {
            _name = lineup [i];

            partyBtns [i].port.gameObject.SetActive (_name != "");
            if (_name != "") {
                DB_Chars.CharData _charData = DB_Chars.I.get_char_data (_name);
                partyBtns [i].port.sprite = Sprites.I.get_sprite (_charData.imgPort);
                partyBtns [i].name.text = _charData.nameUI;
                partyBtns [i].lvl.text = "";
            }
        }
    }
    
    public void btn_char (int _ind){
        MM_Char.I.set_show (true, lineup [_ind]);
    }
}
