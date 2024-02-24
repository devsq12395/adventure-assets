using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_MolotovDth : EvtTrig {
    
    public override void use (){
        List<InGameObject> _objs = new List<InGameObject> ();
        InGameObject _missile = gameObject.GetComponent <InGameObject> ();
        
        ContEffect.I.create_effect ("explosion1", gameObject.transform.position);
        
        create_obj (gameObject.transform.position, _missile);
        for (int i = 0; i < 4; i++){
            create_obj (Calculator.I.get_next_point_in_direction (gameObject.transform.position, i * 90 + 45, 1), _missile);
        }
        for (int i = 0; i < 8; i++){
            create_obj (Calculator.I.get_next_point_in_direction (gameObject.transform.position, i * 45, 2), _missile);
        }
    }
    
    private InGameObject create_obj (Vector2 _pos, InGameObject _missile){
        InGameObject _ret = ContObj.I.create_obj ("molotovEfct", _pos, _missile.owner).GetComponent<InGameObject> ();
        _ret.timedLife = 7f;

        _ret.controllerID = _missile.controllerID;
        
        return _ret;
    }
}
