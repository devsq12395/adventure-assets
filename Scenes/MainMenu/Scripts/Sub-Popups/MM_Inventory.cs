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
    public TextMeshProUGUI tPage;

    [Header("List of Modes: inventory, equip, sell, buy")]
    public string mode;

    public int MAX_ITEMS_IN_PAGE;
    public int page, pageMax;

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
        MAX_ITEMS_IN_PAGE = 12;

        go.SetActive (true);

        go.SetActive (false);
    }

    public void btn_show (){show ("inventory", "");}
    public void show (string _mode, string _itemList){
        go.SetActive (true);
        mode = _mode;
        setup_items (_itemList);
        refresh_item_btns ();
    }

    public void hide (){
        foreach (ItemUI _item in itemUIs) {
            Destroy (_item.go);
        }
        itemUIs.Clear ();

        go.SetActive (false);
    }

    private void setup_items (string _itemList){
        items = new List<Item> ();
        string[]    _itemsStr = (_itemList == "") ? 
            JsonSaving.I.load ("items").Split ('^') : 
            JsonReading.I.read ("items", $"shop-inventory.{_itemList}").Split (',');
        string[] _itemExtracted;
        Item _new;

        itemUIs = new List<ItemUI> ();

        for (int _i = 0; _i < _itemsStr.Length; _i++) {
            if (_itemList == "") {
                // Player's inventory
                _itemExtracted = _itemsStr [_i].Split ('%');
                create_item (_itemExtracted [1], _itemExtracted [0]);
            } else {
                // Other item lists
                create_item (_itemsStr [_i], "0");
            }
        }

        page = 0;
        pageMax = (int)((_itemsStr.Length - 1) / 12);
        tPage.text = $"{page + 1}/{pageMax + 1}";
    }

    private void refresh_item_btns (){
        int _itemPage = page * MAX_ITEMS_IN_PAGE;

        foreach (ItemUI _item in itemUIs) {
            Destroy (_item.go);
        }
        itemUIs.Clear ();

        for (int i = 0; i < MAX_ITEMS_IN_PAGE; i++) {
            if (_itemPage + i >= items.Count) return;

            // Create the UI
            GameObject _newItemUI = Instantiate(goItemObj, goCanvas.transform);
            RectTransform _transform = _newItemUI.GetComponent<RectTransform>();

            int _column = i % 2,
                _row = i / 2;

            _transform.anchoredPosition = new Vector2(-220 + _column * 440, -27 + _row * -58);

            TextMeshProUGUI _tName = _newItemUI.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                            _tStack = _newItemUI.transform.Find("Stack").GetComponent<TextMeshProUGUI>();
            _tName.text = JsonReading.I.read ("items", $"items.{items [_itemPage + i].name}.name-ui");
            _tStack.text = items [_itemPage + i].stack.ToString ();
            if (_tStack.text == "0") _tStack.text = "";

            ItemCheck _comp = _newItemUI.GetComponent<ItemCheck> ();
            _comp.itemIndex = i;

            ItemUI _newUI = new ItemUI(_newItemUI, _tName, _tStack);
            itemUIs.Add(_newUI);
        }
    }

    public void btn_prev () {change_page (-1);}
    public void btn_next () {change_page (1);}
    private void change_page (int _inc){
        page = Mathf.Clamp(page + _inc, 0, pageMax);
        tPage.text = $"{page + 1}/{pageMax + 1}";
        refresh_item_btns ();
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
        int _stackInt;
        Item _new = new Item (_item, int.Parse (_stack));
        items.Add (_new);
    }

    public void add_item (string _item, int _stack){
        Item _new = new Item (_item, _stack);
        items.Add (_new);

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public void remove_item (int _index){
        items.RemoveAt (_index);

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public void check_item (int _btnIndex) {
        MM_ItemCheck.I.show (items [page * MAX_ITEMS_IN_PAGE + _btnIndex], mode);
    }

}