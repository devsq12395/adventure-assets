using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SampleAssist : SkillTrig {

    public string missileObj;
    
    public override void use_active (){
        if (!use_check()) return;
        
        Vector2 _pos = ContPlayer.I.player.gameObject.transform.position;
        GameObject _obj = ContObj.I.create_obj ("SampleAssist", _pos, 1);
        InGameObject _igo = _obj.GetComponent <InGameObject> ();

        ContObj.I.change_facing (_igo, ContPlayer.I.player.facing);
    }
}
