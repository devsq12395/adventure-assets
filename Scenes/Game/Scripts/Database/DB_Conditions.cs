using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DB_Conditions : MonoBehaviour {

    public static DB_Conditions I;
	public void Awake(){ I = this; }
    
    // Controls
    public bool move_cond (InGameObject _obj) {
        if (_obj.isAtk)                             return false;
        if (is_a_ui_showing())                      return false;

        if (ContBuffs.I.get_has_buff (_obj, "binding-chains"))              return false;

        return true;
    }

    public bool atk_cond (InGameObject _obj) {
        if (_obj.isAtk)                             return false;
        if (is_a_ui_showing())                      return false;

        if (ContBuffs.I.get_has_buff (_obj, "binding-chains"))              return false;

        return true;
    }

    public bool coll_cond (InGameObject _obj) {
        if (_obj.isInvul && _obj.type != "collect")                     return false;
        if (ContBuffs.I.get_has_buff (_obj, "invulnerable"))            return false;

        return true;
    }
    
    public bool coll_cond_missile (InGameObject _obj) {
        if (_obj.type == "missile" || _obj.type == "effect" || _obj.type == "collect")                                 
            return false;
        
        return true;
    }

    public bool coll_cond_collect (InGameObject _obj) {
        if (_obj.type == "missile" || _obj.type == "effect" || _obj.type == "collect")
            return false;
        
        return true;
    }
    
    public bool is_check_border (InGameObject _obj){
        if (_obj.type != "unit")            return false;
        if (!_obj.checkBorder)              return false;
        
        return true;
    }
    
    // UI
    public bool is_a_ui_showing (){
        if (GameUI_Inv.I.mode != "hide")            return true;
        if (GameUI_Upg.I.isShow)                    return true;
        if (GameUI_Equip.I.isShow)                  return true;
        
        return false;
    }
    
    // Battle
    public bool dam_condition (InGameObject _atk, InGameObject _def) {
        // Does not require an attacker conditions here
        if (_def.type != "unit")            return false;
        if (ContBuffs.I.get_has_buff (_def, "invulnerable"))                 return false;

        // Requires an attacker conditions here
        if (_atk != null) {
            if (_atk.owner == _def.owner)           return false;
        }

        return true;
    }

    public bool debuff_condition (InGameObject _atk, InGameObject _def) {
        if (_def.type != "unit")            return false;

        if (_atk != null) {
            if (_atk.owner == _def.owner)           return false;
        }

        return true;
    }
    
    public bool can_change_char (int _cI){
        if (is_a_ui_showing())                      return false;
        
        if (ContPlayer.I.players [_cI] == null) return false;
        
        InGameObject _newChar = ContPlayer.I.players [_cI];
        if (_newChar.hp < 0) return false;
        
        return true;
    }
    
    public bool can_mp_regen (InGameObject _obj){
        if (_obj.mp >= _obj.mpMax) return false;
        
        return true;
    }

    public bool can_move (InGameObject _obj){
        if (_obj.type != "unit")                                                return true;
        if (ContBuffs.I.get_has_buff (_obj, "binding-chains"))                  return false;


        return true;
    }
    
    public bool is_kill_on_border_pass (InGameObject _obj){
        if (_obj.type == "unit") return false;
        
        return true;
    }
    
    // Elemental Effects
    public bool check_tags_fire (List<string> _tags){
        return _tags.Any(tag => tag == "burn" || tag == "fire");
    }
    
    public bool buff_names_fire (List<ContBuffs.buff> _buffs){
        return _buffs.Any(buff => buff.name == "burn" || buff.name == "burned");
    }
    
    public bool check_tags_electric (List<string> _tags){
        return _tags.Any(tag => tag == "electric");
    }
    
     public bool buff_names_electric (List<ContBuffs.buff> _buffs){
        return _buffs.Any(buff => buff.name == "charged" || buff.name == "binding-chains" || buff.name == "void-sphere-grounded");
    }
    
    public bool is_overload_fire_to_electric (List<string> _atkTags, List<string> _defTags, List<ContBuffs.buff> _buffs){
        // Attacker Tags
        if (!check_tags_fire (_atkTags)) return false;
        
        // Defender Tags
        if (check_tags_electric (_defTags)) return true;
        
        // Defender Buffs
        if (buff_names_electric (_buffs)) return true;
        
        return false;
    }
    
    public bool is_overload_electric_to_fire (List<string> _atkTags, List<string> _defTags, List<ContBuffs.buff> _buffs){
        // Attacker Tags
        if (!check_tags_electric (_atkTags)) return false;
        
        // Defender Tags
        if (check_tags_fire (_defTags)) return true;
        
        // Defender Buffs
        if (buff_names_fire (_buffs)) return true;
        
        return false;
    }

}