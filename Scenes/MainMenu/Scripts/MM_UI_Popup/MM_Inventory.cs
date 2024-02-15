using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Inventory : MonoBehaviour {
    public static MM_Inventory I;
	public void Awake(){ I = this; }

    public GameObject go, goItem, goCanvas;

    public struct Item {
        string name; int stack;

        public Item (string _name, int _stack) {
            name = _name; stack = _stack;
        }
    }
    private struct ItemUI {
        GameObject go; TextMeshProUGUI name; TextMeshProUGUI stack;

        public Item (GameObject _go, TextMeshProUGUI _name, TextMeshProUGUI _stack) {
            go = _go; name = _name; stack = _stack;
        }
    }

    public List<Item> items;
    public List<ItemUI> itemUIs;

    public void setup (){
        setup_items ();
    }

    private void setup_items (){
        items = new List<Item> ();
        string[] _items = MM_Save.I.load ("items").Split ('^');
        items.AddRange (_items);

        itemUIs = new List<ItemUI> ();
        for (int _i = 0; _i < items.Count; _i++) {
            create_item (_i);
        }
    }

    private void create_item (int _index){
        GameObject _newItemUI = Instantiate(goItem, goCanvas.transform);
        RectTransform _transform = _newItemUI.GetComponent<RectTransform>();

        _transform.anchoredPosition = new Vector2(-110f - 100f * items.Count, -118f);

        TextMeshProUGUI _tName = _newCharUI.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                        _tStack = _newCharUI.transform.Find("Stack").GetComponent<TextMeshProUGUI>();

        ItemUI _newUI = new ItemUI(_newItemUI, _tName, _tStack);
        itemUIs.Add(_newUI);
    }

}