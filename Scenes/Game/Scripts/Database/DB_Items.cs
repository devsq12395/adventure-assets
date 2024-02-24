using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Items : MonoBehaviour {

    public static DB_Items I;
	public void Awake(){ I = this; }

    public Sprite dummy, test1, testSword;

    public struct Item {
        public string name, nameUI, equipType;
        public string desc, id;
        public Sprite sprite;
        public int stack, stackLim;

        public List<string> options;

        public Item (string _name, Sprite _sprite){
            name = _name;
            nameUI = "";
            desc = "";
            equipType = "";
            sprite = _sprite;
            id = Calculator.I.generate_id ();

            stack = 1;
            stackLim = 1;

            options = new List<string> ();
        }
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public Item get_item (string _name){
        Item _new = new Item ("", dummy);

        switch (_name) {
            case "sample1":
                _new.nameUI = "Sample 1";
                _new.desc = "";
                _new.equipType = "";
                _new.sprite = test1;
                break;
                
            case "test sword":
                _new.nameUI = "Test Sword";
                _new.desc = "";
                _new.equipType = "weap";
                _new.sprite = testSword;

                _new.options.AddRange (new string[] { "equip" });
                
                _new.stackLim = 10;
                break;

            case "testUsable":
                _new.nameUI = "Test Usable";
                _new.desc = "";
                _new.equipType = "";
                _new.sprite = test1;
                
                _new.stackLim = 10;
                break;

            default:
                _new.nameUI = "Empty";
                _new.desc = "";
                _new.sprite = dummy;
                break;
        }

        return _new;
    }

    public void click_options (string _opt){
        switch (_opt) {
            case "use": click_options_use (); break;
            case "equip": break;
        }
    }

    private void click_options_use (){
        DB_Items.Item itemSel = GameUI_ChkItm.I.item;

        switch (itemSel.name) {
            case "testUsable":
                Debug.Log ("Use success");
                
                ContPlayer.I.remove_item (itemSel.id);
                GameUI_ChkItm.I.hide ();
                break;
        }
    }
}
