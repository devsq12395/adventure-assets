using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_EmbracedDash : SkillTrig {

    public string missileObj;
    public float DIST = 6f, SPEED = 0.25f;
    
    public override void use_active (){
        if (!use_check()) return;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position;

        ContObj.I.change_facing (_ownerComp, (Calculator.I.is_mouse_left_of_object (_ownerComp) ? "left" : "right"));
        ContObj.I.instant_move_upd_start_dist (_ownerComp, Random.Range (0, 360), SPEED, DIST, "enemy-dash");
        InGameCamera.I.point_to_target ();

        Evt_GiantSlimeDashUpd _event = gameObject.GetComponent <Evt_GiantSlimeDashUpd>();
        _event.isUsingSkill = true; 
    }
}
