using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_VoidSphere : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();

        ContObj.I.change_velocity (_ownerComp, new Vector2 (0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;
        
        Vector2 _pos = gameObject.transform.position;
        float _ang = Calculator.I.get_ang_from_point_and_mouse (_pos);
        
        InGameObject _msl = ContObj.I.create_missile (missileObj, gameObject.transform.position, _ownerComp.owner, _ang).GetComponent <InGameObject> ();
        ContObj.I.const_move_ang_set (_msl, Calculator.I.get_ang_from_point_and_mouse (gameObject.transform.position), _msl.speed);
        _msl.timedLife = 1.5f;

        _msl.controllerID = _ownerComp.id;
    }
}
