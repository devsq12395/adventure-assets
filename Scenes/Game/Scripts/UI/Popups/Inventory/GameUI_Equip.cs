using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI_Equip: MonoBehaviour
{
    
    public static GameUI_Equip I;
	public void Awake(){ I = this; }

    public GameObject go;
    public TextMeshProUGUI tPage;
    
    public int pageCur, pageMax;
    
    public bool isShow = false;

    void Start() {
        go.SetActive (true);

        

        go.SetActive (false);
    }

    void Update() {
        
    }
    
    public void show (){
        go.SetActive (true);
        isShow = true;
    }

    public void hide (){
        go.SetActive (false);
        isShow = true;
    }
    
    // Equipment Manipulation
    public void change_equip (string _name, string _equipType) {
        // Unused
    }
    
    // UI Manipulation
    public void check_change_equip (string _equipType){
        GameUI_Inv.I.show ("equip");
    }
    
    public void refresh_ui_list (){
        
    }
}
