using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ContDamage : MonoBehaviour {

    public static ContDamage I;
	public void Awake(){ I = this; }

    public void damage (InGameObject _atk, InGameObject _def, int _damOrig, List<string> _extraTags_atk) {
        if (!DB_Conditions.I.dam_condition (_atk, _def)) return;

        int _dam = _damOrig;
        
        _dam = check_other_effects (_atk, _def, _dam, _extraTags_atk); 
        _dam = check_tag_effects (_atk, _def, _dam, _extraTags_atk);

        if (_dam < 0) _dam = 0;
        _def.hp -= _dam;

        post_dam_events (_atk, _def, _dam);

        if (_def.hp <= 0) {
            bool _isKill = before_kill_events (_atk, _def);
            if (_isKill) {
                on_kill_events (_atk, _def);
                kill (_def);
            }
        }
    }
    
    public void lose_hp (InGameObject _def, int _damOrig, List<string> _tags) {
        if (!DB_Conditions.I.dam_condition (null, _def)) return;
        
        int _dam = _damOrig;
        
        _dam = check_tag_effects (null, _def, _damOrig, _tags);

        _def.hp -= _dam;
        if (_def.hp < 1) _def.hp = 1;

        post_dam_events (null, _def, _dam);
    }

    public void lose_mp (InGameObject _def, int _mc) {
        _def.mp -= _mc;
        if (_def.mp <= 0) _def.mp = 0;
    }

    public void gain_hp (InGameObject _def, int _healOrig, List<string> _tags) {
        if (!DB_Conditions.I.dam_condition (null, _def)) return;
        
        int _heal = _healOrig;

        _def.hp += _heal;
        if (_def.hp > _def.hpMax) _def.hp = _def.hpMax;
    }

    public void gain_mp (InGameObject _def, int _mc) {
        _def.mp += _mc;
        if (_def.mp > _def.mpMax) _def.mp = _def.mpMax;
    }

    public void kill (InGameObject _def){
        ContBuffs.I.remove_all_buffs (_def);
        ContObj.I.evt_on_death (_def);

        if (_def.type == "unit" && !_def.tags.Contains ("dummy")) {
            SoundHandler.I.play_sfx ("explosion");
            ContEffect.I.create_effect ("explosion1_mini", _def.gameObject.transform.position);
        }

        Destroy (_def.gameObject);
    }

    /*
        EVENTS
    */
    private void post_dam_events (InGameObject _atk, InGameObject _def, int _dam) {
        // If player is hit
        if (_def == ContPlayer.I.player) {
            // Invul
            ContBuffs.I.add_buff (_def, "invulnerable");
            
            // Blood overlay
            MUI_Overlay.I.show_overlay ("blood");
        }
        
        // Codes that require an attacker goes here
        if (_atk != null) {
            
        }
        
        // Dam Text UI
        GameUI_InGameTxt.I.create_ingame_txt ($"{_dam.ToString ()}!", _def.gameObject.transform.position, 2f);

        // Sound Effects
        SoundHandler.I.play_sfx ("big-hit");
    }

    private int check_other_effects (InGameObject _atk, InGameObject _def, int _damOrig, List<string> _tags){
        int _dam = _damOrig;

        // Crit rate and dam
        if (_atk.tags.Contains ("hero")) { 
            int _critRate = Random.Range (0, 100);
            if (_critRate <= _atk.statCritRate) { 
                _dam += _dam * (_atk.statCritDam / 100) + 2;
                GameUI_InGameTxt.I.create_ingame_txt ("Critical!", _def.gameObject.transform.position, 2f);
            }
        }

        // Armor
        _dam -= _def.armor;

        return _dam;
    }
    
    private int check_tag_effects (InGameObject _atk, InGameObject _def, int _damOrig, List<string> _tags){
        int _dam = _damOrig;

        List<string> _atkTags = ((_atk) ? _atk.tags : new List<string>());
        
        /*// Overload
        if (DB_Conditions.I.is_overload_fire_to_electric (_atkTags, _def.tags, _def.buffs) ||
            DB_Conditions.I.is_overload_electric_to_fire (_atkTags, _def.tags, _def.buffs)) {
                _dam += (int)((float)_dam * 0.2f);
                GameUI_InGameTxt.I.create_ingame_txt (DB_Strings.I.get_str ("Overload!"), _def.gameObject.transform.position, 2f);
        }

        // Overload
        if (DB_Conditions.I.is_overload_fire_to_electric (_atkTags, _def.tags, _def.buffs) ||
            DB_Conditions.I.is_overload_electric_to_fire (_atkTags, _def.tags, _def.buffs)) {
                _dam += (int)((float)_dam * 0.2f);
                GameUI_InGameTxt.I.create_ingame_txt (DB_Strings.I.get_str ("Overload!"), _def.gameObject.transform.position, 2f);
        }*/
        
        return _dam;
    }

    private bool before_kill_events (InGameObject _atk, InGameObject _def){
        bool _isKill = true;
        
        // Check enemy count
        if (_def.owner == 2 && _def.type == "unit") {
            if (_def.tags.Contains ("boss")) {
                ContEnemies.I.start_next_wave ();
            } else {
                check_enemy_count ();
            }
        }

        // Codes that require an attacker goes here
        if (_atk != null) {
            
        }

        if (_def.tags.Contains ("hero") && _def.owner == 1) if (check_game_over ()) _isKill = false;

        return _isKill;
    }

    private void on_kill_events (InGameObject _atk, InGameObject _def){
        List<InGameObject> pList = ContPlayer.I.players;

        // Change char
        if (_def.tags.Contains ("hero")) {
            for (int i = 0; i < pList.Count; i++) {
                if (pList [i].hp > 0) {
                    ContPlayer.I.change_char (i);
                }
            }
        }
    }

    /*
        Event functions
    */
    public void check_enemy_count (){
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int enemyCount = 0;

        foreach (GameObject obj in allObjects) {
            InGameObject inGameObject = obj.GetComponent<InGameObject>();
            if (inGameObject != null && inGameObject.owner == 2 && inGameObject.type == "unit") {
                enemyCount++;
            }
        };
        
        if (enemyCount <= 0 && FightCountdown.I.countdownComplete) {
            ContEnemies.I.start_next_wave ();
        }
    }
    private bool check_game_over (){
        List<InGameObject> pList = ContPlayer.I.players;

        // Check game over
        bool _isGameOver = true;
        for (int i = 0; i < pList.Count; i++) {
            if (pList [i].hp > 0) {
                _isGameOver = false;
                break;
            }
        }
        if (_isGameOver) {
            GameUI_GameOver.I.show (
                "fail",
                "Your team is wiped out!"
            );
        }

        return _isGameOver;
    }
}
