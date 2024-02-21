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

    private readonly string[] equipStrs = {"weapon", "armor", "boots", "accessory1", "accessory2"};
    private readonly string[] statStrs = {"hp", "mp", "attack", "skill", "speed"};

    public void setup (){
        go.SetActive (true);

        go.SetActive (false);
    }

    public void setup_char (string _name){
        name = _name;

        cName.text = MM_Strings.I.get_str ($"{_name}-name");
        cLvl.text = $"{MM_Strings.I.get_str ("lvl")} {MM_Save.I.load ($"chars.{_name}.level")}";

        string _strAllStats = "";
        foreach (string _statStr in statStrs) {
            _strAllStats += $"{MM_Strings.I.get_str (_statStr)}: {MM_Save.I.load ($"chars.{_name}.stats.{_statStr}")}\n";
        }
        cStats.text = _strAllStats;

        create_equip_items ();
    }

    public void create_equip_items (){
        foreach (string _equipStr in equipStrs) {
            string _item = MM_Save.I.load ($"chars.{name}.equip.{_equipStr}");

            MM_Inventory.Item _new = new MM_Inventory.Item (_item, 1);
            items.Add (_new);

            GameObject _newItemUI = Instantiate(goItemObj, goCanvas.transform);
            RectTransform _transform = _newItemUI.GetComponent<RectTransform>();

            TextMeshProUGUI     _tName = _newItemUI.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                                _tStack = _newItemUI.transform.Find("Stack").GetComponent<TextMeshProUGUI>();

            _tName.text = _new.name;
            _tStack.text = "";

            _transform.anchoredPosition = new Vector2(-220 + _column * 440, -27 + _row * -58);
        }
    }

}