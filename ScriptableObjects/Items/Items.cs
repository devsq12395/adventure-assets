using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bonuses
{
    public int hp;
    public int mp;
    public int attack;
    public int range;
    public int skill;
    public int speed;
    public int armor;
    public int critRate; // "crit-rate" converted to camelCase
    public int critDam;  // "crit-dam" converted to camelCase
}

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemId;
    public string nameUI; // "name-ui" converted to camelCase
    public string desc;
    public int cost;
    public bool stackable;
    public int stackMax;
    public List<string> tags;
    public string use;
    public string equip;
    public List<string> requires;
    public Bonuses bonuses;
}

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/ItemDatabase", order = 2)]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;
}

[System.Serializable]
public class ShopInventory
{
    public string shopId;
    public List<string> itemIds;
}

[CreateAssetMenu(fileName = "ShopInventoryDatabase", menuName = "ScriptableObjects/ShopInventoryDatabase", order = 3)]
public class ShopInventoryDatabase : ScriptableObject
{
    public List<ShopInventory> shopInventories;
}
