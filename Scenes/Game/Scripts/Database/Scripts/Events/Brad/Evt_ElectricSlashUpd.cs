using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Evt_ElectricSlashUpd : EvtTrig {

    private float countTime_Fx = 0, countTime_Dam = 0;

    public float RANGE_AOE, RANGE_FINAL_KNOCK, AOE_KNOCK, SPEED_KNOCK, DIST_KNOCK;
    public int DAM, DAM_PER_SKILL;
    public List<int> hitIDs;

    public bool isUsingSkill = false;

    private float DIST_PER_EXPLOSION_EFFECT, curDistExpEffect;

    public override void setup (){
        hitIDs = new List<int> ();

        DIST_PER_EXPLOSION_EFFECT = 1f;
        curDistExpEffect = DIST_PER_EXPLOSION_EFFECT;
    }
    
    public override void use (){
        if (!isUsingSkill) return;

        InGameObject _owner = GetComponent <InGameObject> ();
        Vector2 _pos = _owner.transform.position;

        dam_nearby_units (_owner);

        curDistExpEffect -= _owner.instMov_spd;
        if (curDistExpEffect <= 0){
            curDistExpEffect = DIST_PER_EXPLOSION_EFFECT;
            ContEffect.I.create_effect ("explosion1_mini", _pos);
        }

        if (_owner.instMov_mode != "electric-slash") {
            isUsingSkill = false;
            hitIDs.Clear ();

            List<InGameObject> _objs = ContObj.I.get_objs_in_area (_pos, RANGE_FINAL_KNOCK);
            foreach (InGameObject _o in _objs) {
                if (!DB_Conditions.I.dam_condition (_owner, _o)) continue;

                ContObj.I.instant_move_upd_start_dist (_o, Calculator.I.get_ang_from_2_points_deg (_pos, _o.transform.position), SPEED_KNOCK, DIST_KNOCK);
            }
        }
    }

    private void dam_nearby_units (InGameObject _owner){
        Vector2 _pos = _owner.transform.position;
        List<InGameObject> _objs = ContObj.I.get_objs_in_area (_pos, RANGE_AOE);

        foreach (InGameObject _o in _objs) {
            if (!DB_Conditions.I.dam_condition (_owner, _o) || hitIDs.Contains (_o.id)) continue;

            ContDamage.I.damage (_owner, _o, DAM + DAM_PER_SKILL * _owner.statSkill, new List<string>(){"electric"});
            ContObj.I.instant_move_upd_start_dist (_o, Calculator.I.get_ang_from_2_points_deg (_pos, _o.transform.position), SPEED_KNOCK, DIST_KNOCK);
            ContEffect.I.create_effect ("explosion1", _o.transform.position);
            hitIDs.Add (_o.id);
        }
    }
}
