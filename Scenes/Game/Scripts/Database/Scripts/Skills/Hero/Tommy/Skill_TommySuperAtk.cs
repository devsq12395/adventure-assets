using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TommySuperAtk : SkillTrig
{
    public string missileObj, muzzleObj1, muzzleObj2, soundToPlay;

    public int shots;

    public override void use_active()
    {
        if (!use_check()) return;

        InGameObject _ownerComp = gameObject.GetComponent<InGameObject>();

        ContObj.I.change_velocity(_ownerComp, new Vector2(0, 0));
        _ownerComp.isAtk = true;
        _ownerComp.toAnim = 1;
        ContObj.I.change_facing(_ownerComp, Calculator.I.is_mouse_left_of_object(_ownerComp) ? "left" : "right");

        // Determine the offset based on the facing direction
        float offsetX = _ownerComp.facing == "left" ? -0.7f : 0.7f;

        // Spawn the missile at the calculated position
        Vector3 missilePosition = gameObject.transform.position + new Vector3(offsetX, 0, 0);
        GameObject _missile = ContObj.I.create_obj(missileObj, missilePosition, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent<InGameObject>();

        if (_ownerComp.tags.Contains("hero"))
        {
            _missileComp.hitDam = _ownerComp.dam + _ownerComp.skill;
            _missileComp.range = _ownerComp.statRange * 4;
        }

        ContObj.I.const_move_ang_set(_missileComp, Calculator.I.get_ang_from_point_and_mouse(missilePosition), _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;

        // Play the sound effect
        SoundHandler.I.play_sfx(soundToPlay);

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

        shots++;
        if (shots > 7) {
            shots = 0;

            Skill_GunShoot gunShootComp = _ownerComp.GetComponent<Skill_GunShoot>();
            if (gunShootComp != null) {
                gunShootComp.enabled = true;
                gunShootComp.skillSlot = "mouse1";
            }

            Skill_TommySuperAtk superAtkComp = _ownerComp.GetComponent<Skill_TommySuperAtk>();
            if (superAtkComp != null) {
                superAtkComp.enabled = false;
                superAtkComp.skillSlot = "mouse1-off";
            }
        }
    }
}
