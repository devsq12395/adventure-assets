using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameUI_InGameTxt : MonoBehaviour {

    public static GameUI_InGameTxt I;
    public void Awake() { I = this; }

    public GameObject goCanvas;

    public struct InGameTxt {
        public GameObject go;
        public TextMeshProUGUI txtUI;
        public float dur;
        public Vector2 origPos, curPos;

        public InGameTxt(GameObject _go, TextMeshProUGUI _txtUI, string _txt, float _dur, Vector2 _origPos) {
            go = _go;
            txtUI = _txtUI;
            txtUI.text = _txt;
            dur = _dur;
            origPos = _origPos;
            curPos = _origPos;
        }
    }

    public List<InGameTxt> txtList;

    public bool isReady;

    private Dictionary<Vector2, int> posUsed;

    public void setup() {
        txtList = new List<InGameTxt>();
        posUsed = new Dictionary<Vector2, int>();

        isReady = true;
    }

    void Update() {
        if (!isReady) return;

        update_all_txts();
        posUsed.Clear();
    }

    public GameObject create_ingame_txt(string _txt, Vector2 _pos, float _dur) {
        _pos = check_if_pos_used(_pos);

        GameObject _go = DB_Objects.I.get_game_obj("damTxt");
        _go.transform.position = _pos;
        _go.transform.SetParent(goCanvas.transform);

        Canvas _canvas = _go.transform.Find("Canvas").GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _canvas.sortingOrder = 1;

        TextMeshProUGUI _newTxtUI = _canvas.transform.Find("DamTxt").GetComponent<TextMeshProUGUI>();

        // Set text color to yellow if the text is "Critical!"
        if (_txt == "Critical!") {
            _newTxtUI.color = Color.yellow;
        }

        // Set the text scale to 1.5 times its original size
        _newTxtUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        // Enable and set the outline properties
        _newTxtUI.fontMaterial.EnableKeyword("OUTLINE_ON");
        _newTxtUI.outlineWidth = 0.2f; // Adjust the width as needed
        _newTxtUI.gameObject.SetActive(false);
        _newTxtUI.fontSharedMaterial.SetColor("_OutlineColor", Color.black); // Set outline color to black
        _newTxtUI.gameObject.SetActive(true);

        // Animate the text: scale and position tweens
        if (_txt != "Critical!") {
            _newTxtUI.transform.localScale = Vector3.zero;
            _newTxtUI.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() => {
                _newTxtUI.transform.DOScale(Vector3.zero, 2f);
            });
        }

        _newTxtUI.DOFade(0f, _dur);
        _newTxtUI.rectTransform.DOAnchorPosY(_newTxtUI.rectTransform.anchoredPosition.y + 30f, _dur);

        InGameTxt _new = new InGameTxt(_go, _newTxtUI, _txt, _dur, _pos);
        txtList.Add(_new);

        return _go;
    }

    private Vector2 check_if_pos_used(Vector2 _pos) {
        if (posUsed.ContainsKey(_pos)) {
            posUsed[_pos] += 1;
            _pos.y += 0.2f * posUsed[_pos];
        } else {
            posUsed.Add(_pos, 1);
        }

        return _pos;
    }

    public void update_all_txts() {
        InGameTxt _item;
        for (int i = txtList.Count - 1; i >= 0; i--) {
            _item = txtList[i];
            _item.dur -= Time.deltaTime;

            // Text Position
            _item.curPos = new Vector2(_item.curPos.x, _item.curPos.y + 0.03f);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(txtList[i].curPos.x, txtList[i].curPos.y, 0f));
            Vector2 uiPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                txtList[i].txtUI.rectTransform.parent as RectTransform,
                screenPos,
                Camera.main,
                out uiPos);
            txtList[i].txtUI.rectTransform.anchoredPosition = uiPos;

            // Duration
            if (_item.dur <= 0) {
                Destroy(txtList[i].go);
                txtList.RemoveAt(i);
            } else {
                txtList[i] = _item;
            }
        }
    }
}
