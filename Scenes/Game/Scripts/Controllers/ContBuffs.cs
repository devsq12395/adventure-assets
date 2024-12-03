using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContBuffs : MonoBehaviour {

    public static ContBuffs I;
	public void Awake(){ I = this; }

    public struct buff {
        public string name;
        public float dur;
        public GameObject attach;
        public InGameObject owner;
        public Vector3 atchOffset;

        public buff (string _name) {
            name = _name;
            attach = null;

            dur = 1;
            owner = null;
            
            atchOffset = new Vector3 (0, 0, 0);
        }
    }

    public void update_buffs (InGameObject _obj){
        if (!_obj.gameObject) return;
        
        List<int> _toRmv = new List<int> ();

        ContBuffs.buff _cur;
        for (int i = 0; i < _obj.buffs.Count; i++) {
            _cur = _obj.buffs [i];
            _cur.dur -= Time.deltaTime;
            _obj.buffs [i] = _cur;

            if (_cur.dur <= 0 || !_cur.owner ) {
                remove_buff (_obj, _obj.buffs [i].name, false);
                _toRmv.Add (i);
            } else {
                DB_Buffs.I.update_buff_trigger (_obj, _cur.name);
                
                if (_cur.attach) {
                    _cur.attach.transform.position = new Vector3(
                        x: _obj.gameObject.transform.position.x + _cur.atchOffset.x,
                        y: _obj.gameObject.transform.position.y + _cur.atchOffset.y,
                        z: _obj.gameObject.transform.position.z - 1 + _cur.atchOffset.z
                    );
                }
            }
        }

        for (int i = _toRmv.Count - 1; i >= 0; i--) {
            _obj.buffs.RemoveAt (_toRmv [i]);
        }
    }

    public bool get_has_buff (InGameObject _obj, string _buff) {
        return _obj.buffs.Any(_cur => _cur.name == _buff);
    }

    public void add_buff (InGameObject _targ, string _buffName) {
        if (get_has_buff (_targ, _buffName)) remove_buff (_targ, _buffName);

        buff _new = DB_Buffs.I.get_buff_data (_buffName);
        _new.owner = _targ;
        _targ.buffs.Add (_new);
        if (_new.attach != null){
            _new.attach.transform.localScale *= _targ.attachScaleMultiplier;
        }

        DB_Buffs.I.calc_buff_speed_bonus (_targ);
    }

    public void remove_buff (InGameObject _targ, string _buffName, bool _remFromArray = true){
        int _i  = _targ.buffs.FindIndex(b => b.name == _buffName);

        destroy_buff (_targ, _i, _remFromArray);
    }

    public void remove_all_buffs (InGameObject _targ){
        for (int _i = _targ.buffs.Count - 1; _i >= 0; _i--) {
            destroy_buff (_targ, _i, true);
        }
    }

    private void destroy_buff (InGameObject _targ, int _i, bool _remFromArray = true){
        if (_i < 0 || _i >= _targ.buffs.Count) return;

        if (_i != -1) {
            Destroy (_targ.buffs [_i].attach);
            if (_remFromArray) _targ.buffs.RemoveAt (_i);
        }

        DB_Buffs.I.calc_buff_speed_bonus (_targ);
    }
}
