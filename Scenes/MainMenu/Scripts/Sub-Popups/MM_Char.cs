using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Char : MonoBehaviour {
    public static MM_Char I;
	public void Awake(){ I = this; }

    public GameObject go;
    public List<GameObject> goWindows;
    public List<Button> btnTabs;

    public Image i_port, i_sprite;
    public TextMeshProUGUI t_name, t_desc, t_bio, t_skill1, t_skill2;
    public List<TextMeshProUGUI> t_equips;
    public List<Image> i_equips;

    public Sprite i_tabSelected, i_tabUnselected;

    public int curTabIndex;
    public string curChar;

    public void setup (){

    }

    public void set_show (bool _isShow, string _char){
        go.SetActive (_isShow);
        curChar = _char;
        change_tab (0);

        DB_Chars.CharData _charData = DB_Chars.I.get_char_data (_char);
        t_name.text = _charData.nameUI;
        t_desc.text = _charData.desc;
        i_port.sprite = Sprites.I.get_sprite (_charData.imgPort);
        i_sprite.sprite = Sprites.I.get_sprite ($"{_charData.imgPort}-sprite");

        t_bio.text = _charData.bioInfo;
        t_skill1.text = _charData.bioSkill1;
        t_skill2.text = _charData.bioSkill2;
    }

    public void hide (){set_show (false, "");}

    public void change_tab (int _ind){
        curTabIndex = _ind;
        goWindows.ForEach ((window) => window.SetActive (false));
        btnTabs.ForEach ((tab) => tab.image.sprite = i_tabUnselected);

        goWindows [_ind].SetActive (true);
        btnTabs [_ind].image.sprite = i_tabSelected;
    }

    public void set_equips() {
        t_equips.ForEach((txt) => txt.text = "Empty");
        i_equips.ForEach((img) => img.sprite = Sprites.I.get_sprite("empty"));

        List<string> equipStrList = Inv2.I.equipStrList;
        List<Inv2.Item> equippedItems = Inv2.I.get_equipped_items(curChar);

        equippedItems.ForEach((item) => {
            Inv2.ItemData _itemData = Inv2.I.get_item_data(item.name);
            int _index = equipStrList.FindIndex(equipType => equipType == _itemData.equipType);

            if (_index != -1) {
                t_equips[_index].text = _itemData.nameUI; 
                i_equips[_index].sprite = _itemData.sprite;
            }
        });
    }

}