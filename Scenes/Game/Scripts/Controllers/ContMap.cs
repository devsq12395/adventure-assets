using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContMap : MonoBehaviour
{
    public static ContMap I;
    public void Awake() { I = this; }

    public GameObject map;
    public DB_Maps.mapDetails details;
    public Dictionary<string, Vector2> pointList;

    public delegate void create_map();
    public create_map create_map_objs { get; set; }

    public void setup_map()
    {
        string _mission = ZPlayerPrefs.GetString("missionCur");
        DB_Missions.MissionData _data = DB_Missions.I.get_mission_data (_mission);
        List<string> _maps = _data.maps;
        string _curMap = _maps[Random.Range (0, _maps.Count)];

        details = DB_Maps.I.get_map_details(_curMap);

        map = details.mapObj;
        pointList = details.pointList;

        ContEnemies.I.setup(_data.enemies);

        create_map_objs();

        // Create a border around the map
        CreateMapBorder(details.size.x, details.size.y);
    }

    private void CreateMapBorder(float width, float height)
    {
        GameObject borderObject = new GameObject("MapBorder");
        LineRenderer lineRenderer = borderObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 5;
        lineRenderer.loop = true;

        // Define the corners of the border
        Vector3[] corners = new Vector3[5];
        corners[0] = new Vector3(-width, -height, 0);
        corners[1] = new Vector3(width, -height, 0);
        corners[2] = new Vector3(width, height, 0);
        corners[3] = new Vector3(-width, height, 0);
        corners[4] = new Vector3(-width, -height, 0);

        lineRenderer.SetPositions(corners);

        // Configure the LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        // Optionally, set the sorting order so it appears above the map
        lineRenderer.sortingOrder = 2;
    }

    public void change_map(string _map)
    {
        PlayerPrefs.SetString("map", _map);
        Transition_Game.I.change_state("toNextMap");
    }
}
