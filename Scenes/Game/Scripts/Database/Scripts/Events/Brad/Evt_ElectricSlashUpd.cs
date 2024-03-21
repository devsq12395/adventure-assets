using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_ElectricSlashUpd : EvtTrig {

    private float countTime_Fx = 0, countTime_Dam = 0;

    private float RANGE;
    public int DAM;
    public List<int> hitIDs;

    public bool isUsingSkill = false;

    public override void setup (){
        RANGE = 3;
        DAM = 3;
        hitIDs = new List<int> ();
    }
    
    public override void use (){
        if (!isUsingSkill) return;

        InGameObject _owner = GetComponent <InGameObject> ();
        dam_nearby_units (_owner);

        if (_owner.propellType != "dash") {
            isUsingSkill = false;
        }
    }

    private void dam_nearby_units (InGameObject _owner){
        List<InGameObject> _objs = ContObj.I.get_objs_in_area (_owner.transform.position, RANGE);

        foreach (InGameObject _o in _objs) {
            if (!DB_Conditions.I.dam_condition (_owner, _o) || hitIDs.Contains (_o.id)) continue;

            ContDamage.I.damage (_owner, _o, DAM, new List<string>(){"electric"});
            ContEffect.I.create_effect ("explosion3", _o.transform.position);
            hitIDs.Add (_o.id);
        }
    }
}
