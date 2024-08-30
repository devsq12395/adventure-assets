using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTrig : MonoBehaviour {

    public string scriptTag = "skill";
    public string skillName;
    [Header("------ Skill Slots: mouse1, shift, Skill1, Skill2, Skill3 ------")]
    public string skillSlot = "mouse1"; // Use the input name like "Skill1" for the set skill1 key, or "shift" for the set shift key
        // OFFICIAL LIST: "mouse1", "shift", "Skill1", "Skill2", "Skill3"

    public int mc;
    public float cdMax = 0.5f, cd;

    void Update (){
        on_update ();
    }

    public virtual void use_active (){
        
    }

    public virtual void use_passive (){
        
    }

    public virtual void on_update (){

    }

    public bool use_check (){
        if (cd > 0) return false;
        
        InGameObject _igo = gameObject.GetComponent <InGameObject> ();
        if (_igo.mp < mc) return false;

        ContDamage.I.lose_mp (_igo, mc);
        cd = cdMax;
        return true;
    }
}
