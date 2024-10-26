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
        DB_Maps.I.SIZE_PER_PIECE = 50;

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

    public void create_map_objects(string biome) {
        // Fetch map object names for the specified biome
        List<string> _mapGameObjectNames = DB_Maps.I.get_map_lists(biome);
        List<List<bool>> _mapMatrix = new List<List<bool>>();

        float SIZE_PER_PIECE = DB_Maps.I.SIZE_PER_PIECE;
        
        // Calculate the offset so the center of the map is at (0, 0)
        float offsetX = (details.mapObjMatrix_sizeX - 1) / 2f * SIZE_PER_PIECE;
        float offsetY = (details.mapObjMatrix_sizeY - 1) / 2f * SIZE_PER_PIECE;
        
        // Initialize the map matrix
        for (int y = 0; y < details.mapObjMatrix_sizeY; y++) {
            List<bool> row = new List<bool>();
            for (int x = 0; x < details.mapObjMatrix_sizeX; x++) {
                row.Add(false); // Initialize all positions as empty
            }
            _mapMatrix.Add(row);
        }

        Vector2 _curPos = Vector2.zero;

        while (_curPos.y < details.mapObjMatrix_sizeY) {
            if (!_mapMatrix[(int)_curPos.y][(int)_curPos.x]) {
                // Randomly select and place a new map object
                string _newGameObj = _mapGameObjectNames[Random.Range(0, _mapGameObjectNames.Count)];
                DB_Maps.mapObject _mapObj = DB_Maps.I.get_map_game_object(_newGameObj);

                _mapObj.go.transform.position = new Vector2(
                    _curPos.x * SIZE_PER_PIECE - offsetX,
                    _curPos.y * SIZE_PER_PIECE - offsetY
                );
                
                // Mark the position as occupied in the matrix
                _mapMatrix[(int)_curPos.y][(int)_curPos.x] = true;

                // Place down-connected object if defined and within bounds
                if (!string.IsNullOrEmpty(_mapObj.nextMap_down) && _curPos.y + 1 < details.mapObjMatrix_sizeY) {
                    string _newGameObjDown = _mapObj.nextMap_down;
                    DB_Maps.mapObject _mapObjDown = DB_Maps.I.get_map_game_object(_newGameObjDown);

                    _mapObjDown.go.transform.position = new Vector2(
                        _curPos.x * SIZE_PER_PIECE - offsetX,
                        (_curPos.y + 1) * SIZE_PER_PIECE - offsetY
                    );

                    _mapMatrix[(int)_curPos.y + 1][(int)_curPos.x] = true;
                }

                // Place right-connected object if defined and within bounds
                if (!string.IsNullOrEmpty(_mapObj.nextMap_right) && _curPos.x + 1 < details.mapObjMatrix_sizeX) {
                    string _newGameObjRight = _mapObj.nextMap_right;
                    DB_Maps.mapObject _mapObjRight = DB_Maps.I.get_map_game_object(_newGameObjRight);

                    _mapObjRight.go.transform.position = new Vector2(
                        (_curPos.x + 1) * SIZE_PER_PIECE - offsetX,
                        _curPos.y * SIZE_PER_PIECE - offsetY
                    );

                    _mapMatrix[(int)_curPos.y][(int)_curPos.x + 1] = true;
                }
            }

            // Move to the next position in the matrix
            _curPos.x++;
            if (_curPos.x >= details.mapObjMatrix_sizeX) {
                _curPos.x = 0;
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
