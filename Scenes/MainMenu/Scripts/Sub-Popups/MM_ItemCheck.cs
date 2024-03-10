using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_ItemCheck : MonoBehaviour {
    public static MM_ItemCheck I;
    public void Awake () {I = this;}

    public GameObject go, goBtnsInter, goBtnsNonInter;
    public TextMeshProUGUI tName, tDesc;

    private string isEquip, mode;

    MM_Inventory.Item item;

    public void show (MM_Inventory.Item _item) {
        go.SetActive (true);

        item = _item;
        bool isEquip = MM_Inventory.I.mode == "equip" && JsonReading.I.read ("items", $"items.{_item.name}.equip") != "";

        tName.text = JsonReading.I.read ("items", $"items.{_item.name}.name-ui");
        tDesc.text = JsonReading.I.read ("items", $"items.{_item.name}.desc");

        goBtnsInter.SetActive (isEquip);
        goBtnsNonInter.SetActive (!isEquip);
    }

    public void hide (){
        go.SetActive (false);
    }

    public void action (){
        MiniDialog _dialog;

        switch (mode) {
            case "buy": 
                
                break;
            case "sell": 
                
                break;
        }
    }
}
