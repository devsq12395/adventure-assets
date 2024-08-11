using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Inventory : MonoBehaviour {


    /*
        2024/08/11 - As of New Horizons v.0.3, this script is unused
    */

    
    public static MM_Inventory I;
	public void Awake(){ I = this; }

    public GameObject go, goItemObj, goCanvas;
    public Image imgWindow;
    public TextMeshProUGUI tPage, tGold;

    [Header("List of Modes: inventory, equip, sell, buy")]
    public string mode;

    public int MAX_ITEMS_IN_PAGE;
    public int page, pageMax;

    public struct Item {
        public string name; public int stack; public int ID;

        public Item (string _name, int _stack, int _id) {
            name = _name; stack = _stack; ID = _id;
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

        refresh_main_items_list ();

        go.SetActive (false);
    }

    public void refresh_main_items_list (){
        // Setup main item inventory
        itemsMain = new List<Item> ();
        string[]    _itemsStr = JsonSaving.I.load ("items").Split (','), 
                    _itemExtracted;

        for (int _i = 0; _itemsStr.Length > 1 && _i < _itemsStr.Length; _i++) {
            _itemExtracted = _itemsStr [_i].Split ('%');
            Item _new = new Item (_itemExtracted [1], int.Parse (_itemExtracted [0]), int.Parse (_itemExtracted [2]));
            itemsMain.Add (_new);
        }
    }

    public void get_last_mission_item_rewards (){
        List<string> _rewards = new List<string>();
        string _rewardsStr = JsonSaving.I.load ("rewards");

        if (_rewardsStr.Length > 1) {
            _rewards.AddRange (JsonSaving.I.load ("rewards").Split (","));
            _rewards.ForEach ((_item) => {
                add_item (_item, 1);
            });
        }

        JsonSaving.I.save ("rewards", "");
    }

    public void btn_show (){show ("inventory", "");}
    public void show (string _mode, string _itemList, string[] _sortTag = null){
        go.SetActive (true);
        refresh_main_items_list ();

        imgWindow.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);       
        
        mode = _mode;
        setup_items (_itemList, _sortTag);
        refresh_item_btns (_sortTag);
    }

    public void hide (){
        foreach (ItemUI _item in itemUIs) {
            Destroy (_item.go);
        }
        itemUIs.Clear ();

        imgWindow.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
    }

    private void setup_items (string _itemList, string[] _sortTag = null){
        items.Clear ();

        if (_itemList == ""){
            // Load main inventory - Including sorted list
            foreach (Item _item in itemsMain) {
                if (_sortTag != null) {
                    string[] _tags = JsonReading.I.read ("items", $"items.{_item.name}.tags").Split (',');
                    if (!_sortTag.All ((_tag) => _tags.Contains (_tag))) continue;
                }
                create_item_sorted_inventory (_item.name, _item.stack.ToString ());
            }
        } else {
            // Load other item list - Shop
            string[]    _itemsStr = JsonReading.I.read ("items", $"shop-inventory.{_itemList}").Split (','),
                        _itemExtracted;
            Item _new;

            for (int _i = 0; _i < _itemsStr.Length; _i++) {
                create_item_shop (_itemsStr [_i], "0");
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
        Item _item = items [page * MAX_ITEMS_IN_PAGE + _btnIndex];
        if (mode == "buy" && JsonReading.I.read ("items", $"items.{_item.name}.requires").Length > 0) {
            MM_Craft.I.show (_item.name, "item");
        } else {
            MM_ItemCheck.I.show (_item, mode);
        }
    }

    // This is used by the loaded item list, not the main list
    private void create_item_shop (string _item, string _stack){ 
        Item _new = new Item (_item, int.Parse (_stack), items.Count);
        items.Add (_new);
    }
    private void create_item_sorted_inventory (string _item, string _stack){ 
        Item _itemActual = get_item_from_inv (_item);
        Item _new = new Item (_item, int.Parse (_stack), _itemActual.ID);
        items.Add (_new);
    }

    // Saves the main list to json
    public void save_all_items_to_json (){
        List<string> _itemsToJoin = new List<string>();
        foreach (Item _item in itemsMain) {
            _itemsToJoin.Add ($"{_item.stack}%{_item.name}%{_item.ID}");
        }

        string _joined = string.Join (",", _itemsToJoin.ToArray ());
        JsonSaving.I.save ("items", _joined);
    }

    // The rest of these functions will be used to interact with the main list
    public void add_item (string _item, int _stack){
        bool _stackable = JsonReading.I.read ("items", $"items.{_item}.stackable") == "true";

        if (_stackable && has_item_from_inv (_item)) {
            Item _itemFromInv = get_item_from_inv (_item);
            _itemFromInv.stack += _stack;

            remove_item (_itemFromInv.ID);
            add_item (_itemFromInv.name, _itemFromInv.stack);
        } else {
            if (_stackable) {
                Item _new = new Item (_item, _stack, Random.Range(1000000, 10000000));
                itemsMain.Add (_new);
            } else {
                for (int i = 1; i <= _stack; i++) {
                    Item _new = new Item (_item, 1, Random.Range(1000000, 10000000));
                    itemsMain.Add (_new);
                }
            }
        }

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public void remove_item (int _ID){
        for (int i = 0; i < itemsMain.Count; i++) {
            if (itemsMain [i].ID == _ID) { Debug.Log ("removing");
                itemsMain.RemoveAt (i);
                break;
            }
        }

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public void remove_stack (Item _item, int _stack) {
        _item.stack -= _stack;
        for (int i = 0; i < itemsMain.Count; i++) {
            if (itemsMain [i].ID == _item.ID) {
                if (_item.stack <= 0) {
                    itemsMain.RemoveAt (i);
                } else {
                    itemsMain [i] = _item;
                }
                break;
            }
        }

        save_all_items_to_json ();
        refresh_item_btns ();
    }

    public bool has_item_from_inv (string _itemName) {
        foreach (Item _item in itemsMain) {
            if (_item.name == _itemName) return true;
        }
        return false;
    }

    public Item get_item_from_inv (string _itemName) {
        foreach (Item _item in itemsMain) {
            if (_item.name == _itemName) return _item;
        }
        return itemsMain [0];
    }
}