using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_GoblinMageFireballShotgun : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos));
        create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) + 25);
        create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) - 25);
    }

    private void create_missile (InGameObject _ownerComp, float _ang){
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();
        _missileComp.hitDam = _ownerComp.dam;

        ContObj.I.const_move_ang_set (_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;
    }
}
