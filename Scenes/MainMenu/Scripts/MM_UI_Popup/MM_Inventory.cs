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
        public string name; public int stack;

        public Item (string _name, int _stack) {
            name = _name; stack = _stack;
        }
    }
    public struct ItemUI {
        public GameObject go; public TextMeshProUGUI name; public TextMeshProUGUI stack;

        public ItemUI (GameObject _go, TextMeshProUGUI _name, TextMeshProUGUI _stack) {
            go = _go; name = _name; stack = _stack;
        }
    }

    public List<Item> items;
    public List<ItemUI> itemUIs;

    public void setup (){
        setup_items ();
    }

    public void show (){
        go.SetActive (true);
    }

    private void setup_items (){
        items = new List<Item> ();
        string[]    _itemsStr = MM_Save.I.load ("items").Split ('^'),
                    _itemExtracted;
        Item _new;

        itemUIs = new List<ItemUI> ();

        for (int _i = 0; _i < _itemsStr.Length; _i++) {
            _itemExtracted = _itemsStr [_i].Split ('%');
            Debug.Log ($"{_itemExtracted [1]}, {_itemExtracted [0]}");
            create_item (_itemExtracted [1], _itemExtracted [0]);
        }
    }

    private void create_item (string _item, string _stack){ 
        // Create "Item" object
        Item _new = new Item (_item, int.Parse (_stack));
        items.Add (_new);

        // Create the UI
        GameObject _newItemUI = Instantiate(goItem, goCanvas.transform);
        RectTransform _transform = _newItemUI.GetComponent<RectTransform>();

        int _index = items.Count - 1,
            _column = _index % 2,
            _row = _index / 2;

        _transform.anchoredPosition = new Vector2(-220 + _column * 440, -27 + _row * -58);

        TextMeshProUGUI _tName = _newItemUI.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                        _tStack = _newItemUI.transform.Find("Stack").GetComponent<TextMeshProUGUI>();
        _tName.text = _new.name;
        _tStack.text = _new.stack.ToString ();

        ItemUI _newUI = new ItemUI(_newItemUI, _tName, _tStack);
        itemUIs.Add(_newUI);
    }

}