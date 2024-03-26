using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Evt_ElectricSlashUpd : EvtTrig {

    private float countTime_Fx = 0, countTime_Dam = 0;

    public float RANGE, RANGE_KNOCK;
    public int DAM, DAM_PER_SKILL;
    public List<int> hitIDs;

    public bool isUsingSkill = false;

    private float DIST_PER_EXPLOSION_EFFECT, curDistExpEffect;
    Rigidbody2D rigidBody;

    public override void setup (){
        hitIDs = new List<int> ();

        DIST_PER_EXPLOSION_EFFECT = 0.5f;
        curDistExpEffect = DIST_PER_EXPLOSION_EFFECT;

        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    public override void use (){
        if (!isUsingSkill) return;

        InGameObject _owner = GetComponent <InGameObject> ();
        Vector2 _pos = _owner.transform.position;

        dam_nearby_units (_owner);

        curDistExpEffect -= rigidBody.velocity.magnitude;
        if (curDistExpEffect <= 0){
            curDistExpEffect = DIST_PER_EXPLOSION_EFFECT;
            ContEffect.I.create_effect ("explosion1_mini", _pos);
        }

        if (_owner.propellType != "dash") {
            isUsingSkill = false;
            List<InGameObject> _objs = ContObj.I.get_objs_in_area (_pos, RANGE_KNOCK);
            foreach (InGameObject _o in _objs) {
                if (!DB_Conditions.I.dam_condition (_owner, _o)) continue;

                ContObj.I.propell_to_angle (_o, Calculator.I.get_ang_from_2_points_deg (_pos, _o.transform.position), 40f, 1f, 5f, "knocked");
            }

            hitIDs.Clear ();
        }
    }

    private void dam_nearby_units (InGameObject _owner){
        List<InGameObject> _objs = ContObj.I.get_objs_in_area (_owner.transform.position, RANGE);

        foreach (InGameObject _o in _objs) {
            if (!DB_Conditions.I.dam_condition (_owner, _o) || hitIDs.Contains (_o.id)) continue;

            ContDamage.I.damage (_owner, _o, DAM + DAM_PER_SKILL * _owner.statSkill, new List<string>(){"electric"});
            ContEffect.I.create_effect ("explosion1", _o.transform.position);
            hitIDs.Add (_o.id);
        }
    }
}
