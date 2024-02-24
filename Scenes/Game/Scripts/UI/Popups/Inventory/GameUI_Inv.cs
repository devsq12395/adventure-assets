using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI_Inv : MonoBehaviour {
    
    public static GameUI_Inv I;
	public void Awake(){ I = this; }

    public GameObject goUI, goCanvas;

    public GameObject go;
    public TextMeshProUGUI tPage;
    public int NUMBER_OF_BUTTONS;

    public string mode;
    
    public int pageCur, pageMax;
    public int NUM_OF_ITEMS_PER_PAGE;

    public List<string> items;

    void Start() {
        go.SetActive (true);

        

        go.SetActive (false);
    }

    void Update() {
        
    }
    
    public void show (string _mode){
        mode = _mode;
        go.SetActive (true);

        update_item_list ();
    }

    public void hide (){
        go.SetActive (false);
        mode = "hide";
    }

    public void update_item_list (){

    }

    public void create_item (string _txt){
        GameObject _newItemUI = Instantiate(goUI, goCanvas.transform);
        RectTransform _transform = _newItemUI.GetComponent<RectTransform>();

        int _COLUMN_COUNT = 2;
        float   _ITEM_WIDTH = 100f, _ITEM_HEIGHT = 100f, _XPOS_START = -110, _YPOS_START = -110,
                _HORIZ_SPACING = 10f, _VERT_SPACING = 10f;

        int _row = items.Count / _COLUMN_COUNT,
            _col = items.Count % _COLUMN_COUNT;

        _transform.anchoredPosition = new Vector2(
            _XPOS_START + _col * (_ITEM_WIDTH + _HORIZ_SPACING), 
            _YPOS_START + _row * (_ITEM_HEIGHT + _VERT_SPACING)
        );

        items.Add (_txt);
    }
    
    // UI Manipulation
    public void switch_page (int _inc){
        pageCur += _inc;
        if (pageCur > pageMax)  pageCur = 0;
        if (pageCur < 0)        pageCur = pageMax;
    }
}
