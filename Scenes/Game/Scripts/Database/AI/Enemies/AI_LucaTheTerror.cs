using System.Collections;
using UnityEngine;

public class AI_LucaTheTerror : InGameAI {

    public float channelingTime = 2f; // Duration for channeling effect
    public float effectInterval = 0.25f; // Interval between effect spawns

    private bool isChanneling = false;
    private ObjGlitter objGlitterComponent;

    private void Start() {
        // Initialize the ObjGlitter component
        objGlitterComponent = gameObject.GetComponent<ObjGlitter>();
    }

    public override void on_update() {
        InGameObject _p = ContPlayer.I.player;
        Vector2 _pPos = _p.gameObject.transform.position;

        ContObj.I.face_player(inGameObj);

        stateTime += Time.deltaTime;
        if (isChanneling) return;

        if (state == 0) {
            if (stateTime >= 3.5f) {
                stateTime = 0;
                state = 1;
            }
        } else if (state == 1) {
            if (Calculator.I.get_dist_from_2_points(gameObject.transform.position, _pPos) <= 7f || 
                stateTime >= 1f) {
                ContObj.I.move_walk_to_pos_stop(inGameObj);

                stateTime = 0;
                state = 2;
            }
        } else if (state <= 7) {
            if (stateTime >= 0.25f) {
                ContObj.I.use_skill_active(inGameObj, "attack");

                stateTime = 0;
                state++;
            }
        } else if (state == 8) {
            int _randMove = Random.Range(0, 3);

            ContObj.I.move_walk_to_pos_stop(inGameObj);
            if (_randMove == 0 || _randMove == 1) {
                isChanneling = true;
                StartCoroutine(ChannelingEffect(_randMove));
            } else {
                ContObj.I.use_skill_active(inGameObj, "luca-teleport");
                state = 9;
            }
        } else if (state == 9) {
            ContObj.I.move_walk_to_pos(inGameObj, _pPos);
            ContObj.I.change_facing(inGameObj, ((gameObject.transform.position.x > _pPos.x) ? "left" : "right"));

            stateTime = 0;
            state = 0;
        }
    }

    private IEnumerator ChannelingEffect(int move) {
        // Enable ObjGlitter component
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = true;
        }

        float elapsedTime = 0f;

        while (elapsedTime < channelingTime) {
            // Create effect every effectInterval
            ContEffect.I.create_effect("explosion2", gameObject.transform.position);

            yield return new WaitForSeconds(effectInterval);
            elapsedTime += effectInterval;
        }

        // Perform the skill after channeling
        if (move == 0) {
            ContObj.I.use_skill_active(inGameObj, "luca-flame-wave");
        } else if (move == 1) {
            ContObj.I.use_skill_active(inGameObj, "luca-spread");
        }

        // Disable ObjGlitter component
        if (objGlitterComponent != null) {
            objGlitterComponent.enabled = false;
        }

        isChanneling = false;
        state = 9;
    }
}
