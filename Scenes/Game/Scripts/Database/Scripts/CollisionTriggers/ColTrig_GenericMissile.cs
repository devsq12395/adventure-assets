using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_GenericMissile : ColTrig {

    public bool destroyOnHit;

    public override void on_hit_enemy (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_missile (_hit) && !hitIDs.Contains (_hit.id)) return;

        InGameObject    _this = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_this.controllerID);

        ContEffect.I.create_effect (_this.onHitSFX, _this.gameObject.transform.position);
        ContDamage.I.damage (_owner, _hit, _this.hitDam, _this.tags);

        if (destroyOnHit) {
            Destroy (gameObject);
        } else {
            hitIDs.Add (_hit.id);
        }
    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
