using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_VoidSphere : ColTrig {
    
    public int dam;
    public List<string> damTags;
    
    public override void on_hit_enemy (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_missile (_hit))  return;
        if (ContBuffs.I.get_has_buff (_hit, "void-sphere"))   return;

        InGameObject    _this = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_this.controllerID);

        ContEffect.I.create_effect ("explosion2", gameObject.transform.position);
        ContDamage.I.damage (_owner, _hit, dam, damTags);
        ContBuffs.I.add_buff (_hit, "void-sphere");
        ContBuffs.I.add_buff (_hit, "void-sphere-grounded");

    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
