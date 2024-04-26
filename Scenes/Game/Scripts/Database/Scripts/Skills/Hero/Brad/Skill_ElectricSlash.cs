using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_ElectricSlash : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        float DIST = 6f;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position;

        ContObj.I.change_facing (_ownerComp, (Calculator.I.is_mouse_left_of_object (_ownerComp) ? "left" : "right"));
        ContObj.I.instant_move_upd_start_dist (_ownerComp, Calculator.I.get_ang_from_point_and_mouse (_pos), 0.5f, 9f, "electric-slash");
        InGameCamera.I.point_to_target ();

        Evt_ElectricSlashUpd _event = gameObject.GetComponent <Evt_ElectricSlashUpd>();
        _event.isUsingSkill = true;

        gameObject.GetComponents<BoxCollider2D> ()
            .Where(bc => !bc.isTrigger).ToList ()
            .ForEach (bc => bc.enabled = false);

        ContBuffs.I.add_buff (_ownerComp, "invulnerable"); //0.5f default dur
    }
}
