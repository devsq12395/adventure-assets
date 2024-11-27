using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
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

[System.Serializable]
public class Bio
{
    public string info;
    public string skill1;
    public string skill2;
}

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public string characterId;
    public string imgPort;
    public string equipWeapon;
    public Stats stats;
    public Bio bio;
    public List<string> requires;
    public string goldCost;
}

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "ScriptableObjects/CharacterDatabase", order = 2)]
public class CharacterDatabase : ScriptableObject
{
    public List<Character> characters;
}
