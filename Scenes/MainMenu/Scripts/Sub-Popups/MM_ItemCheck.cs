using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MM_ItemCheck : MonoBehaviour {
    public static MM_ItemCheck I;
    public void Awake () {I = this;}

    public GameObject go, goBtnsInter, goBtnsNonInter;
    public Image imgWindow;
    public TextMeshProUGUI tName, tDesc, tActionBtn;

    private List<GameObject> goBtns;

    private string isEquip, mode;

    public Inv2.Item item;

    public void setup (){
        go.SetActive (true);

        goBtns = new List<GameObject> ();
        goBtns.Add (goBtnsInter);
        goBtns.Add (goBtnsNonInter);

        go.SetActive (false);
    }

    public void show (Inv2.Item _item, string _mode) {
        Inv2_DB.ItemData _data = Inv2_DB.I.get_item_data (_item.name);

        go.SetActive (true);
        imgWindow.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        imgWindow.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);   
        
        string _itmStr = $"items.{_item.name}";

        item = _item;
        mode = _mode;
        bool isEquip = JsonReading.I.read ("items", $"{_itmStr}.equip") != "";

        tName.text = JsonReading.I.read ("items", $"{_itmStr}.name-ui");
        tDesc.text = $"{JsonReading.I.read ("items", $"{_itmStr}.desc")}\n\nCost: {JsonReading.I.read ("items", $"{_itmStr}.cost")}";

        setup_action_btns ();
    }

    public void hide (){
        imgWindow.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.2f).SetEase(Ease.InBack).OnComplete(() => go.SetActive(false));
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
            case "buy": case "sell": 
                MM_BuyOrSell.I.show (mode, item);
                hide ();
                break;
            case "check-equipped":
                MM_Char.I.open_change_equip ();
                hide ();
                break;
            case "equip":
                MM_Char.I.change_equip (item.name);
                hide (); 
                MM_Inventory.I.remove_item (item.ID);
                MM_Inventory.I.hide ();
                break;
        }
    }
}
