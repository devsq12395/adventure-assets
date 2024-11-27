using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DefaultAtk_NoStop : SkillTrig {

    public string missileObj, soundToPlay;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();

        if (_ownerComp.tags.Contains ("hero")) {
            _missileComp.hitDam = _ownerComp.statAttack;
            _missileComp.range = _ownerComp.statRange * 4;
        }

        ContObj.I.const_move_ang_set (_missileComp, Calculator.I.get_ang_from_point_and_mouse (gameObject.transform.position), _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;

        SoundHandler.I.play_sfx (soundToPlay);
    }
}
