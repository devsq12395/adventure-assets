using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_Map : MonoBehaviour {
    public static MM_Map I;
	public void Awake(){ I = this; }

    public GameObject go;
    public Image imgWindow;

    public List<string> mapNames;
    public List<GameObject> mapObjs;
    public List<Node> nodes;

    public Dictionary<string, GameObject> maps;

    public GameObject map;
    public string mapID;

    public void setup (){
        maps = new Dictionary<string, GameObject>();
        for (int i = 0; i < mapNames.Count; i++) {
            maps.Add(mapNames[i], mapObjs[i]);
        }
    }

    public void generate_map (string _mapID){ 
        GameObject _map = Instantiate (maps [_mapID], go.transform);
        map = _map;

        get_nodes ();
        Car.I.setup ();
    }

    private void get_nodes (){
        nodes = new List<Node>();
        nodes.AddRange(FindObjectsOfType<Node>());
    }

    public void show (string _mapID){
        go.SetActive (true);

        generate_map (_mapID);
    }

    public void hide (){
        Destroy (map);
    }

    public void select_node (string _type, string _val){
        switch (_type) {
            //////////// GENERICS
            case "mission": MM_Mission.I.show (PlayerPrefs.GetString ($"missionCurPool.{_val}")); break;
            case "dialog": MMCont_Dialog.I.create_dialog (_val); break;
            case "map": 
                // Unused
                Destroy (map);
                generate_map (_val);
                break;
            case "to-menu":
                hide ();
                break;

            //////////// CHANGE MAP
            case "change-map-wooster-square-2":
                if (PlayerPrefs.GetInt("areasState.wooster-square-2") == 1) {
                    ZPlayerPrefs.SetString("main-menu-map", "wooster-square-2");

                    // Set start node
                    switch (_val) {
                        case "entry-1": PlayerPrefs.SetString("start-node", "1"); break;
                    }

                    MainMenu.I.move_curtain ("changeMap");
                }
                break;

            //////////// DIALOG
            case "dialog-vic":
                int _statusVicDialog = PlayerPrefs.GetInt ("activity.dialog-with-vic");

                switch (_statusVicDialog) {
                    case 0: MMCont_Dialog.I.create_dialog ("dialog-vic-1"); break;
                    case 1: MMCont_Dialog.I.create_dialog ("dialog-vic-4"); break;

                    case 2: MMCont_Dialog.I.create_dialog ("dialog-vic-5"); break;
                    case 3: MMCont_Dialog.I.create_dialog ("dialog-vic-8"); break; 

                    case 4: MMCont_Dialog.I.create_dialog ("dialog-vic-10"); break;
                    case 5: MMCont_Dialog.I.create_dialog ("dialog-vic-17"); break;

                    case 6: MMCont_Dialog.I.create_dialog ("dialog-vic-19"); break;
                }
                break;

            case "dialog-anthony":
                int _statusAnthonyDialog = PlayerPrefs.GetInt ("activity.dialog-with-anthony");

                switch (_statusAnthonyDialog) {
                    case 0: MMCont_Dialog.I.create_dialog ("anthony-1"); break;
                    case 1: MMCont_Dialog.I.create_dialog ("anthony-4"); break;

                    case 2: MMCont_Dialog.I.create_dialog ("anthony-5"); break;
                }
                break;
        }
    }
}