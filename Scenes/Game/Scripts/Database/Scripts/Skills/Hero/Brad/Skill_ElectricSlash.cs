using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ElectricSlash : SkillTrig {

    public string missileObj, frontSlashObj, explodeSlashObj;

    private float countTime_Fx = 0, countTime_Dam = 0, dashAngle;

    public float RANGE_AOE, RANGE_FINAL_KNOCK, AOE_KNOCK, SPEED_KNOCK, DIST_KNOCK;
    public int DAM, DAM_PER_SKILL;
    public List<int> hitIDs;

    public bool isUsingSkill = false;

    private float DIST_PER_EXPLOSION_EFFECT, curDistExpEffect;
    
    private GameObject slashInstance; // Store the front slash instance
    private InGameObject ownerComp; // Cache the owner component for reuse
    
    public override void on_start (){
        hitIDs = new List<int>();
    }

    public override void use_active (){
        if (!use_check()) return;
        
        float DIST = 6f;
        
        ownerComp = gameObject.GetComponent<InGameObject>();
        if (!DB_Conditions.I.can_move(ownerComp)) return;

        Vector2 _pos = gameObject.transform.position;

        // Determine the owner's facing direction and movement
        dashAngle = Calculator.I.get_ang_from_point_and_mouse(_pos);
        ContObj.I.change_facing(ownerComp, (Calculator.I.is_mouse_left_of_object(ownerComp) ? "left" : "right"));
        ContObj.I.instant_move_upd_start_dist(ownerComp, dashAngle, 0.5f, 9f, "electric-slash");
        InGameCamera.I.point_to_target();

        ContBuffs.I.add_buff(ownerComp, "invulnerable"); // 0.5s default duration

        SoundHandler.I.play_sfx("dash");
        MUI_Overlay.I.show_overlay("zoom");

        // Create the front slash object in front of the owner
        create_front_slash(ownerComp);

        isUsingSkill = true;
    }

    public override void on_update() {
        if (!isUsingSkill) return;

        InGameObject _owner = GetComponent<InGameObject>();
        Vector2 _pos = _owner.transform.position;

        // Handle damage to nearby units
        dam_nearby_units(_owner);

        // Move explosion effects
        curDistExpEffect -= _owner.instMov_spd;
        if (curDistExpEffect <= 0) {
            curDistExpEffect = DIST_PER_EXPLOSION_EFFECT;
            ContEffect.I.create_effect("explosion1_mini", _pos);
        }

        // Update the front slash's position to follow the owner
        if (slashInstance != null) {
            update_front_slash_position(_owner);
        }

        // End the slash skill if the movement is finished
        if (_owner.instMov_mode != "electric-slash") {
            isUsingSkill = false;
            hitIDs.Clear();

            ContEffect.I.create_effect ("smoke-expand", gameObject.transform.position);

            // Apply final knockback to units in the area
            List<InGameObject> _objs = ContObj.I.get_objs_in_area(_pos, RANGE_FINAL_KNOCK);
            foreach (InGameObject _o in _objs) {
                if (!DB_Conditions.I.dam_condition(_owner, _o)) continue;

                ContObj.I.instant_move_upd_start_dist(_o, Calculator.I.get_ang_from_2_points_deg(_pos, _o.transform.position), SPEED_KNOCK, DIST_KNOCK);
            }

            // Create missiles
            for (var i = 0; i < 8; i++) {
                InGameObject _missile = create_missile(ownerComp, 45 * i);
                _missile.timedLife = 0.4f;
            }

            // Destroy the front slash object
            if (slashInstance != null) {
                Destroy(slashInstance);
                slashInstance = null;
            }
        }
    }

    private void dam_nearby_units(InGameObject _owner) {
        Vector2 _pos = _owner.transform.position;
        List<InGameObject> _objs = ContObj.I.get_objs_in_area(_pos, RANGE_AOE);

        foreach (InGameObject _o in _objs) {
            if (!DB_Conditions.I.dam_condition(_owner, _o) || hitIDs.Contains(_o.id)) continue;

            ContDamage.I.damage(_owner, _o, _owner.skill, new List<string> { "electric" });
            ContObj.I.instant_move_upd_start_dist(_o, Calculator.I.get_ang_from_2_points_deg(_pos, _o.transform.position), SPEED_KNOCK, DIST_KNOCK);
            ContEffect.I.create_effect("explosion1", _o.transform.position);
            hitIDs.Add(_o.id);
        }
    }

    private InGameObject create_missile(InGameObject _ownerComp, float _ang) {
        GameObject _missile = ContObj.I.create_obj(explodeSlashObj, gameObject.transform.position, _ownerComp.owner);
        InGameObject _missileComp = _missile.GetComponent<InGameObject>();
        _missileComp.hitDam = _ownerComp.skill;

        ContObj.I.const_move_ang_set(_missileComp, _ang, _missileComp.speed);

        _missileComp.controllerID = _ownerComp.id;

        return _missileComp;
    }

    // Create the front slash object at the front of the owner
    private void create_front_slash(InGameObject _ownerComp) {
        Vector2 ownerPos = _ownerComp.transform.position;

        // Calculate the initial position of the front slash in front of the owner
        Vector2 offset = Calculator.I.get_offset_from_angle_and_distance(dashAngle, 0.75f); // Distance in front
        Vector2 slashPos = ownerPos + offset;

        // Create the slash object at the calculated position
        slashInstance = ContObj.I.create_obj(frontSlashObj, slashPos, _ownerComp.owner);

        // Set the slashInstance's rotation to match the dash angle
        slashInstance.transform.rotation = Quaternion.Euler(0, 0, dashAngle);
    }

    // Update the position of the front slash to follow the owner
    private void update_front_slash_position(InGameObject _ownerComp) {
        Vector2 ownerPos = _ownerComp.transform.position;

        // Maintain a fixed distance in front of the owner
        Vector2 offset = Calculator.I.get_offset_from_angle_and_distance(dashAngle, 1.5f);
        Vector2 slashPos = ownerPos + offset;

        // Update the slash's position
        slashInstance.transform.position = slashPos;

        // Update the slashInstance's rotation to match the dash direction
        slashInstance.transform.rotation = Quaternion.Euler(0, 0, dashAngle);
    }

}
