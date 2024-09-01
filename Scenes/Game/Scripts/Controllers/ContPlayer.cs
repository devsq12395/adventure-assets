using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContPlayer : MonoBehaviour {

    public static ContPlayer I;
	public void Awake(){ I = this; }

    public int NUMBER_OF_ITEM_SLOTS;

    public InGameObject player;
    public List<InGameObject> players;

    public List<DB_Items.Item> items;

    public int gold, pla_curSel;
    public float sta, staMax, staReturnCount, staReturnAmount;

    void Start (){
        items = new List<DB_Items.Item> ();
        players = new List<InGameObject> ();

        sta = 10;
        staMax = 10;
        staReturnCount = 0.75f;
        staReturnAmount = 0.05f;
    }
    
    public void update (){
        stamina_update ();
    }

    public void setup_player (){
        NUMBER_OF_ITEM_SLOTS = 5;
        
        create_players ();
    }
    
    // Chars
    public void create_players () {
        string _cN = "";
        List<string> _lineup = new List<string>();
        _lineup = Enumerable.Range(1, 4).Select(i => ZPlayerPrefs.GetString($"lineup-{i}")).ToList();
        for (int _ch = 0; _ch < _lineup.Count; _ch++){
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

        MUI_HPBars.I.set_char (player.name);
        ContEffect.I.create_effect ("move-smoke", _posPl);

        SoundHandler.I.play_sfx ("dash-smoke");
    }

    // Stamina
    public bool check_and_use_stamina (int _staReq){
        if (_staReq > sta) {
            return false;
        } else {
            sta -= _staReq;
            staReturnCount = 2;
            return true;
        }
    }

    public void stamina_update (){
        if (staReturnCount > 0) {
            staReturnCount -= Time.deltaTime;
        } else {
            if (sta < staMax) {
                sta += staReturnAmount;
                if (sta > staMax) sta = staMax;
            }
        }
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
}