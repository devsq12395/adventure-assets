using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameOnAtkEnd : StateMachineBehaviour
{
    override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        InGameObject _obj = animator.GetComponent <InGameObject> ();

        _obj.isAtk = false;
    }
}
