using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Char : MonoBehaviour {
    public static MM_Char I;
	public void Awake(){ I = this; }

    public GameObject go, goItemObj, goCanvas;

    public TextMeshProUGUI cName, cDetails, cDetails2, cBio;

    public string name;
    public Dictionary<string, MM_Inventory.ItemUI> itemsUI;
    public bool isShow;

    private readonly string[]   equipStrs = {"weapon", "armor", "boots", "accessory1", "accessory2"},
                                statStrs = {"hp", "attack", "skill", "armor", "speed", "crit-rate", "crit-dam"},
                                bioStrs = {"info", "skill1", "skill2"};

    private string changeEquipSlot;

    public void setup (){
        itemsUI = new Dictionary<string, MM_Inventory.ItemUI> ();

        go.SetActive (true);

        go.SetActive (false);
    }

    public void toggle_show (bool _isShow, string _name = ""){
        isShow = _isShow;

        if (isShow) show (_name); else hide ();
    }

    private void show (string _name){
        go.SetActive (true);
        setup_char (_name);
    }

    public void hide (){
        clear_equip_items ();
        go.SetActive (false);
    }

    private void clear_equip_items (){
        if (itemsUI.Count <= 0) return;

        for (int _i = equipStrs.Length - 1; _i >= 0; _i--) {
            Destroy (itemsUI [equipStrs [_i]].go);
        }
        itemsUI.Clear ();
    }

    public void setup_char (string _name){
        name = _name;

        string  _cName = MM_Strings.I.get_str($"{_name}-name"),
                _cStats1 = "", _cStats2 = "";
        
        for (int i = 0; i < statStrs.Length; i++) {
            string _statToAdd = $"{JsonReading.I.get_str (statStrs [i])}: {StatCalc.I.get_stat(_name, statStrs [i]).ToString ()}\n";
            if (i < 5) {
                _cStats1 += _statToAdd;
            } else {
                _cStats2 += _statToAdd;
            }
        }

        string _cBio = string.Join ("\n\n", bioStrs.Select ((bio) => JsonReading.I.read ("chars", $"chars.{name}.bio.{bio}")));

        cName.text = _cName;
        cDetails.text = _cStats1;
        cDetails2.text = _cStats2;
        cBio.text = _cBio;

        create_equip_items ();
    }

    public void create_equip_items (){
        clear_equip_items ();

        for (int i = 0; i < equipStrs.Length; i++) {
            string  _equipStr = equipStrs [i],
                    _item = JsonSaving.I.load ($"chars.{name}.equip.{_equipStr}");

            MM_Inventory.Item _new = new MM_Inventory.Item (_item, 1, 0);

            GameObject _newItemUI_go = Instantiate(goItemObj, goCanvas.transform);
            RectTransform _transform = _newItemUI_go.GetComponent<RectTransform>();

            TextMeshProUGUI _tName = _newItemUI_go.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            _tName.text = (_new.name == "") ? "Empty" : JsonReading.I.read ("items", $"items.{_new.name}.name-ui");

            ItemEquip _itemEquip = _newItemUI_go.GetComponent<ItemEquip> ();
            _itemEquip.equipSlot = _equipStr;

            MM_Inventory.ItemUI _newUI = new MM_Inventory.ItemUI(_newItemUI_go, _tName, null);
            _transform.anchoredPosition = new Vector2(66.2f, -33 - 42 * i);

            itemsUI.Add (_equipStr, _newUI);
        }
    }

    public void examine_equipped (string _equipSlot) {
        changeEquipSlot = _equipSlot;
        string _oldEquipped = JsonSaving.I.load ($"chars.{name}.equip.{changeEquipSlot}");

        if (_oldEquipped == "") {
            open_change_equip ();
        } else {
            MM_Inventory.Item _equipped = new MM_Inventory.Item (_oldEquipped, 1, 0);
            MM_ItemCheck.I.show (_equipped, "check-equipped");
        }
    }

    public void open_change_equip (){
        List<string> _itemTags = new List<string>() {changeEquipSlot};
        if (changeEquipSlot == "weapon") _itemTags.Add (JsonSaving.I.load ($"chars.{name}.equipWeapon"));

        MM_Inventory.I.show ("equip", "", _itemTags.ToArray ());
    }

    public void change_equip (string _equipItem){
        string _oldEquipped = JsonSaving.I.load ($"chars.{name}.equip.{changeEquipSlot}");
        if (_oldEquipped != "") MM_Inventory.I.add_item (_oldEquipped, 1);

        JsonSaving.I.save ($"chars.{name}.equip.{changeEquipSlot}", _equipItem);

        setup_char (name);
    }
}