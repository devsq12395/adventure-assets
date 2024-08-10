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
    public void Awake() { I = this; }

    public struct Item {
        public string name; public int stack; public int ID;

        public Item(string _name, int _stack, int _id) {
            name = _name; stack = _stack; ID = _id;
        }
    }
    public struct ItemUI {
        public GameObject go; public TextMeshProUGUI name; public TextMeshProUGUI stack;

        public ItemUI(GameObject _go, TextMeshProUGUI _name, TextMeshProUGUI _stack) {
            go = _go; name = _name; stack = _stack;
        }
    }

    public void add_item(string _itemName, int _stack = 1) {
        Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_itemName);

        if (get_has_item (_itemName) && _data.stackable){
            Item _item = get_item_from_name (_itemName);
            _item.stack += _stack;
        } else {
            Item newItem = new Item (_itemName, _stack, Random.Range (1000, 10000000));
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

    public List<Item> get_items_in_page(int _page, int _itemsPerPage) {
        load_items();
        List<Item> itemsInPage = items.Skip((_page - 1) * _itemsPerPage).Take(_itemsPerPage).ToList();
        return itemsInPage;
    }

    public int get_max_pages(int _itemsPerPage) {
        load_items ();
        return Mathf.CeilToInt((float)items.Count / _itemsPerPage);
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
        }
        ZPlayerPrefs.Save();
    }

    public void load_items() {
        // Called from SaveHandler.Awake
        items.Clear();
        int itemCount = ZPlayerPrefs.GetInt("ItemCount", 0);
        for (int i = 0; i < itemCount; i++) {
            string name = ZPlayerPrefs.GetString($"Item_{i}_name"); Debug.Log (name);
            int stack = ZPlayerPrefs.GetInt($"Item_{i}_stack");
            int id = ZPlayerPrefs.GetInt($"Item_{i}_ID");
            items.Add(new Item(name, stack, id));
        }
    }

    /*
        FOR SHOPS
    */
    public List<Item> generate_item_set (List<string> _itemNames) {
        List<Item> _ret = new List<Item> ();
        _itemNames.ForEach ((_itemName) => {
            _ret.Add (new Item (_itemName, 1, Random.Range (1000, 10000000)));
        });

        return _ret;
    }
}