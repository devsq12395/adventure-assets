using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_GenericEnemy : ColTrig {
    public override void on_hit_enemy (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_missile (_hit)) return;

        InGameObject _this = GetComponent <InGameObject> ();

        ContEffect.I.create_effect (_this.onHitSFX, _hit.gameObject.transform.position);
        ContDamage.I.damage (_this, _hit, _this.hitDam, _this.tags);

        // Propell player on hit
        float _ang = Calculator.I.get_ang_from_2_points_rad (gameObject.gameObject.transform.position, _hit.transform.position) * Mathf.Rad2Deg;
        ContObj.I.propell_to_angle (_hit, _ang, 15f, 1f, 5f, "knocked");
    }

    public override void on_hit_ally (InGameObject _hit){
        
    }
}
