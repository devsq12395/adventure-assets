using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_GiantSlimeShotgun : SkillTrig {

    public string missileObj;

    public int lastPattern;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        ContObj.I.change_velocity (_ownerComp, new Vector2 (0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;

        switch (lastPattern) {
            case 0:
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos));
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) + 25);
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) + 50);
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) - 25);
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) - 50);
                break;
            case 1:
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos));
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) + 25);
                create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) - 25);
                break;
        }
        lastPattern++;
        if (lastPattern > 1) lastPattern = 0;

        SoundHandler.I.play_sfx ("explosion");
    }

    private void create_missile (InGameObject _ownerComp, float _ang){
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();
        _missileComp.hitDam = _ownerComp.skill;

        ContObj.I.const_move_ang_set (_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;
    }
}
