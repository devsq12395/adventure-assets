using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI_ChkItm : MonoBehaviour
{
    
    public static GameUI_ChkItm I;
	public void Awake(){ I = this; }

    public GameObject go;
    
    public TextMeshProUGUI name, desc;
    public Image img;

    public List<GameObject> buttonsGo;
    public List<TextMeshProUGUI> btnTxt;

    public DB_Items.Item item;

    void Start() {
        go.SetActive (true);

        

        go.SetActive (false);
    }

    void Update() {
        
    }
    
    public void show (DB_Items.Item _item){
        go.SetActive (true);
        item = _item;
        setup_window (item);
    }

    public void hide (){
        go.SetActive (false);
    }

    private void setup_window (DB_Items.Item _item){

        name.text = _item.name;
        desc.text = _item.desc;
        img.sprite = _item.sprite;

        for (int o = 0; o < btnTxt.Count; o++) {
            bool _hasOpt = (o < _item.options.Count);
            buttonsGo [o].SetActive (_hasOpt);

            if (_hasOpt) {
                btnTxt[o].text = _item.options [o];
            }
        }
    }

    public void click_options (string _option){
        DB_Items.I.click_options (_option);
    }
}
