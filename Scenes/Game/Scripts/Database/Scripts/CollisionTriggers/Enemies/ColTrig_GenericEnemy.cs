using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_GenericEnemy : ColTrig {
    public override void on_hit_enemy (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_missile (_hit)) return;

        InGameObject _this = GetComponent <InGameObject> ();

        ContEffect.I.create_effect (_this.onHitSFX, _hit.gameObject.transform.position);
        ContDamage.I.damage (_this, _hit, _this.hitDam, _this.tags);

        // Calculate knockback direction and angle
        Vector3 knockDirection = _hit.transform.position - transform.position;
        float knockAngle = Mathf.Atan2(knockDirection.y, knockDirection.x) * Mathf.Rad2Deg;

        // Add knockback effect using ForcedMove
        ForcedMove knockback = _hit.gameObject.AddComponent<ForcedMove>();
        knockback.setup_forced_move(15.0f, 1f, knockAngle, ForcedMove.MoveMode.Knockback, "meleeKnockback");
    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
