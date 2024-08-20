using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Inv2 : MonoBehaviour {
    public static Inv2 I;
    public List<Item> items = new List<Item>();

    public struct Item {
        public string name; public int stack; public int ID; public string equippedBy;

        public Item(string _name, int _stack, int _id, string _equippedBy = "") {
            name = _name; stack = _stack; ID = _id; equippedBy = _equippedBy;
        }
    }
    public struct ItemUI {
        public GameObject go; public TextMeshProUGUI name; public TextMeshProUGUI stack;

        public ItemUI(GameObject _go, TextMeshProUGUI _name, TextMeshProUGUI _stack) {
            go = _go; name = _name; stack = _stack;
        }
    }

    public List<string> equipStrList;

    public void Awake (){
        I = this;
        equipStrList = new List<string>(){"weapon", "armor", "boots", "accessory1", "accessory2"};
    }

    public void add_item(string _itemName, int _stack = 1) {
        Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_itemName);

        if (get_has_item (_itemName) && _data.stackable){
            Item _item = get_item_from_name (_itemName);
            _item.stack += _stack;
        } else {
            Item newItem = new Item (_itemName, _stack, Random.Range (1000, 10000000), "");
            items.Add (newItem);
        }

        save_items();
    }

    public Item get_item_from_id (int id) {
        load_items ();
        return items.FirstOrDefault(item => item.ID == id);
    }
    public Item get_item_from_name (string name) {
        load_items ();
        return items.FirstOrDefault(item => item.name == name);
    }
    public bool get_has_item (string name){
        load_items ();
        return items.Any(item => item.name == name);
    }

    public List<Item> get_items_in_page(int _page, int _itemsPerPage, List<Item> _itemSet) {
        load_items();
        List<Item> itemsInPage = _itemSet.Skip((_page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        return itemsInPage;
    }

    public int get_max_pages(int _itemsPerPage) {
        load_items ();
        int _ret = Mathf.CeilToInt((float)items.Count / _itemsPerPage);
        if (_ret < 1) _ret = 1;
        return _ret;
    }

    public void remove_item(int id) {
        Item? item = get_item_from_id(id);
        if (item.HasValue) {
            items.Remove(item.Value);
            save_items();
        }
    }

    public List<Item> get_items_with_tag(string _tag) {
        // This needs to change
        List<Item> _ret = new List<Item>();
        items.ForEach ((_item) => {
            Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_item.name);
            if (_data.tags.Contains (_tag)) {
                _ret.Add (_item);
            }
        });

        return _ret;
    }

    public void save_items() {
        ZPlayerPrefs.SetInt("ItemCount", items.Count);
        for (int i = 0; i < items.Count; i++) {
            ZPlayerPrefs.SetString($"Item_{i}_name", items[i].name);
            ZPlayerPrefs.SetInt($"Item_{i}_stack", items[i].stack);
            ZPlayerPrefs.SetInt($"Item_{i}_ID", items[i].ID);
            ZPlayerPrefs.SetString($"Item_{i}_equippedBy", items[i].equippedBy);
        }
        ZPlayerPrefs.Save();
    }

    public void load_items() {
        // Called from SaveHandler.Awake
        items.Clear();
        int itemCount = ZPlayerPrefs.GetInt("ItemCount", 0);
        for (int i = 0; i < itemCount; i++) {
            string name = ZPlayerPrefs.GetString($"Item_{i}_name");
            int stack = ZPlayerPrefs.GetInt($"Item_{i}_stack");
            int id = ZPlayerPrefs.GetInt($"Item_{i}_ID");
            string equippedBy = ZPlayerPrefs.GetString($"Item_{i}_equippedBy");

            items.Add(new Item(name, stack, id, equippedBy));
        }
    }

    /*
        FOR SHOPS
    */
    public List<Item> generate_item_set (List<string> _itemNames) {
        List<Item> _ret = new List<Item> ();
        _itemNames.ForEach ((_itemName) => {
            _ret.Add (new Item (_itemName, 1, Random.Range (1000, 10000000), ""));
        });

        return _ret;
    }

    /*
        FOR EQUIP
    */
    public List<Item> get_equipped_items (string _charName){
        load_items();
        return items.Where(item => item.equippedBy == _charName).ToList();
    }

    public void set_item_equip(Item _item, string _charName) {
        List<Item> equippedItems = get_equipped_items(_charName);

        // Find the item to change based on the equip slot (e.g., weapon, armor)
        bool hasOldEquip = false;
        Item _toChange = equippedItems.FirstOrDefault((item) => {
            Inv2_DB.ItemData _dataToSet = Inv2_DB.I.get_item_data(_item.name);
            Inv2_DB.ItemData _dataToChange = Inv2_DB.I.get_item_data(item.name);
            hasOldEquip = _dataToSet.equipTo == _dataToChange.equipTo;
            return hasOldEquip;
        });

        if (hasOldEquip) {
            // Clear the equipped status of the current item
            _toChange.equippedBy = "";

            // Find the index of the current equipped item in the inventory and update it
            int toChangeIndex = Inv2.I.items.FindIndex(item => item.name == _toChange.name && item.equippedBy == _charName);
            if (toChangeIndex != -1) {
                Inv2.I.items[toChangeIndex] = _toChange;
            }
        }

        // Equip the new item
        _item.equippedBy = _charName;
        Debug.Log($"{_item.name} equipped by {_item.equippedBy}");

        // Find the index of the new item in the inventory and update it
        int itemIndex = Inv2.I.items.FindIndex(item => item.name == _item.name && item.equippedBy == "");
        if (itemIndex != -1) {
            Inv2.I.items[itemIndex] = _item;
        }

        // Save the updated inventory
        save_items();
    }

    /*
        EXTRAS
    */
    public void log_all_items (){
        Debug.Log ("///////////////// LOGGING ALL ITEMS ////////////////////////");

        items.ForEach ((item) => {
            Inv2_DB.ItemData _itemData = Inv2_DB.I.get_item_data (item.name);
            Debug.Log ($"{item.name}, {_itemData.equipTo}, {item.equippedBy}");
        });

        Debug.Log ("///////////////// ALL ITEMS LOGGED ////////////////////////");
    }
}
