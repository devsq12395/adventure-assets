using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MUI_Tutorial : MonoBehaviour
{
    public static MUI_Tutorial I;
    public void Awake() { I = this; }

    public GameObject go;
    public TextMeshProUGUI uiTxt;
    public Image imgKey;
    public Sprite imgWASD, imgShift, imgClick, imgSkill, imgNumKey, none;

    public bool isShow; 
    public string state;

    private string nextTutToShow;
    private float endDur;
    private List<string> checkers;

    public void setup (){
        checkers = new List<string>();
    }

    void Update (){
        update_state_events();
    }

    public void show (string _toShow) {
        if (isShow){
            nextTutToShow = _toShow;
            hide ();
        } else {
            go.SetActive (true);
            isShow = true;

            state = _toShow;
            setup_ui ();

            go.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
            go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }
    }

    public void hide (){
        state = "";
        go.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutBack).OnComplete(()=>{
            isShow = false;
            if (nextTutToShow != "")  {
                show(nextTutToShow);
                nextTutToShow = ""; // Reset after showing
            }
        });
    }

    private void setup_ui (){
        switch (state) {
            case "move":
                uiTxt.text = "Use WASD keys to move. Try moving on all 4 directions.";
                imgKey.sprite = imgWASD;
                break;
            case "shift":
                uiTxt.text = "Press Shift to dash and dodge missiles. This consumes stamina.";
                imgKey.sprite = imgShift;
                break;
            case "click":
                uiTxt.text = "To attack, aim with the mouse then press click.";
                imgKey.sprite = imgClick;
                break;
            case "skill":
                uiTxt.text = "Press E to use your skill. Your skill has a cooldown of a few seconds.";
                imgKey.sprite = imgSkill;
                break;
            case "switch-char":
                uiTxt.text = "Switch character with keys 1,2,3,4. ";
                imgKey.sprite = imgNumKey;
                break;
            case "end":
                PlayerPrefs.SetInt("combat-tut-state", 1);
                uiTxt.text = "You are now ready to fight! Good luck and have fun!";
                imgKey.sprite = none;
                break;
        }
    }

    private void update_state_events (){
        switch (state) {
            case "move":
                if (Input.GetKeyDown(KeyCode.W) && !checkers.Contains("up")) {
                    checkers.Add("up");
                }
                if (Input.GetKeyDown(KeyCode.S) && !checkers.Contains("down")) {
                    checkers.Add("down");
                }
                if (Input.GetKeyDown(KeyCode.A) && !checkers.Contains("left")) {
                    checkers.Add("left");
                }
                if (Input.GetKeyDown(KeyCode.D) && !checkers.Contains("right")) {
                    checkers.Add("right");
                }

                if (checkers.Contains("up") && checkers.Contains("down") && checkers.Contains("left") && checkers.Contains("right")) {
                    show("shift");
                }
                break;
            case "shift":
                if (Input.GetKeyDown(KeyCode.LeftShift)) {
                    show ("click");
                }
                break;
            case "click":
                if (Input.GetMouseButtonDown(0)) {
                    show ("skill");
                }
                break;
            case "skill":
                if (Input.GetKeyDown(KeyCode.E)) {
                    show ("switch-char");
                }
                break;
            case "switch-char":
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) ||
                    Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4)) {
                    show("end");
                }
                break;
            case "end":
                endDur += Time.deltaTime;
                if (endDur >= 5f) hide ();
                break;
        }
    }

}
