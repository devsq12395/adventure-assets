using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_GiantSlimeDash : SkillTrig {

    public string missileObj;

    public List<int> hitIDs;

    public bool isUsingSkill = false;

    public float DIST = 6f;
    public float RANGE_AOE, SPEED_KNOCK, DIST_KNOCK;
    private float DIST_PER_EXPLOSION_EFFECT, curDistExpEffect;

    public Afterimage afterimage;

    public override void on_start (){
        hitIDs = new List<int>();
    }
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position,
            _pPos = ContPlayer.I.player.gameObject.transform.position;

        ContObj.I.change_facing (_ownerComp, ((_pos.x > _pPos.x) ? "left" : "right"));
        ContObj.I.instant_move_upd_start_dist (_ownerComp, Calculator.I.get_ang_from_2_points_deg (_pos, _pPos), 0.5f, DIST, "enemy-dash");

        isUsingSkill = true; 
        afterimage = gameObject.AddComponent<Afterimage>();
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
            ContEffect.I.create_effect("smoke", _pos);
        }

        // End the slash skill if the movement is finished
        if (_owner.instMov_mode != "enemy-dash") {
            isUsingSkill = false;
            hitIDs.Clear();

            ContEffect.I.create_effect ("smoke-expand", gameObject.transform.position);
            Destroy (afterimage);
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
}
