using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Char : MonoBehaviour {
    public static MM_Char I;
    public void Awake() { I = this; }

    public GameObject go;
    public List<GameObject> goWindows;
    public List<Button> btnTabs;

    public Image i_port, i_sprite;
    public TextMeshProUGUI t_name, t_desc, t_bio, t_skill1, t_skill2, t_statsCombat, t_statsOther;
    public List<TextMeshProUGUI> t_equips;
    public List<Image> i_equips;

    public Sprite i_tabSelected, i_tabUnselected;

    public int curTabIndex;
    public string curChar;

    public void setup () {

    }

    public void set_show(bool _isShow, string _char) {
        go.SetActive(_isShow);
        curChar = _char;
        change_tab(0);

        // Fetch character data
        DB_Chars.CharData _charData = DB_Chars.I.get_char_data(_char);
        t_name.text = _charData.nameUI;
        t_desc.text = _charData.desc;
        i_port.sprite = Sprites.I.get_sprite(_charData.imgPort);
        i_sprite.sprite = Sprites.I.get_sprite($"{_charData.imgPort}-sprite");

        t_bio.text = _charData.bioInfo;
        t_skill1.text = _charData.bioSkill1;
        t_skill2.text = _charData.bioSkill2;

        // Use cached stats from _charStats
        Dictionary<string, int> _charStats = StatCalc.I.get_all_stats_of_char(_char);

        // Access stats from the dictionary instead of calling get_stat multiple times
        t_statsCombat.text = $"HP: {(_charStats.ContainsKey("hp") ? _charStats["hp"] : 0)}\n" +
                             $"Attack: {(_charStats.ContainsKey("attack") ? _charStats["attack"] : 0)}\n" +
                             $"Skill: {(_charStats.ContainsKey("skill") ? _charStats["skill"] : 0)}\n" +
                             $"Crit Rate: {(_charStats.ContainsKey("crit-rate") ? _charStats["crit-rate"] : 0)}%\n" +
                             $"Crit Dam: {(_charStats.ContainsKey("crit-dam") ? _charStats["crit-dam"] : 0)}%";

        t_statsOther.text = $"Science: {(_charStats.ContainsKey("science") ? _charStats["science"] : 0)}\n" +
                            $"Magic: {(_charStats.ContainsKey("magic") ? _charStats["magic"] : 0)}\n" +
                            $"Driving: {(_charStats.ContainsKey("driving") ? _charStats["driving"] : 0)}\n" +
                            $"Espionage: {(_charStats.ContainsKey("espionage") ? _charStats["espionage"] : 0)}\n" +
                            $"Computers: {(_charStats.ContainsKey("computers") ? _charStats["computers"] : 0)}\n" +
                            $"Repair: {(_charStats.ContainsKey("repair") ? _charStats["repair"] : 0)}\n" +
                            $"Luck: {(_charStats.ContainsKey("luck") ? _charStats["luck"] : 0)}";

        set_equips();
    }

    public void hide () { set_show(false, ""); }

    public void change_tab(int _ind) {
        curTabIndex = _ind;
        // Manual loop to replace ForEach for goWindows
        for (int i = 0; i < goWindows.Count; i++) {
            goWindows[i].SetActive(false);
        }
        // Manual loop to replace ForEach for btnTabs
        for (int i = 0; i < btnTabs.Count; i++) {
            btnTabs[i].image.sprite = i_tabUnselected;
        }

        goWindows[_ind].SetActive(true);
        btnTabs[_ind].image.sprite = i_tabSelected;
    }

    public void set_equips() {
        // Manual loop to replace ForEach for t_equips
        for (int i = 0; i < t_equips.Count; i++) {
            t_equips[i].text = "Empty";
        }
        // Manual loop to replace ForEach for i_equips
        for (int i = 0; i < i_equips.Count; i++) {
            i_equips[i].sprite = Sprites.I.get_sprite("empty");
        }

        List<string> equipStrList = Inv2.I.equipStrList;
        List<Inv2.Item> equippedItems = Inv2.I.get_equipped_items(curChar);

        // Manual loop to replace ForEach for equippedItems
        for (int i = 0; i < equippedItems.Count; i++) {
            Inv2.Item item = equippedItems[i];
            Inv2_DB.ItemData _itemData = Inv2_DB.I.get_item_data(item.name);

            // Manually find the index (replacing FindIndex)
            int _index = -1;
            for (int j = 0; j < equipStrList.Count; j++) {
                if (equipStrList[j] == _itemData.equipTo) {
                    _index = j;
                    break;
                }
            }

            if (_index != -1) {
                t_equips[_index].text = _itemData.nameUI;
                i_equips[_index].sprite = _itemData.sprite;
            }
        }
    }

    public void btn_equip (int _ind) {
        MM_Inv2.I.itemSet = Inv2.I.equipStrList[_ind];
        if (MM_Inv2.I.itemSet == "weapon") {
            DB_Chars.CharData _charData = DB_Chars.I.get_char_data(curChar);
            MM_Inv2.I.itemSet = _charData.equipWeapon;
        }
        MM_Inv2.I.show("equip");
    }
}
