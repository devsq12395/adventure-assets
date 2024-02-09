using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Zombie : InGameAI {

    private Vector2 randPoint;

    public override void on_update (){
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position,
                _goPos = gameObject.transform.position;;

        // Debug.DrawRay(_goPos, _pPos-_goPos, Color.green);
        if (CheckLineOfSight(_goPos, _pPos)) {
            ContObj.I.move_walk_to_pos (inGameObj, _pPos);
        } else {
            pathfinding (_p, _pPos, _goPos);
        }
    }

    private void pathfinding(InGameObject _p, Vector2 _pPos, Vector2 _goPos) {
        float _firstA = Calculator.I.get_ang_from_2_points_deg (_goPos, _pPos);

        for (float _a = 1; _a <= 360; _a++) {
            if (checkAndMove(_goPos, _pPos, _firstA + _a)) break;
            if (checkAndMove(_goPos, _pPos, _firstA - _a)) break;
        }
    }

    private bool checkAndMove(Vector2 _goPos, Vector2 _pPos, float _a) {
        bool _ret = false;
        RaycastHit2D hit = Physics2D.Linecast(_goPos, Calculator.I.get_pos_on_dist (_goPos, _a, 3), LayerMask.GetMask("Collider"));

        if (hit.collider == null) {
            ContObj.I.move_walk_to_pos (inGameObj, Calculator.I.get_pos_on_dist (_goPos, _a, 200));
            return true;
        } else {
            return false;
        }
    }

    private bool CheckLineOfSight(Vector2 from, Vector2 to) {
        RaycastHit2D hit = Physics2D.Linecast(from, to, LayerMask.GetMask("Collider"));
        return hit.collider == null;
    }
}
