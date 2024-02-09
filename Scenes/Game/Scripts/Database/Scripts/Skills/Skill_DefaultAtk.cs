using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DefaultAtk : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();

        ContObj.I.change_velocity (_ownerComp, new Vector2 (0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;
        ContObj.I.change_facing (_ownerComp, Calculator.I.is_mouse_left_of_object (_ownerComp) ? "left" : "right");
        
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();
        ContObj.I.const_move_ang_set (_missileComp, Calculator.I.get_ang_from_point_and_mouse (gameObject.transform.position), _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;
    }
}
