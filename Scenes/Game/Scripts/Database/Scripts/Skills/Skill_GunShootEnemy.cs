using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_GunShootEnemy : SkillTrig
{
    public string missileObj, muzzleObj1, muzzleObj2;

    public override void use_active()
    {
        if (!use_check()) return;

        InGameObject _ownerComp = gameObject.GetComponent<InGameObject>();

        ContObj.I.change_velocity(_ownerComp, new Vector2(0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;

        // Determine the facing direction based on the player's position
        Vector2 _misPos = gameObject.transform.position;
        Vector2 _playerPos = ContPlayer.I.player.gameObject.transform.position;
        Vector2 _dir = _playerPos - _misPos;
        float _ang = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;

        // Set the facing direction of the owner
        ContObj.I.change_facing(_ownerComp, (_playerPos.x < gameObject.transform.position.x) ? "left" : "right");

        // Determine the offset based on the facing direction
        float offsetX = _ownerComp.facing == "left" ? -0.7f : 0.7f;

        // Spawn the missile at the calculated position
        Vector3 missilePosition = gameObject.transform.position + new Vector3(offsetX, 0, 0);
        GameObject _missile = ContObj.I.create_obj(missileObj, missilePosition, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent<InGameObject>();
        _missileComp.hitDam = _ownerComp.dam;

        ContObj.I.const_move_ang_set(_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;

        // Spawn muzzle objects at the specified positions
        Vector3 muzzlePosition1 = gameObject.transform.position + new Vector3(offsetX, 0, 0);
        Vector3 muzzlePosition2 = gameObject.transform.position + new Vector3(offsetX * 2, 0, 0);

        // Create muzzle objects
        GameObject muzzle1 = ContEffect.I.create_effect(muzzleObj1, muzzlePosition1);
        GameObject muzzle2 = ContEffect.I.create_effect(muzzleObj2, muzzlePosition2);

        // Rotate the muzzle objects if facing left
        if (_ownerComp.facing == "left")
        {
            muzzle1.transform.rotation = Quaternion.Euler(0, 180, 0);
            muzzle2.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
