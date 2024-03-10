using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MM_ItemCheck : MonoBehaviour {
    public static MM_ItemCheck I;
    public void Awake () {I = this;}

    public GameObject go, goBtnsInter, goBtnsNonInter;
    public TextMeshProUGUI tName, tDesc, tActionBtn;

    private List<GameObject> goBtns;

    private string isEquip, mode;

    MM_Inventory.Item item;

    public void setup (){
        go.SetActive (true);

        goBtns = new List<GameObject> ();
        goBtns.Add (goBtnsInter);
        goBtns.Add (goBtnsNonInter);

        go.SetActive (false);
    }

    public void show (MM_Inventory.Item _item, string _mode) {
        go.SetActive (true);

        item = _item;
        mode = _mode;
        bool isEquip = JsonReading.I.read ("items", $"items.{_item.name}.equip") != "";

        tName.text = JsonReading.I.read ("items", $"items.{_item.name}.name-ui");
        tDesc.text = JsonReading.I.read ("items", $"items.{_item.name}.desc");

        setup_action_btns ();
    }

    public void hide (){
        go.SetActive (false);
    }

    private void setup_action_btns (){
        foreach (GameObject _go in goBtns) _go.SetActive (false);

        switch (mode) {
            case "inventory":
                goBtnsNonInter.SetActive (true);
                break;
            default:
                goBtnsInter.SetActive (true);
                tActionBtn.text = JsonReading.I.get_str ($"UI-main-menu.{mode}");
                break;
        }
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
