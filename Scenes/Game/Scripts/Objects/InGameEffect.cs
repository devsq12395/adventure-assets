using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class InGameEffect : MonoBehaviour
{
    [Header("------ MODE ------")]
    public string mode; 
    // 'anim' - kill on anim end
    // 'timed' - kill after time
    // 'doodad' - stays forever
    [Header("List of Modes:")]
    [Header("anim - kill on anim end, add the InGameAnimEnd on the animation. Check Explosion01 on how to.")]
    [Header("timed - kill after time")]
    [Header("doodad - stays forever")]
    [Header("collectible - kill on collect")]

    public float timeLimit, zPos;

    private Renderer renderer;

    [Header("Add tween here. Set dur and it's added automatically.")]
    public float tween_popOut_dur;

    private bool animEndCallbackAdded = false;

    public Vector3 curPos; // Changed to Vector3 to store z position as well

    // Jump fields
    public float jumpHeight, jumpTargetHeight, jumpDuration, jumpStartTime;
    public bool isJumping;

    // Shadow fields
    private GameObject shadow;
    private SpriteRenderer shadowRenderer;
    private Vector2 shadowOffset;
    private Color shadowColor = new Color(0, 0, 0, 0.4f);
    
    void Start() {
        renderer = GetComponent<Renderer>();

        if (tween_popOut_dur > 0) {
            add_tween_pop_out(tween_popOut_dur);
        }

        // Initialize shadow
        Transform shadowTransform = transform.Find("shadow");
        if (shadowTransform != null) {
            shadow = shadowTransform.gameObject;
            shadowRenderer = shadow.GetComponent<SpriteRenderer>();
            if (shadowRenderer != null) {
                shadowRenderer.color = shadowColor;
                shadowOffset = shadow.transform.localPosition;
            }
        }

        curPos = gameObject.transform.position; // Initialize curPos here
    }

    void Update() {
        if (mode == "time"){
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 0) {
                Destroy (gameObject);
            }
        }

        if (mode == "collectible" && !isJumping) {
            InGameObject player = ContPlayer.I.player;
            float distanceToPlayer = Vector2.Distance(curPos, (Vector2)player.transform.position);
            float collectionDistance = 1.5f;

            if (distanceToPlayer <= collectionDistance) {
                ColTrig colTrig = GetComponent<ColTrig>();
                if (colTrig != null) {
                    colTrig.on_hit_collectible (player);
                }
            }
        }

        update_render();
        forced_move_update();

        UpdateShadowPositionAndScale();
        if (isJumping) {
            UpdateJump();
        }
    }

    public void destroy_game_object () {
        // Used by Unity animator
        Destroy (gameObject);
    }

    private void update_render() {
        bool _isActive = Vector2.Distance(transform.position, ContPlayer.I.player.transform.position) <= 35f;

        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null) {
            renderer.enabled = _isActive;
        }

        Vector3 _pos = curPos;
        float normalizedY = (_pos.y - 0.1f + ContMap.I.details.size.y) / (2 * ContMap.I.details.size.y);
        zPos = Mathf.Lerp(-9, -1, normalizedY);
        _pos.z = zPos;
        gameObject.transform.position = new Vector3(
            _pos.x, 
            _pos.y + jumpHeight, 
            _pos.z
        );
    }

    public void forced_move_update () {
        var forcedMoves = gameObject.GetComponents<ForcedMoveEffect>();
        foreach (var forcedMove in forcedMoves) {
            forcedMove.update_pos();
            if (forcedMove.elapsedTime >= forcedMove.duration && forcedMove.duration > 0) {
                Destroy(forcedMove);
            }
        }
    }

    public void add_tween_pop_out (float _dur) {
        Sequence _seq = DOTween.Sequence();
        _seq.Append(transform.DOScale(Vector3.one * 1.5f, _dur));
        _seq.Join(renderer.material.DOFade(0f, _dur));
        _seq.AppendCallback(destroy_game_object);
        _seq.Play();
    }

    // Jump
    public void SetJump(float height, float duration) { Debug.Log ("SetJump");
        if (!isJumping) {
            jumpTargetHeight = height;
            jumpDuration = duration;
            jumpStartTime = Time.time;
            isJumping = true;
        }
    }

    public void UpdateJump() {
        float elapsedTime = Time.time - jumpStartTime;
        float halfDuration = jumpDuration / 2;

        if (elapsedTime < jumpDuration) {
            if (elapsedTime < halfDuration) { // Ascending phase
                float progress = elapsedTime / halfDuration;
                jumpHeight = Mathf.Lerp(0, jumpTargetHeight, progress);
            } else { // Descending phase
                float fallElapsedTime = elapsedTime - halfDuration;
                float fallProgress = fallElapsedTime / halfDuration;
                jumpHeight = Mathf.Lerp(jumpTargetHeight, 0, fallProgress);
            }
        } else { // End of jump
            jumpHeight = 0;
            isJumping = false;
        }
    }

    public void UpdateShadowPositionAndScale() {
        if (shadow != null) {
            shadow.transform.localPosition = new Vector2 (
                shadowOffset.x,
                shadowOffset.y - jumpHeight
            );
        }
    }
}
