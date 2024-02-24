using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_BlizzardUpd : EvtTrig {

    private float countTime_Fx = 0, countTime_Dam = 0;

    private float RANGE;
    public int DAM;

    public override void setup (){
        RANGE = 8;
        DAM = 3;
    }
    
    public override void use (){
        countTime_Fx += Time.deltaTime;
        countTime_Dam += Time.deltaTime;

        InGameObject    _dummy = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_dummy.controllerID);

        if (countTime_Fx >= 0.4f) {
            countTime_Fx = 0;
            create_blast (_dummy.transform.position, _dummy);
        }

        if (countTime_Dam >= 0.5f) {
            countTime_Dam = 0;
            dam_nearby_units (_owner, _dummy);
        }
    }

    private void dam_nearby_units (InGameObject _owner, InGameObject _dummy){
        List<InGameObject> _objs = ContObj.I.get_objs_in_area (_dummy.transform.position, RANGE);

        foreach (InGameObject _o in _objs) {
            if (!DB_Conditions.I.dam_condition (_owner, _o)) continue;

            ContDamage.I.damage (_owner, _o, DAM, _dummy.tags);
            ContEffect.I.create_effect ("explosion3", _o.transform.position);
        }
    }
    
    private void create_blast (Vector2 _pos, InGameObject _dummy){
        for (int i = 1; i <= 4; i++) {
            ContEffect.I.create_effect ("explosion3", new Vector2 (
                _pos.x + Random.Range (-RANGE / 2, RANGE / 2),
                _pos.y + Random.Range (-RANGE / 2, RANGE / 2)
            ));
        }
    }
}
