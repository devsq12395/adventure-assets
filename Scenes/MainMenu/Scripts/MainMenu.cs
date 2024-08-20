using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class MainMenu : MonoBehaviour {
    public static MainMenu I;
	public void Awake(){ I = this; }

    public GameObject curtainGo, headerGo;
    public RectTransform curtainRect;
    private string curtainState;
    public float targetX;

    public string login;

    public TextMeshProUGUI headerTxt_Gold;

    void Start() {
        login = "tommy";

        // Setup all scripts
        MM_Profile.I.setup ();
        MM_Area.I.setup ();
        MM_Party.I.setup ();

        MM_ItemCheck.I.setup ();
        MM_ChangeParty.I.setup ();
        MM_Char.I.setup ();
        MM_Mission.I.setup ();
        MM_Craft.I.setup ();

        MM_Map.I.setup ();
        MM_Map.I.show (ZPlayerPrefs.GetString("main-menu-map"));

        MM_Inv2.I.setup ();

        update_header ();

        move_curtain("menuStart", 0, curtainRect.anchoredPosition.y);

        SoundHandler.I.play_bgm ("menu");
    }

    void Update() {
        update_show_header ();
    }

    public void show_popup (string _popup){
        switch (_popup) {
            case "profile":             MM_Profile.I.toggle_show (true); break;
            case "area":                MM_Area.I.toggle_show (true); break;
            case "party":               MM_Party.I.toggle_show (true); break;
        }
    }

    public void update_show_header (){
        bool _isShow = 
            !FindObjectOfType<MiniDialog>() &&
            !MM_Party.I.isShow &&
            !MM_BuyOrSell.I.go.activeSelf &&
            !MM_Mission.I.go.activeSelf
        ;
        headerGo.SetActive (_isShow);
    }

    public void update_header (){
        headerTxt_Gold.text = $"Gold: {ZPlayerPrefs.GetInt("gold")}";
    }

    public void update_gold (int _inc){
        int gold = SaveHandler.I.gain_gold (_inc);

        update_header ();
        MM_Inv2.I.update_gold (gold);
    }

    public void move_curtain(string _state, float _curtainPosX = 0, float _curtainPosY = 0) {
        curtainGo.SetActive(true);

        Vector2 _curtainPos;
        switch (_state){ 
            case "menuStart":
                _curtainPos = new Vector2(0, curtainRect.anchoredPosition.y);
                targetX = Screen.width + curtainRect.rect.width / 2;
                break;
            default:
                _curtainPos = new Vector2(-Screen.width - curtainRect.rect.width / 2, curtainRect.anchoredPosition.y);
                targetX = 0;
                break;
        }

        Debug.Log (_curtainPos);
        float speed = 1f;
        curtainState = _state;
        curtainRect.anchoredPosition = _curtainPos;

        curtainRect.DOAnchorPosX(targetX, speed).OnComplete(() => curtain_move_end());
    }

    private void curtain_move_end() {
        switch (curtainState) {
            case "menuStart":
                curtainGo.SetActive(false);
                main_menu_start_callback ();
                break;
            case "toGame":
                MasterScene.I.change_main_scene ("Game");
                break;
        }
    }

    public void main_menu_start_callback (){ 
        string _callback = ZPlayerPrefs.GetString("main-menu-start-callback");

        switch (_callback) {
            case "show-intro-1": MMCont_Dialog.I.show_intro_1 (); break;
            case "finished-mission-vic-1":
            
                break;
        } 
    }
}
