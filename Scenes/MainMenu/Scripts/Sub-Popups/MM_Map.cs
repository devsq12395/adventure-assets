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
        foreach (Transform child in _map.transform) {
            MapNode _comp = child.gameObject.GetComponent<MapNode>();

            if (_comp && _comp.type != "to-menu" ) {
                bool _isLocked = JsonSaving.I.load ($"areasUnlocked.{_comp.ID}") == "0";

                if (_isLocked) {
                    Button _btn = child.gameObject.GetComponent<Button>();
                    Image _img = child.gameObject.GetComponent<Image>();

                    _btn.interactable = false;
                }
            }
        }

        map = _map;
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
            case "mission": MM_Mission.I.show (JsonSaving.I.load ($"missionCurPool.{_val}")); break;
            case "dialog": MMCont_Dialog.I.create_dialog (_val); break;
            case "map": 
                Destroy (map);
                generate_map (_val);
                break;
            case "to-menu":
                hide ();
                break;

            case "dialog-vic":
                string _statusVicDialog = JsonSaving.I.load ("activity.dialog-with-vic");

                switch (_statusVicDialog) {
                    case "0": MMCont_Dialog.I.create_dialog ("dialog-vic-1"); break;
                    case "1": MMCont_Dialog.I.create_dialog ("dialog-vic-4"); break;

                    case "2": MMCont_Dialog.I.create_dialog ("dialog-vic-5"); break;
                    case "3": MMCont_Dialog.I.create_dialog ("dialog-vic-8"); break; 

                    case "4": MMCont_Dialog.I.create_dialog ("dialog-vic-10"); break;
                    case "5": MMCont_Dialog.I.create_dialog ("dialog-vic-17"); break;

                    case "6": MMCont_Dialog.I.create_dialog ("dialog-vic-19"); break;
                }
                break;

            case "dialog-anthony":
                string _statusAnthonyDialog = JsonSaving.I.load ("activity.dialog-with-anthony");

                switch (_statusAnthonyDialog) {
                    case "0": MMCont_Dialog.I.create_dialog ("anthony-1"); break;
                    case "1": MMCont_Dialog.I.create_dialog ("anthony-4"); break;

                    case "2": MMCont_Dialog.I.create_dialog ("anthony-5"); break;
                }
                break;
        }
    }
}