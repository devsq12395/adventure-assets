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

    public TextMeshProUGUI cName, cLvl, cStats;

    public string name;
    public Dictionary<string, MM_Inventory.ItemUI> itemsUI;
    public bool isShow;

    private readonly string[] equipStrs = {"weapon", "armor", "boots", "accessory1", "accessory2"};
    private readonly string[] statStrs = {"hp", "mp", "attack", "skill", "speed"};

    public void setup (){
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

    private void hide (){
        for (int _i = equipStrs.Length - 1; _i >= 0; _i--) {
            Destroy (itemsUI [equipStrs [_i]].go);
        }
        itemsUI.Clear ();
        go.SetActive (false);
    }

    public void setup_char (string _name){
        name = _name;

        cName.text = MM_Strings.I.get_str ($"{_name}-name");
        cLvl.text = $"{MM_Strings.I.get_str ("lvl")} {JsonSaving.I.load ($"chars.{_name}.level")}";

        string _strAllStats = "";
        foreach (string _statStr in statStrs) {
            _strAllStats += $"{MM_Strings.I.get_str (_statStr)}: {JsonSaving.I.load ($"chars.{_name}.stats.{_statStr}")}\n";
        }
        cStats.text = _strAllStats;

        create_equip_items ();
    }

    public void create_equip_items (){
        itemsUI = new Dictionary<string, MM_Inventory.ItemUI> ();

        foreach (string _equipStr in equipStrs) {
            string _item = JsonSaving.I.load ($"chars.{name}.equip.{_equipStr}");

            MM_Inventory.Item _new = new MM_Inventory.Item (_item, 1);

            GameObject _newItemUI_go = Instantiate(goItemObj, goCanvas.transform);
            RectTransform _transform = _newItemUI_go.GetComponent<RectTransform>();

            TextMeshProUGUI     _tName = _newItemUI_go.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                                _tStack = _newItemUI_go.transform.Find("Stack").GetComponent<TextMeshProUGUI>();

            _tName.text = _new.name;
            _tStack.text = "";

            MM_Inventory.ItemUI _newUI = new MM_Inventory.ItemUI(_newItemUI_go, _tName, _tStack);
            // _transform.anchoredPosition = new Vector2(-220 + _column * 440, -27 + _row * -58);

            itemsUI.Add (_equipStr, _newUI);
        }
    }

    public void open_change_equip (){
        
    }

}