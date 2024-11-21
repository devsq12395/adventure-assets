using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_AxeSkeleton : SkillTrig {

    public string missileObj;
    public Vector3 targetPoint;
    
    public override void use_active() {
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent<InGameObject>();

        GameObject _missile = ContObj.I.create_obj(missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent<InGameObject>();

        _missileComp.controllerID = _ownerComp.id;
        _missileComp.hitDam = _ownerComp.dam;

        ContObj.I.change_velocity(_ownerComp, Vector2.zero);
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;

        Vector2 _misPos = gameObject.transform.position,
                _targetPos = targetPoint,
                _dir = _targetPos - _misPos;
        float _ang = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        ContObj.I.change_facing(_ownerComp, ((_targetPos.x < gameObject.transform.position.x) ? "left" : "right"));

        ContObj.I.const_move_ang_set(_missileComp, _ang, _missileComp.speed);

        Mortar mortar = _missile.GetComponent<Mortar>();
        mortar.targetPoint = targetPoint;
    }
}
