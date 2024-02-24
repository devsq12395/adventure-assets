using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContDamage : MonoBehaviour {

    public static ContDamage I;
	public void Awake(){ I = this; }

    public void damage (InGameObject _atk, InGameObject _def, int _damOrig, List<string> _extraTags_atk) {
        if (!DB_Conditions.I.dam_condition (_atk, _def)) return;

        int _dam = _damOrig;
        
        _dam = check_tag_effects (_atk, _def, _damOrig, _extraTags_atk);

        _def.hp -= _dam;

        post_dam_events (_atk, _def, _dam);

        if (_def.hp <= 0) {
            kill (_def);
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

    public void kill (InGameObject _def){
        ContBuffs.I.remove_all_buffs (_def);
        ContObj.I.evt_on_death (_def);
        Destroy (_def.gameObject);
    }

    private void post_dam_events (InGameObject _atk, InGameObject _def, int _dam) {
        // Invul if player
        if (_def == ContPlayer.I.player) {
            ContBuffs.I.add_buff (_def, "invulnerable");
        }
        
        // Codes that require an attacker goes here
        if (_atk != null) {
            
        }
        
        // Dam Text UI
        GameUI_InGameTxt.I.create_ingame_txt (_dam.ToString (), _def.gameObject.transform.position, 2f);
    }
    
    private int check_tag_effects (InGameObject _atk, InGameObject _def, int _damOrig, List<string> _tags){
        int _dam = _damOrig;

        List<string> _atkTags = ((_atk) ? _atk.tags : new List<string>());
        
        // Overload
        if (DB_Conditions.I.is_overload_fire_to_electric (_atkTags, _def.tags, _def.buffs) ||
            DB_Conditions.I.is_overload_electric_to_fire (_atkTags, _def.tags, _def.buffs)) {
                _dam += (int)((float)_dam * 0.2f);
                GameUI_InGameTxt.I.create_ingame_txt (DB_Strings.I.get_str ("Overload!"), _def.gameObject.transform.position, 2f);
        }
        
        return _dam;
    }
}
