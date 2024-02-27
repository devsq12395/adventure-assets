using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_Inventory : MonoBehaviour {
    public static MM_Inventory I;
	public void Awake(){ I = this; }

    public GameObject go, goItemObj, goCanvas;

    [Header("List of Modes: inventory, equip")]
    public string mode;

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
        go.SetActive (true);

        go.SetActive (false);
    }

    public void show (string _mode){
        go.SetActive (true);
        mode = _mode;
        setup_items ();
    }

    public void hide (){
        go.SetActive (false);
    }

    private void setup_items (){
        items = new List<Item> ();
        string[]    _itemsStr = JsonSaving.I.load ("items").Split ('^'),
                    _itemExtracted;
        Item _new;

        itemUIs = new List<ItemUI> ();

        for (int _i = 0; _i < _itemsStr.Length; _i++) {
            _itemExtracted = _itemsStr [_i].Split ('%');
            create_item (_itemExtracted [1], _itemExtracted [0]);
        }
    }

    public void save_all_items_to_json (){
        List<string> _itemsToJoin = new List<string>();
        foreach (Item _item in items) {
            _itemsToJoin.Add ($"{_item.stack}%{_item.name}");
        }

        string _joined = string.Join ("^", _itemsToJoin.ToArray ());
        JsonSaving.I.save ("items", _joined);
    }

    private void create_item (string _item, string _stack){ 
        // Create "Item" object
        Item _new = new Item (_item, int.Parse (_stack));
        items.Add (_new);

        // Create the UI
        GameObject _newItemUI = Instantiate(goItemObj, goCanvas.transform);
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

    public void add_item (string _item, int _stack){
        Item _new = new Item (_item, _stack);
        items.Add (_new);

        save_all_items_to_json ();
    }

    public void remove_item (int _index){
        items.RemoveAt (_index);

        save_all_items_to_json ();
    }

}