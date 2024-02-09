using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Buffs : MonoBehaviour {

    public static DB_Buffs I;
	public void Awake(){ I = this; }

    public ContBuffs.buff get_buff_data (string _name) {
        ContBuffs.buff _new = new ContBuffs.buff (_name);
        _new.dur = get_buff_data_dur (_name);

        string _atchName = (get_buff_has_attach (_name) ? "buffAtch_" + _name : "dummy");
        _new.attach = DB_Objects.I.get_game_obj (_atchName);
        _new.atchOffset = get_buff_attach_offset (_name);

        return _new;
    }

    public float get_buff_data_dur (string _name){
        switch (_name) {
            case "invulnerable": return 0.5f; break;
            case "molotov": return 1f; break;
            case "grounded": return 1f; break;
            case "burn": return 4f; break;
            case "burned": return 1f; break;
            case "binding-chains": return 3f; break;
            case "void-sphere": return 0.5f; break;
            case "frost-wave": return 4f; break;
            default: return 1f; break;
        }
    }

    public bool get_buff_has_attach (string _name) {
        switch (_name) {
            case "burn": case "binding-chains": case "frost-wave": 
                return true;
                break;
            default: return false; break;
        }
    }
    
    public void update_buff_trigger (InGameObject _obj, string _buff){
        switch (_buff) {
            case "burn":
                if (ContBuffs.I.get_has_buff (_obj, "burned")) return;
                
                ContDamage.I.lose_hp (_obj, 1, new List<string>{"burn"});
                ContBuffs.I.add_buff (_obj, "burned");
                break;
        }
    }
    
    public Vector3 get_buff_attach_offset (string _buff){
        Vector3 _ret = new Vector3 (0, 0, 0);
        
        switch (_buff) {
            case "burn": _ret.y = 0f; break;
        }
        
        return _ret;
    }

    public void calc_buff_speed_bonus (InGameObject _obj){
        float _spd = _obj.speed;

        foreach (ContBuffs.buff _b in _obj.buffs) {
            switch (_b.name) {
                // Increase

                // Decrease
                case "frost-wave": _spd -= 1.5f; break;
            }
        }
        if (_spd < 0) _spd = 0.001f;
        _obj.speed = _spd;
    }
}
