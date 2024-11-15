using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Dash : SkillTrig {

    public string missileObj;
    public bool dashing;

    public Afterimage afterimage;
    
    public override void use_active (){
        if (!use_check()) return;
        if (!ContPlayer.I.check_and_use_stamina (3)) return;
        
        float DIST = 3f;
        
        // PROPELL DASH
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
        if (!DB_Conditions.I.can_move (_ownerComp)) return;
        Vector2 _pos = gameObject.transform.position;

        // Get angle from WASD input instead of mouse position
        float angle = Calculator.I.get_ang_from_wasd();
        
        if (angle != -1) { // Check if a valid angle is returned
            ContObj.I.change_facing(_ownerComp, (angle > 90f && angle < 270f) ? "left" : "right");
            ContObj.I.const_move_ang_with_dist(_ownerComp, angle, 4f, 0.7f);
            dashing = true;
            _ownerComp.isInvul = true;
            afterimage = gameObject.AddComponent<Afterimage>();

            ContEffect.I.create_effect("move-smoke", _pos);

            SoundHandler.I.play_sfx("dash-smoke");
        }

        ContObj.I.set_jump (_ownerComp, 3, 0.15f);
    }

    public override void on_update (){
        if (dashing) {
            InGameObject _ownerComp = gameObject.GetComponent <InGameObject> ();
            InGameCamera.I.point_to_target ();

            if (!_ownerComp.constMovAng_isOn) {
                dashing = false;
                _ownerComp.isInvul = false;
                Destroy (afterimage);
            }
        } 
    }
}
