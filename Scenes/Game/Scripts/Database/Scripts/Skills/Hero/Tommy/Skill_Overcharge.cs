using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Overcharge : SkillTrig
{
    public override void use_active()
    {
        if (!use_check()) return;

        InGameObject _ownerComp = gameObject.GetComponent<InGameObject>();
        _ownerComp.ultPerc = 0;

        Skill_GunShoot gunShootComp = _ownerComp.GetComponent<Skill_GunShoot>();
        if (gunShootComp != null) {
            gunShootComp.enabled = false;
            gunShootComp.skillSlot = "mouse1-off";
        }

        Skill_TommySuperAtk superAtkComp = _ownerComp.GetComponent<Skill_TommySuperAtk>();
        if (superAtkComp != null) {
            superAtkComp.shots = 0;
            superAtkComp.enabled = true;
            superAtkComp.skillSlot = "mouse1";
        }

        ContEffect.I.create_effect ("smoke-expand", gameObject.transform.position);
        ContEffect.I.create_effect ("magic-spark-seraphine", gameObject.transform.position);
        MUI_Overlay.I.show_overlay ("ult");
        SoundHandler.I.play_sfx ("magic");
    }
}
