using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ShotgunEnemyWithMuzzle : SkillTrig {

    public string missileObj, muzzleObj1, muzzleObj2;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos));
        create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) + 25);
        create_missile (_ownerComp, Calculator.I.get_ang_from_2_points_deg (gameObject.transform.position, _pPos) - 25);

        // Determine the offset based on the facing direction
        float offsetX = _ownerComp.facing == "left" ? -0.7f : 0.7f;

        // Spawn muzzle objects at the specified positions
        Vector3 muzzlePosition1 = gameObject.transform.position + new Vector3(offsetX, 0, 0);
        Vector3 muzzlePosition2 = gameObject.transform.position + new Vector3(offsetX * 2, 0, 0);

        // Create muzzle objects
        GameObject muzzle1 = ContEffect.I.create_effect(muzzleObj1, muzzlePosition1);
        GameObject muzzle2 = ContEffect.I.create_effect(muzzleObj2, muzzlePosition2);

        // Rotate the muzzle objects if facing left
        if (_ownerComp.facing == "left") {
            muzzle1.transform.rotation = Quaternion.Euler(0, 180, 0);
            muzzle2.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void create_missile (InGameObject _ownerComp, float _ang){
        GameObject _missile = ContObj.I.create_obj (missileObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent <InGameObject> ();
        _missileComp.hitDam = _ownerComp.dam;

        ContObj.I.const_move_ang_set (_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;
    }
}
