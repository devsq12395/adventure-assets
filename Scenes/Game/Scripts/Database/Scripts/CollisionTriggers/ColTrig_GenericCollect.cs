using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColTrig_GenericCollect : ColTrig {

    public bool destroyOnHit = true, isCollected = false;

    public string collectType, storage1, storage2, storage3;

    public override void on_hit_collectible (InGameObject _hit){
        if (!DB_Conditions.I.coll_cond_collect (_hit) && !hitIDs.Contains (_hit.id) || isCollected) return;

        InGameObject _this = GetComponent <InGameObject> ();

        int intStorage1, intStorage2, intStorage3;
        switch (collectType){
            case "gold":
                int.TryParse (storage1, out intStorage1);
                SaveHandler.I.gain_gold (intStorage1);
                GameUI_InGameTxt.I.create_ingame_txt ($"+{intStorage1} Gold", _hit.gameObject.transform.position, 2f, new Color(1.0f, 0.84f, 0.0f));
                SoundHandler.I.play_sfx ("gold-get");
                break;
        }

        isCollected = true;
        if (destroyOnHit) {
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(gameObject));
        } else {
            hitIDs.Add (_hit.id);
        }
    }

    public override void on_hit_enemy (InGameObject _hit){
        
    }
}
