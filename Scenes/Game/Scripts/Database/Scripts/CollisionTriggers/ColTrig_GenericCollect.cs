using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColTrig_GenericCollect : ColTrig {

    public bool destroyOnHit = true;

    public string collectType, storage1, storage2, storage3;

    public override void on_hit_ally (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_collect (_hit) && !hitIDs.Contains (_hit.id)) return;

        InGameObject    _this = GetComponent <InGameObject> (),
                        _owner = ContObj.I.get_obj_with_id (_this.controllerID);

        ContEffect.I.create_effect (_this.onHitSFX, _this.gameObject.transform.position);

        int intStorage1, intStorage2, intStorage3;
        switch (collectType){
            case "gold":
                int.TryParse (storage1, out intStorage1);
                SaveHandler.I.gain_gold (intStorage1);
                GameUI_InGameTxt.I.create_ingame_txt ($"+{intStorage1}", _hit.gameObject.transform.position, 2f, new Color(1.0f, 0.84f, 0.0f));
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
