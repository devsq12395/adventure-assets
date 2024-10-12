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
            case "mission": MM_Mission.I.show (_val); break;
            case "dialog": MMCont_Dialog.I.create_dialog (_val); break;
            case "shop": MMCont_Dialog.I.input (null, _val); break;
            case "map": 
                // Unused
                Destroy (map);
                generate_map (_val);
                break;
            case "to-menu":
                hide ();
                break;

            //////////// CHANGE MAP
            case "to-wooster-square-2":
                if (PlayerPrefs.GetInt("areasState.to-wooster-square-2") == 1) {
                    ZPlayerPrefs.SetString("main-menu-map", "wooster-square-2");

                    // Set start node
                    switch (_val) {
                        case "entry-1": PlayerPrefs.SetString("start-node", "1"); break;
                        case "entry-2": PlayerPrefs.SetString("start-node", "2"); break;
                    }

                    MainMenu.I.move_curtain ("changeMap");
                } else {
                    MMCont_Dialog.I.create_dialog ("dialog-area-locked");
                }
                break;
            case "to-wooster-square-3":
                if (PlayerPrefs.GetInt("areasState.to-wooster-square-3") == 1) {
                    ZPlayerPrefs.SetString("main-menu-map", "wooster-square-3");

                    // Set start node
                    switch (_val) {
                        case "entry-1": PlayerPrefs.SetString("start-node", "1"); break;
                    }

                    MainMenu.I.move_curtain ("changeMap");
                } else {
                    MMCont_Dialog.I.create_dialog ("dialog-area-locked");
                }
                break;
            case "to-wooster-square-4":
                if (PlayerPrefs.GetInt("areasState.to-wooster-square-4") == 1) {
                    // Commented out for 0.3 demo
                    // ZPlayerPrefs.SetString("main-menu-map", "wooster-square-4");

                    // // Set start node
                    // switch (_val) {
                    //     case "entry-1": PlayerPrefs.SetString("start-node", "1"); break;
                    // }

                    // MainMenu.I.move_curtain ("changeMap");
                } else {
                    MMCont_Dialog.I.create_dialog ("dialog-area-locked");
                }
                break;

            //////////// DIALOG
            case "dialog-vic":
                int _statusVicDialog = PlayerPrefs.GetInt ("activity.dialog-with-vic");

                switch (_statusVicDialog) {
                    case 0: MMCont_Dialog.I.create_dialog ("dialog-vic-1"); break;
                    case 1: MMCont_Dialog.I.create_dialog ("dialog-vic-4"); break;

                    case 2: MMCont_Dialog.I.create_dialog ("dialog-vic-5"); break;
                }
                break;

            case "dialog-vincenzo":
                int _statusVincenzoDialog = PlayerPrefs.GetInt ("activity.dialog-with-vincenzo");
                Debug.Log (_statusVincenzoDialog);

                if (_statusVincenzoDialog < 5) {
                    MMCont_Dialog.I.create_dialog ("dialog-vincenzo-1");
                } else {
                    switch (_statusVincenzoDialog) {
                        case 5: MMCont_Dialog.I.create_dialog ("dialog-vincenzo-5"); break;
                        case 6: MMCont_Dialog.I.create_dialog ("dialog-vincenzo-6"); break;
                    }
                }
                break;

            case "dialog-beatrice":
                int _statusBeatriceDialog = PlayerPrefs.GetInt ("activity.dialog-with-beatrice");

                if (_statusBeatriceDialog < 5) {
                    MMCont_Dialog.I.create_dialog ("dialog-beatrice-1");
                } else {
                    switch (_statusBeatriceDialog) {
                        case 5: MMCont_Dialog.I.create_dialog ("dialog-beatrice-5"); break;
                        case 8: MMCont_Dialog.I.create_dialog ("dialog-beatrice-8"); break;
                    }
                }
                break;

            case "field-1":
                int _house1_unlockedAnastasia = ZPlayerPrefs.GetInt ("charUnlocked.anastasia");
                string _statusField1 = ZPlayerPrefs.GetString("missionCurPool.field-1");

                if (_statusField1 == "field-1-1"){
                    Debug.Log ($"_house1_unlockedAnastasia = {_house1_unlockedAnastasia}");
                    MMCont_Dialog.I.create_dialog ((_house1_unlockedAnastasia == 0) ? "dialog-field-1" : "dialog-field-3");
                } else {
                    MMCont_Dialog.I.create_dialog ("dialog-field-6");
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