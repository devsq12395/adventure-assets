using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/Mission", order = 1)]
public class Mission : ScriptableObject
{
    public string missionId;
    public string name;
    public string desc;
    public string sprite;
    public string faction;
    public string difficulty;
    public string rewards;
    public string enemies;
    public List<string> maps;
    public List<string> unlocksArea;
    public List<string> missionsSet;
    public List<string> activitySet;
}

[CreateAssetMenu(fileName = "MissionsData", menuName = "ScriptableObjects/Missions", order = 2)]
public class Missions : ScriptableObject
{
    public List<Mission> missionList;
}