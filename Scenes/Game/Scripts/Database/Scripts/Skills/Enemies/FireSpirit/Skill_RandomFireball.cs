using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RandomFireball : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;

        create_missile (_ownerComp, Random.Range (0, 360));
    }

    private void create_missile (InGameObject _ownerComp, float _ang){
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();

        ContObj.I.const_move_ang_set (_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;
    }
}
