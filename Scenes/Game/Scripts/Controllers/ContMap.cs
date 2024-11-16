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

    public List<GameObject> maps;
    private List<List<bool>> _mapMatrix; // Define _mapMatrix here

    public string setup_map() {
        DB_Maps.I.SIZE_PER_PIECE = 100;

        Grid[] allGrids = FindObjectsOfType<Grid>();
        foreach (Grid gridObject in allGrids) {
            if (gridObject != null) {
                Destroy(gridObject.gameObject);
            }
        }

        string _mission = ZPlayerPrefs.GetString("missionCur");
        DB_Missions.MissionData _data = DB_Missions.I.get_mission_data(_mission);
        List<string> _maps = _data.maps;

        int curMapLvl = PlayerPrefs.GetInt("cur-map-lvl");
        string _curMap = _maps[curMapLvl];

        details = DB_Maps.I.get_map_details(_curMap);
        pointList = details.pointList;
        ContEnemies.I.setup(_data.enemies[curMapLvl]);

        // Initialize the map matrix based on map size
        InitializeMapMatrix(details.mapObjMatrix_sizeX, details.mapObjMatrix_sizeY);

        // Create a border around the map
        CreateMapBorder(details.size.x, details.size.y);

        // Create the map game objects
        create_map_objects(details.biome);

        return _curMap;
    }

    private void InitializeMapMatrix(int width, int height) {
        _mapMatrix = new List<List<bool>>();
        for (int y = 0; y < height; y++) {
            List<bool> row = new List<bool>();
            for (int x = 0; x < width; x++) {
                row.Add(false); // Initialize all positions as empty
            }
            _mapMatrix.Add(row);
        }
    }

    public void create_map_objects(string biome) {
        List<string> _mapGameObjectNames = DB_Maps.I.get_map_lists(biome);
        float SIZE_PER_PIECE = DB_Maps.I.SIZE_PER_PIECE;

        // Calculate offsets to center the map at (0, 0)
        float offsetX = (details.mapObjMatrix_sizeX - 1) / 2f * SIZE_PER_PIECE;
        float offsetY = (details.mapObjMatrix_sizeY - 1) / 2f * SIZE_PER_PIECE;

        Vector2 _curPos = new Vector2(0, 0);  // Start at top-left

        while (_curPos.y < details.mapObjMatrix_sizeY) {
            // Check if the slot is already occupied
            if (!_mapMatrix[(int)_curPos.y][(int)_curPos.x]) {
                string _newGameObj = _mapGameObjectNames[Random.Range(0, _mapGameObjectNames.Count)];
                DB_Maps.mapObject _mapObj = DB_Maps.I.get_map_game_object(_newGameObj);

                // Place the object at the calculated position
                _mapObj.go.transform.position = new Vector2(
                    (_curPos.x * SIZE_PER_PIECE) - offsetX,
                    (_curPos.y * SIZE_PER_PIECE) - offsetY
                );

                // Mark the slot as occupied
                _mapMatrix[(int)_curPos.y][(int)_curPos.x] = true;

                // Place any connected objects, marking those slots as well
                PlaceConnectedObject(_mapObj, _curPos, offsetX, offsetY, SIZE_PER_PIECE);
            }

            // Move to the next position
            _curPos.x++;
            if (_curPos.x >= details.mapObjMatrix_sizeX) {
                _curPos.x = 0;
                _curPos.y++;
            }
        }
    }

    private void PlaceConnectedObject(DB_Maps.mapObject _mapObj, Vector2 _curPos, float offsetX, float offsetY, float SIZE_PER_PIECE) {
        if (!string.IsNullOrEmpty(_mapObj.nextMap_down) && _curPos.y + 1 < details.mapObjMatrix_sizeY) {
            DB_Maps.mapObject _mapObjDown = DB_Maps.I.get_map_game_object(_mapObj.nextMap_down);
            _mapObjDown.go.transform.position = new Vector2(
                _curPos.x * SIZE_PER_PIECE - offsetX,
                (_curPos.y + 1) * SIZE_PER_PIECE - offsetY
            );
            _mapMatrix[(int)_curPos.y + 1][(int)_curPos.x] = true;
        }

        if (!string.IsNullOrEmpty(_mapObj.nextMap_right) && _curPos.x + 1 < details.mapObjMatrix_sizeX) {
            DB_Maps.mapObject _mapObjRight = DB_Maps.I.get_map_game_object(_mapObj.nextMap_right);
            _mapObjRight.go.transform.position = new Vector2(
                (_curPos.x + 1) * SIZE_PER_PIECE - offsetX,
                _curPos.y * SIZE_PER_PIECE - offsetY
            );
            _mapMatrix[(int)_curPos.y][(int)_curPos.x + 1] = true;
        }
    }

    private void CreateMapBorder(float width, float height) {
        GameObject borderObject = new GameObject("MapBorder");
        LineRenderer lineRenderer = borderObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 5;
        lineRenderer.loop = true;

        Vector3[] corners = new Vector3[5];
        corners[0] = new Vector3(-width, -height, 0);
        corners[1] = new Vector3(width, -height, 0);
        corners[2] = new Vector3(width, height, 0);
        corners[3] = new Vector3(-width, height, 0);
        corners[4] = new Vector3(-width, -height, 0);

        lineRenderer.SetPositions(corners);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.sortingOrder = 1;
    }

    public void change_map(string _map) {
        PlayerPrefs.SetString("map", _map);
        Transition_Game.I.change_state("toNextMap");
    }
}
