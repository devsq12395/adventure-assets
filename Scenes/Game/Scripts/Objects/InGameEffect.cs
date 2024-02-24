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

    public float timeLimit;

    private Renderer renderer;

    [Header("Add tween here. Set dur and it's added automatically.")]
    public float tween_popOut_dur;

    private bool animEndCallbackAdded = false;
    
    void Start() {
        renderer = GetComponent <Renderer> ();

        if (tween_popOut_dur > 0) {
            add_tween_pop_out (tween_popOut_dur);
        }
    }

    void Update() {
        if (mode == "time"){
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 0) {
                Destroy (gameObject);
            }
        }

        update_render ();
    }

    public void destroy_game_object () {
        // Used by Unity animator
        Destroy (gameObject);
    }

    private void update_render (){
        if (Vector2.Distance(transform.position, Camera.main.transform.position) > GameConstants.RENDER_DIST) {
            renderer.enabled = false;
        } else {
            renderer.enabled = true;
        }
    }

    public void add_tween_pop_out (float _dur){
        Sequence _seq = DOTween.Sequence();
        _seq.Append(transform.DOScale(Vector3.one * 1.5f, _dur));
        _seq.Join(renderer.material.DOFade(0f, _dur));
        _seq.AppendCallback(destroy_game_object);
        _seq.Play();
    }
}
