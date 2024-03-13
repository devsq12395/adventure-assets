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

    public TextMeshProUGUI cName, cDetails;

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

        string  _cName = MM_Strings.I.get_str ($"{_name}-name"),
                _cStats = string.Join("\n", statStrs.Select(_statStr => $"{MM_Strings.I.get_str(_statStr)}: {JsonSaving.I.load($"chars.{_name}.stats.{_statStr}")}"));
        cName.text = _cName;
        cDetails.text = _cStats;

        create_equip_items ();
    }

    public void create_equip_items (){
        itemsUI = new Dictionary<string, MM_Inventory.ItemUI> ();

        for (int i = 0; i < equipStrs.Length; i++) {
            string  _equipStr = equipStrs [i],
                    _item = JsonSaving.I.load ($"chars.{name}.equip.{_equipStr}");

            MM_Inventory.Item _new = new MM_Inventory.Item (_item, 1, 0);

            GameObject _newItemUI_go = Instantiate(goItemObj, goCanvas.transform);
            RectTransform _transform = _newItemUI_go.GetComponent<RectTransform>();

            TextMeshProUGUI     _tName = _newItemUI_go.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                                _tStack = _newItemUI_go.transform.Find("Stack").GetComponent<TextMeshProUGUI>();

            _tName.text = _new.name;
            _tStack.text = "";

            MM_Inventory.ItemUI _newUI = new MM_Inventory.ItemUI(_newItemUI_go, _tName, _tStack);
            _transform.anchoredPosition = new Vector2(66.2f, -33 - 42 * i);

            itemsUI.Add (_equipStr, _newUI);
        }
    }

    public void open_change_equip (string _equipSlot){
        
    }

}