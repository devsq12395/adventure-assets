using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_Molotov : ColTrig {
    
    public int dam;
    public List<string> damTags;
    
    public override void on_hit_enemy (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_missile (_hit))  return;
        if (ContBuffs.I.get_has_buff (_hit, "molotov"))   return;

        InGameObject    _this = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_this.controllerID);

        ContDamage.I.damage (_owner, _hit, dam, damTags);
        ContBuffs.I.add_buff (_hit, "molotov");
        ContBuffs.I.add_buff (_hit, "burn");
    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
