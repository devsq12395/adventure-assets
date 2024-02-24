using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DefEnemyAtk : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();

        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();

        _missileComp.controllerID = _ownerComp.id;

        ContObj.I.change_velocity (_ownerComp, new Vector2 (0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;

        Vector2 _misPos = _missile.transform.position,
                _mousePos = ContPlayer.I.player.gameObject.transform.position,
                _dir = _mousePos - _misPos;
        float _ang = Mathf.Atan2 (_dir.y, _dir.x) * Mathf.Rad2Deg;

        ContObj.I.change_facing (_ownerComp, ((_dir.x < gameObject.transform.position.x) ? "left" : "right"));

        ContObj.I.const_move_ang_set (_missileComp, _ang, _missileComp.speed);
    }
}
