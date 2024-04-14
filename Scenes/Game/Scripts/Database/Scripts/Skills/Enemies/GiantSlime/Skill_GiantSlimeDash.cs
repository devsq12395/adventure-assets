using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_GiantSlimeDash : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        float DIST = 6f;
        
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position;

        ContObj.I.change_facing (_ownerComp, (_pPos.x <= _pos.x ? "left" : "right"));
        ContObj.I.propell_to_angle (_ownerComp, Calculator.I.get_ang_from_2_points_deg (_pos, _pPos), 50f, 1f, 5f, "dash");

        Evt_GiantSlimeDashUpd _event = gameObject.GetComponent <Evt_GiantSlimeDashUpd>();
        _event.isUsingSkill = true;

        gameObject.GetComponents<BoxCollider2D> ()
            .Where(bc => !bc.isTrigger).ToList ()
            .ForEach (bc => bc.enabled = false);
    }
}
