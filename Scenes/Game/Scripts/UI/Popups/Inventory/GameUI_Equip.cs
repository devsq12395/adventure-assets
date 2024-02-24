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
        int _ind = ContPlayer.I.get_item_index (_name);
        string _oldItem = ContPlayer.I.items [_ind].name;
        
        ContPlayer.I.remove_item (_name);
        ContPlayer.I.add_item (_oldItem);
        
        GameUI_Inv.I.hide ();
        refresh_ui_list ();
    }
    
    // UI Manipulation
    public void check_change_equip (string _equipType){
        GameUI_Inv.I.show ("equip");
    }
    
    public void refresh_ui_list (){
        
    }
}
