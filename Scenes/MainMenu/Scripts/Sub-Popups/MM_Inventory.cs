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
    public TextMeshProUGUI tPage, tGold;

    [Header("List of Modes: inventory, equip, sell, buy")]
    public string mode;

    public int MAX_ITEMS_IN_PAGE;
    public int page, pageMax;

    public struct Item {
        public string name; public int stack; public int index;

        public Item (string _name, int _stack, int _index) {
            name = _name; stack = _stack; index = _index;
        }
    }
    public struct ItemUI {
        public GameObject go; public TextMeshProUGUI name; public TextMeshProUGUI stack;

        public ItemUI (GameObject _go, TextMeshProUGUI _name, TextMeshProUGUI _stack) {
            go = _go; name = _name; stack = _stack;
        }
    }

    public List<Item> items, itemsMain;
    public List<ItemUI> itemUIs;

    private string itemListName;

    public void setup (){
        MAX_ITEMS_IN_PAGE = 12;
        items = new List<Item>();
        itemUIs = new List<ItemUI> ();

        go.SetActive (true);

        // Setup main item inventory
        itemsMain = new List<Item> ();
        string[]    _itemsStr = JsonSaving.I.load ("items").Split ('^'), 
                    _itemExtracted;

        for (int _i = 0; _i < _itemsStr.Length; _i++) {
            _itemExtracted = _itemsStr [_i].Split ('%');
            Item _new = new Item (_itemExtracted [1], int.Parse (_itemExtracted [0]), itemsMain.Count);
            itemsMain.Add (_new);
        }

        go.SetActive (false);
    }

    public void btn_show (){show ("inventory", "");}
    public void show (string _mode, string _itemList, string[] _sortTag = null){
        go.SetActive (true);
        mode = _mode;
        setup_items (_itemList, _sortTag);
        refresh_item_btns (_sortTag);
    }

    public void hide (){
        foreach (ItemUI _item in itemUIs) {
            Destroy (_item.go);
        }
        itemUIs.Clear ();

        go.SetActive (false);
    }

    private void setup_items (string _itemList, string[] _sortTag = null){
        itemListName = _itemList;
        items.Clear ();

        if (_itemList == ""){
            // Load main inventory
            foreach (Item _item in itemsMain) {
                if (_sortTag != null) {
                    string[] _tags = JsonReading.I.read ("items", $"items.{_item.name}.tags").Split (',');
                    if (!_sortTag.All ((_tag) => _tags.Contains (_tag))) continue;
                }
                create_item (_item.name, _item.stack.ToString ());
            }
        } else {
            // Load other item list
            string[]    _itemsStr = JsonReading.I.read ("items", $"shop-inventory.{_itemList}").Split (','),
                        _itemExtracted;
            Item _new;

            for (int _i = 0; _i < _itemsStr.Length; _i++) {
                create_item (_itemsStr [_i], "0");
            }
        }

        page = 0;
        pageMax = (int)((items.Count - 1) / 12);
        tPage.text = $"{page + 1}/{pageMax + 1}";
    }

    private void refresh_item_btns (string[] _sortTag = null){
        tGold.text = $"{JsonReading.I.get_str ("gold")}: {JsonSaving.I.load ("gold")}";

        int _itemPage = page * MAX_ITEMS_IN_PAGE;

        foreach (ItemUI _item in itemUIs) {
            Destroy (_item.go);
        }
        itemUIs.Clear ();

        List<Item> _items = new List<Item>();
        _items.AddRange (items);
        for (int i = 0; i < MAX_ITEMS_IN_PAGE; i++) {
            if (_itemPage + i >= _items.Count) return;

            // Create the UI
            GameObject _newItemUI = Instantiate(goItemObj, goCanvas.transform);
            RectTransform _transform = _newItemUI.GetComponent<RectTransform>();

            int _column = i % 2,
                _row = i / 2;

            _transform.anchoredPosition = new Vector2(-220 + _column * 440, -27 + _row * -58);

            TextMeshProUGUI _tName = _newItemUI.transform.Find("Name").GetComponent<TextMeshProUGUI>(),
                            _tStack = _newItemUI.transform.Find("Stack").GetComponent<TextMeshProUGUI>();
            _tName.text = JsonReading.I.read ("items", $"items.{_items [_itemPage + i].name}.name-ui");
            switch (mode) {
                case "buy": _tStack.text = $"{JsonReading.I.read ("items", $"items.{_items [_itemPage + i].name}.cost").ToString ()}"; break;
                default: _tStack.text = $"x{_items [_itemPage + i].stack.ToString ()}"; break;
            }
            if (_tStack.text == "0") _tStack.text = "";

            ItemCheck _comp = _newItemUI.GetComponent<ItemCheck> ();
            _comp.itemIndex = i;

            ItemUI _newUI = new ItemUI(_newItemUI, _tName, _tStack);
            itemUIs.Add(_newUI);
        }
    }
    
    /*
        INTERACTABLE FUNCTIONS
        - Button functions
        - Other that interacts with the main items list, or loaded items list
    */

    public void btn_prev () {change_page (-1);}
    public void btn_next () {change_page (1);}
    private void change_page (int _inc){
        page = Mathf.Clamp(page + _inc, 0, pageMax);
        tPage.text = $"{page + 1}/{pageMax + 1}";
        refresh_item_btns ();
    }

    public void check_item (int _btnIndex) {
        MM_ItemCheck.I.show (items [page * MAX_ITEMS_IN_PAGE + _btnIndex], mode);
    }

    // This is used by the loaded item list, not the main list
    private void create_item (string _item, string _stack){ 
        Item _new = new Item (_item, int.Parse (_stack), items.Count);
        items.Add (_new);
    }

    // Saves the main list to json
    public void save_all_items_to_json (){
        List<string> _itemsToJoin = new List<string>();
        foreach (Item _item in itemsMain) {
            _itemsToJoin.Add ($"{_item.stack}%{_item.name}");
        }

        string _joined = string.Join ("^", _itemsToJoin.ToArray ());
        JsonSaving.I.save ("items", _joined);
    }

    // The rest of these functions will be used to interact with the main list
    public void add_item (string _item, int _stack){
        bool _stackable = JsonReading.I.read ("items", $"items.{_item}.stackable") == "true";

        if (_stackable && has_item_from_inv (_item)) {
            Item _itemFromInv = get_item_from_inv (_item);

            _itemFromInv.stack += _stack;
            itemsMain [_itemFromInv.index] = _itemFromInv;
        } else {
            for (int i = 1; i <= _stack; i++) {
                Item _new = new Item (_item, 1, itemsMain.Count);
                itemsMain.Add (_new);
            }
        }

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public void remove_item (int _index){
        itemsMain.RemoveAt (_index);

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public void remove_stack (MM_Inventory.Item _item, int _stack) {
        _item.stack -= _stack;
        if (_item.stack <= 0) {
            itemsMain.RemoveAt (_item.index);
        } else {
            itemsMain [_item.index] = _item;
        }

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    private bool has_item_from_inv (string _itemName) {
        foreach (Item _item in itemsMain) {
            if (_item.name == _itemName) return true;
        }
        return false;
    }

    private Item get_item_from_inv (string _itemName) {
        foreach (Item _item in itemsMain) {
            if (_item.name == _itemName) return _item;
        }
        return itemsMain [0];
    }
}