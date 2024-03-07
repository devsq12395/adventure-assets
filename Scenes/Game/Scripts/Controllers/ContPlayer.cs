using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContPlayer : MonoBehaviour {

    public static ContPlayer I;
	public void Awake(){ I = this; }

    public int NUMBER_OF_ITEM_SLOTS;

    public InGameObject player;
    public List<InGameObject> players;

    public List<DB_Items.Item> items;

    public int gold, pla_curSel;

    void Start (){
        items = new List<DB_Items.Item> ();
        players = new List<InGameObject> ();
    }
    
    public void update (){
        
    }

    public void setup_player (){
        NUMBER_OF_ITEM_SLOTS = 5;
        
        create_players ();

        setup_items ();
    }
    
    // Chars
    public void create_players () {
        string _cN = "";
        string[] _lineup = JsonSaving.I.load ("lineup").Split (',');
        for (int _ch = 0; _ch < _lineup.Length; _ch++){
            _cN = _lineup [_ch];
            if (_cN == "") continue;
            
            GameObject _go = ContObj.I.create_obj (_cN, ContMap.I.pointList ["playerLounge"], 1);
        
            InGameObject _p = _go.GetComponent <InGameObject> ();
            _p.checkBorder = false;
            _p.isInvul = true;
            players.Add (_p);
            
            MUI_CharPane.I.create_char (_ch);
        }
        pla_curSel = PlayerPrefs.GetInt ("player_charSel");
        player = players [pla_curSel];
        player.gameObject.transform.position = ContMap.I.pointList ["playerSpawn"];
        player.checkBorder = true;
        player.isInvul = false;
        
        InGameCamera.I.target = player.transform;
    }
    
    public void change_char (int _cI){
        if (!DB_Conditions.I.can_change_char (_cI)) return;
        
        Vector2 _posPl = player.gameObject.transform.position,
                _pos = new Vector2 (_posPl.x, _posPl.y);
        player.checkBorder = false;
        player.gameObject.transform.position = ContMap.I.pointList ["playerLounge"];
        player.isInvul = true;
        
        pla_curSel = _cI;
        player = players [_cI];
        player.gameObject.transform.position = _pos;
        player.checkBorder = true;
        player.isInvul = false;
        
        InGameCamera.I.target = player.transform;
    }

    // Skills
    public void remove_skill (){
        
    }

    public void use_skill (string _input){
        InGameObject _player = ContPlayer.I.player;

        if (!DB_Conditions.I.atk_cond (_player)) return;

        SkillTrig _skill = ContObj.I.get_skill_with_skill_slot (_player, _input);
        if (_skill) {
            _skill.use_active ();
        } else {
            Debug.Log ("Skill not found...");
        }
    }

    // Items
    public void setup_items (){
        items = new List<DB_Items.Item> ();

        for (var i = 0; i < GameConstants.INVENTORY_SLOTS; i++) {
            items.Add (DB_Items.I.get_item (PlayerPrefs.GetString ("Item" + (i+1).ToString ())));
        }
    }
    
    public void add_item (string _name) {
        DB_Items.Item _new = DB_Items.I.get_item (_name);
        items.Add (_new);
        save_item (items.Count - 1, _name);
    }
    
    public void remove_item (string _id) {
        items.RemoveAt (get_item_index (_id));
        save_items ();
    }

    public void save_item (int _slot, string _name){
        PlayerPrefs.SetString (PlayerPrefs.GetString ("Item" + (_slot+1).ToString ()), _name);
    }

    public void save_items (){
        for (var i = 0; i < GameConstants.INVENTORY_SLOTS; i++) {
            save_item (i, items [i].name);
        }
    }

    public void use_item (int _slot){
        
    }
    
    public int get_item_index (string _id){
        for (int i = 0; i < items.Count; i++) {
            if (items [i].id == _id) {
                return i;
            }
        }
        
        return 0;
    }

    // Gold
    public void get_gold (int _inc){
        gold += _inc;
    }

    public void lose_gold (int _dec){
        gold -= _dec;
    }
}