using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DestroyerSlash : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();

        ContObj.I.change_velocity (_ownerComp, new Vector2 (0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;

        _ownerComp.ultPerc = 0;
        
        create_missile (_ownerComp, Calculator.I.get_ang_from_point_and_mouse (gameObject.transform.position));
        create_missile (_ownerComp, Calculator.I.get_ang_from_point_and_mouse (gameObject.transform.position) + 25);
        create_missile (_ownerComp, Calculator.I.get_ang_from_point_and_mouse (gameObject.transform.position) - 25);

        SoundHandler.I.play_sfx("dash");
        SoundHandler.I.play_sfx("magic");
        ContEffect.I.create_effect ("smoke-expand", gameObject.transform.position);
        ContEffect.I.create_effect ("bindChainExp1", gameObject.transform.position);
        MUI_Overlay.I.show_overlay ("ult");
    }

    private void create_missile (InGameObject _ownerComp, float _ang){
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();
        _missileComp.hitDam = _ownerComp.skill;

        ContObj.I.const_move_ang_set (_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;
    }
}
