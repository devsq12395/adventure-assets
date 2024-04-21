using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_ShotgunWave : ColTrig {
    public List<string> damTags;
    public List<int> hitIDs;

    public int DAM, DAM_PER_SKILL;

    public override void on_setup (){
        hitIDs = new List<int> ();
    }
    
    public override void on_hit_enemy (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_missile (_hit) && !hitIDs.Contains (_hit.id))  return;

        InGameObject    _this = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_this.controllerID);

        ContEffect.I.create_effect ("explosion2", gameObject.transform.position);
        ContDamage.I.damage (_owner, _hit, DAM + DAM_PER_SKILL * _owner.statSkill, damTags);

        hitIDs.Add (_hit.id);
    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
