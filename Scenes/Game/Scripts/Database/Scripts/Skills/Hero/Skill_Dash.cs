using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Dash : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        float DIST = 3f;
        
        // INSTANT TELE - UNUSED
        /*
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();

        Vector2 _pos = gameObject.transform.position,
                _mousePos = InGameCamera.I.get_mouse_pos (),
                _mousePos_scrn = Input.mousePosition,
                _dir = _mousePos - _pos;
        float _ang = Mathf.Atan2 (_dir.y, _dir.x);

        ContObj.I.change_facing (_ownerComp, (Calculator.I.is_mouse_left_of_object (_ownerComp) ? "left" : "right"));
        ContObj.I.move_forward_instant (_ownerComp, _ang, DIST);
        InGameCamera.I.point_to_target ();
        */
        
        // PROPELL DASH
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position;

        ContObj.I.change_facing (_ownerComp, (Calculator.I.is_mouse_left_of_object (_ownerComp) ? "left" : "right"));
        ContObj.I.propell_to_angle (_ownerComp, Calculator.I.get_ang_from_point_and_mouse (_pos), 50f, 1f, 5f, "dash");
        InGameCamera.I.point_to_target ();

        ContEffect.I.create_effect ("move-smoke", _pos);

        SoundHandler.I.play_sfx ("dash-smoke");
    }
}
