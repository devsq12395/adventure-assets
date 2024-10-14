using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        equipStrList = new List<string>(){"weapon", "armor", "boots", "pendant", "armlet"};
    }

    public void add_item(string _itemName, int _stack = 1) {
        Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_itemName);

        if (get_has_item (_itemName) && _data.stackable) {
            Item _item = get_item_from_name (_itemName);
            _item.stack += _stack;
        } else {
            Item newItem = new Item(_itemName, _stack, Random.Range(1000, 10000000), "");
            items.Add(newItem);
        }

        save_items();
    }

    public Item get_item_from_id(int id) {
        load_items();
        for (int i = 0; i < items.Count; i++) {
            if (items[i].ID == id) {
                return items[i];
            }
        }
        return default(Item); // or handle the case where the item isn't found
    }

    public Item get_item_from_name(string name) {
        load_items();
        for (int i = 0; i < items.Count; i++) {
            if (items[i].name == name) {
                return items[i];
            }
        }
        return default(Item); // or handle the case where the item isn't found
    }

    public bool get_has_item(string name) {
        load_items();
        for (int i = 0; i < items.Count; i++) {
            if (items[i].name == name) {
                return true;
            }
        }
        return false;
    }

    public List<Item> get_items_in_page(int _page, int _itemsPerPage, List<Item> _itemSet) {
        load_items();
        List<Item> itemsInPage = new List<Item>();
        int startIndex = (_page - 1) * _itemsPerPage;
        int endIndex = startIndex + _itemsPerPage;

        for (int i = startIndex; i < endIndex && i < _itemSet.Count; i++) {
            itemsInPage.Add(_itemSet[i]);
        }

        return itemsInPage;
    }

    public int get_max_pages(int _itemsPerPage) {
        load_items();
        int totalItems = items.Count;
        int _ret = Mathf.CeilToInt((float)totalItems / _itemsPerPage);
        if (_ret < 1) _ret = 1;
        return _ret;
    }

    public void remove_item(int id) {
        load_items();
        for (int i = 0; i < items.Count; i++) {
            if (items[i].ID == id) {
                items.RemoveAt(i);
                save_items();
                return;
            }
        }
    }

    public List<Item> get_items_with_tag(string _tag) {
        List<Item> _ret = new List<Item>();
        for (int i = 0; i < items.Count; i++) {
            Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data(items[i].name);
            for (int j = 0; j < _data.tags.Count; j++) {
                if (_data.tags[j] == _tag) {
                    _ret.Add(items[i]);
                    break;
                }
            }
        }
        return _ret;
    }

    public string get_item_name_in_equipped_slot (string _slot, string _charName){
        List<Inv2.Item> itemsEquipped = Inv2.I.get_equipped_items(_charName);
        string _ret = "";
        for (int i = 0; i < itemsEquipped.Count; i++) {
            Inv2_DB.ItemData _itemData = Inv2_DB.I.get_item_data (itemsEquipped[i].name);
            if (_itemData.equipTo == _slot) {
                _ret = itemsEquipped[i].name;
                break;
            }
        }
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

    public List<Item> generate_item_set(List<string> _itemNames) {
        List<Item> _ret = new List<Item>();
        for (int i = 0; i < _itemNames.Count; i++) {
            _ret.Add(new Item(_itemNames[i], 1, Random.Range(1000, 10000000), ""));
        }
        return _ret;
    }

    public List<Item> get_equipped_items(string _charName) {
        load_items();
        List<Item> equippedItems = new List<Item>();
        for (int i = 0; i < items.Count; i++) {
            if (items[i].equippedBy == _charName) {
                equippedItems.Add(items[i]);
            }
        }
        return equippedItems;
    }

    public void set_item_equip(Item _item, string _charName) {
        List<Item> equippedItems = get_equipped_items(_charName);

        bool hasOldEquip = false;
        Item _toChange = default(Item);
        // Find if an item is already equipped in the equip slot of _item
        for (int i = 0; i < equippedItems.Count; i++) {
            Inv2_DB.ItemData _dataToSet = Inv2_DB.I.get_item_data(_item.name);
            Inv2_DB.ItemData _dataToChange = Inv2_DB.I.get_item_data(equippedItems[i].name);
            if (_dataToSet.equipTo == _dataToChange.equipTo) {
                _toChange = equippedItems[i];
                hasOldEquip = true;
                break;
            }
        }

        // If there is, unequip
        if (hasOldEquip) {
            _toChange.equippedBy = "";
            for (int i = 0; i < items.Count; i++) {
                if (items[i].ID == _toChange.ID) {
                    items[i] = _toChange;
                    break;
                }
            }
        }

        _item.equippedBy = _charName;

        for (int i = 0; i < items.Count; i++) {
            if (items[i].ID == _item.ID) {
                items[i] = _item;
                break;
            }
        }

        save_items();
    }

    public void log_all_items() {
        Debug.Log("///////////////// LOGGING ALL ITEMS ////////////////////////");
        for (int i = 0; i < items.Count; i++) {
            Inv2_DB.ItemData _itemData = Inv2_DB.I.get_item_data(items[i].name);
            Debug.Log($"{items[i].name}, {_itemData.equipTo}, {items[i].equippedBy}");
        }
        Debug.Log("///////////////// ALL ITEMS LOGGED ////////////////////////");
    }
}
