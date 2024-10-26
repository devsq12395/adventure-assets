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

    private List<GameObject> maps;

    public void setup_map() {
        Grid[] allGrids = FindObjectsOfType<Grid>();
        foreach (Grid gridObject in allGrids) {
            if (gridObject != null){
                Destroy(gridObject.gameObject);
            }
        }

        string _mission = ZPlayerPrefs.GetString("missionCur");
        DB_Missions.MissionData _data = DB_Missions.I.get_mission_data (_mission);
        List<string> _maps = _data.maps;

        int curMapLvl = PlayerPrefs.GetInt ("cur-map-lvl");
        string _curMap = _maps[curMapLvl];

        details = DB_Maps.I.get_map_details(_curMap);
        pointList = details.pointList;
        ContEnemies.I.setup(_data.enemies[curMapLvl]);

        // Create a border around the map
        CreateMapBorder(details.size.x, details.size.y);

        // Create the map game objects
        create_map_objects (details.biome);
    }

    public void create_map_objects (string biome){
        List<string> _mapGameObjectNames = DB_Maps.I.get_map_game_object (biome);
        List<List<bool>> _mapMatrix = new List<List<bool>>();

        float SIZE_PER_PIECE = DB_Maps.I.SIZE_PER_PIECE;

        // (0, 0) will be the uppermost part of the matrix
        // Remember that _mapMatrix works this way: _mapMatrix [Y][X]
        Vector2 _curPos = new Vector2 (0, 0);

        while (_curPos.y <= details.mapObjMatrix_sizeY){
            // Add a tile to current position if empty
            if (!_mapMatrix [_curPos.y][_curPos.x]) {
                string _newGameObj = _mapGameObjectNames [Random.Range (0, _mapGameObjectNames.Count)];

                DB_Maps.mapObject _mapObj = DB_Maps.I.get_map_game_object (_newGameObj);

                _mapObj.go.transform.position = new Vector2 (
                    ((SIZE_PER_PIECE * mapObjMatrix_sizeX) / 2) - (SIZE_PER_PIECE / 2),
                    ((SIZE_PER_PIECE * mapObjMatrix_sizeY) / 2) - (SIZE_PER_PIECE / 2)
                );
                
                
            }

            // Check next position
            _curPos.x++;
            if (_curPos.x > details.mapObjMatrix_sizeX) {
                _curPos.y++;
            }
        }
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
        lineRenderer.sortingOrder = 1;
    }

    public void change_map(string _map)
    {
        PlayerPrefs.SetString("map", _map);
        Transition_Game.I.change_state("toNextMap");
    }
}
