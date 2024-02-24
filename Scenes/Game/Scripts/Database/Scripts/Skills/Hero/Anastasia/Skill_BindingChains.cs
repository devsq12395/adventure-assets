using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BindingChains : SkillTrig {
    
    public override void use_active (){
        if (!use_check()) return;
        
        int DAM = 4;
        float RANGE = 10f;
        List<string> damTags = new List<string>(){"electric"};

        InGameObject _owner = gameObject.GetComponent <InGameObject> ();
        Vector2 _pos = gameObject.transform.position;

        ContEffect.I.create_effect ("bindChainExp1", _owner.transform.position);

        List<InGameObject> _objsInArea = ContObj.I.get_objs_in_area (_pos, RANGE);

        foreach (InGameObject _o in _objsInArea) {
            if (!DB_Conditions.I.debuff_condition (_owner, _o)) continue;

            ContEffect.I.create_effect ("bindChainExp1", _o.gameObject.transform.position);
            ContBuffs.I.add_buff (_o, "binding-chains");
            GameUI_InGameTxt.I.create_ingame_txt (DB_Strings.I.get_str ("Binded!"), _o.gameObject.transform.position, 2f);
            ContDamage.I.damage (_owner, _o, DAM, damTags);
        }
    }
}
