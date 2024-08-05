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
    private List<Item> items = new List<Item>();

    public void Awake() { 
        I = this; 
        load_items();
    }

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

    public void add_item(Item item) {
        // This needs to be able to handle stacking
        items.Add(item);
        save_items();
    }

    public Item? get_item(int id) {
        return items.FirstOrDefault(item => item.ID == id);
    }

    public void remove_item(int id) {
        Item? item = get_item(id);
        if (item.HasValue) {
            items.Remove(item.Value);
            save_items();
        }
    }

    public List<Item> get_items_with_tag(string tag) {
        // This needs to change
        return items.Where(item => item.name.Contains(tag)).ToList();
    }

    private void save_items() {
        ZPlayerPrefs.SetInt("ItemCount", items.Count);
        for (int i = 0; i < items.Count; i++) {
            ZPlayerPrefs.SetString($"Item_{i}_name", items[i].name);
            ZPlayerPrefs.SetInt($"Item_{i}_stack", items[i].stack);
            ZPlayerPrefs.SetInt($"Item_{i}_ID", items[i].ID);
        }
        ZPlayerPrefs.Save();
    }

    private void load_items() {
        items.Clear();
        int itemCount = ZPlayerPrefs.GetInt("ItemCount", 0);
        for (int i = 0; i < itemCount; i++) {
            string name = ZPlayerPrefs.GetString($"Item_{i}_name");
            int stack = ZPlayerPrefs.GetInt($"Item_{i}_stack");
            int id = ZPlayerPrefs.GetInt($"Item_{i}_ID");
            items.Add(new Item(name, stack, id));
        }
    }
}
