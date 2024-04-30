using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_GiantSlimeDash : SkillTrig {

    public string missileObj;
    public float DIST = 6f;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position,
            _pPos = ContPlayer.I.player.gameObject.transform.position;

        ContObj.I.change_facing (_ownerComp, (Calculator.I.is_mouse_left_of_object (_ownerComp) ? "left" : "right"));
        ContObj.I.instant_move_upd_start_dist (_ownerComp, Calculator.I.get_ang_from_2_points_deg (_pos, _pPos), 0.5f, DIST, "enemy-dash");
        InGameCamera.I.point_to_target ();

        Evt_GiantSlimeDashUpd _event = gameObject.GetComponent <Evt_GiantSlimeDashUpd>();
        _event.isUsingSkill = true; 
    }
}
