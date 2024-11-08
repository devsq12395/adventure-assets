using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_GenericCollect : ColTrig {

    public bool destroyOnHit = true;

    public override void on_hit_ally (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_collect (_hit) && !hitIDs.Contains (_hit.id)) return;

        InGameObject    _this = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_this.controllerID);

        ContEffect.I.create_effect (_this.onHitSFX, _this.gameObject.transform.position);

        switch (_this.name){
            case "gold-1":

                GameUI_InGameTxt.I.create_ingame_txt ($"{_dam.ToString ()}", _def.gameObject.transform.position, 2f);
                break;
        }

        if (destroyOnHit) {
            Destroy (gameObject);
        } else {
            hitIDs.Add (_hit.id);
        }
    }

    public override void on_hit_enemy (InGameObject _hit){
        
    }
}
