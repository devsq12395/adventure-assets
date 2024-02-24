using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameUI_InGameTxt: MonoBehaviour{
    
    public static GameUI_InGameTxt I;
	public void Awake(){ I = this; }
    
    public struct InGameTxt {
        public GameObject go;
        public TextMeshProUGUI txtUI;
        public float dur;
        
        public InGameTxt (GameObject _go, TextMeshProUGUI _txtUI, string _txt, float _dur){
            go = _go;
            txtUI = _txtUI;
            txtUI.text = _txt;
            dur = _dur;
        }
    }
    
    public List<InGameTxt> txtList;
    
    public bool isReady;
    
    private Dictionary<Vector2, int> posUsed;
    
    public void setup (){
        txtList = new List<InGameTxt> ();
        posUsed = new Dictionary<Vector2, int> ();
        
        isReady = true;
    }
    
    void Update (){
        if (!isReady) return;
        
        all_txt_dur ();
        posUsed.Clear ();
    }
    
    public GameObject create_ingame_txt (string _txt, Vector2 _pos, float _dur){
        _pos = check_if_pos_used (_pos);
        
        GameObject _go = DB_Objects.I.get_game_obj ("damTxt");
        _go.transform.position = _pos;
        
        Canvas _canvas = _go.transform.Find ("Canvas").GetComponent <Canvas> ();
        _canvas.worldCamera = Camera.main;
        _canvas.sortingOrder = 1;
        
        TextMeshProUGUI _newTxtUI = _canvas.transform.Find ("DamTxt").GetComponent <TextMeshProUGUI> ();
        
        _newTxtUI.color = new Color(_newTxtUI.color.r, _newTxtUI.color.g, _newTxtUI.color.b, 1f);
        _newTxtUI.rectTransform.anchoredPosition = new Vector2(_newTxtUI.rectTransform.anchoredPosition.x, _newTxtUI.rectTransform.anchoredPosition.y);

        _newTxtUI.DOFade(0f, _dur);
        _newTxtUI.rectTransform.DOAnchorPosY(_newTxtUI.rectTransform.anchoredPosition.y + 2f, _dur);
        
        InGameTxt _new = new InGameTxt (_go, _newTxtUI, _txt, _dur);
        txtList.Add (_new);
        
        return _go;
    }
    
    private Vector2 check_if_pos_used (Vector2 _pos){
        if (posUsed.ContainsKey (_pos)) {
            posUsed [_pos] += 1;
            _pos.y += 0.2f * posUsed [_pos];
        } else {
            posUsed.Add (_pos, 1);
        }
        
        return _pos;
    }
    
    public void all_txt_dur (){
        InGameTxt _item;
        for (int i = txtList.Count - 1; i >= 0; i--) {
            _item = txtList [i];
            _item.dur -= Time.deltaTime;
            
            if (_item.dur <= 0) {
                Destroy (txtList [i].go);
                txtList.RemoveAt (i);
            } else {
                txtList [i] = _item;
            }
        }
    }
}