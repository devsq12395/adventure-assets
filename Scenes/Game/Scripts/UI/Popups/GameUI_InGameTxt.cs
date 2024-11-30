using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameUI_InGameTxt : MonoBehaviour {

    public static GameUI_InGameTxt I;
    public void Awake() { I = this; }

    public GameObject goCanvas; // Canvas set to World Space

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

    public GameObject create_ingame_txt(string _txt, Vector2 _pos, float _dur, Color _color = default(Color)) {
        // Ensure position is valid
        _pos = check_if_pos_used(_pos);

        // Spawn the damage text game object
        GameObject _go = DB_Objects.I.get_game_obj("damTxt");

        // Make the text a child of the canvas (which is set to World Space)
        _go.transform.SetParent(goCanvas.transform, false);

        // Set the world position of the text
        _go.transform.position = new Vector2 (_pos.x, _pos.y + 2);

        // Adjust scale for readability in World Space
        Canvas _canvas = _go.transform.Find("Canvas").GetComponent<Canvas>();
        _canvas.transform.localScale = new Vector3(22f, 22f, 1f); // Adjust scale if needed for visibility

        // Get the TextMeshProUGUI component
        TextMeshProUGUI _newTxtUI = _canvas.transform.Find("DamTxt").GetComponent<TextMeshProUGUI>();

        // Customize text appearance (color, size, etc.)
        _newTxtUI.color = _color;
        _newTxtUI.fontSize = 1; // Font size appropriate for World Space
        _newTxtUI.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        // Enable and set the outline properties
        _newTxtUI.fontMaterial.EnableKeyword("OUTLINE_ON");
        _newTxtUI.outlineWidth = 0.2f;
        _newTxtUI.gameObject.SetActive(false);
        _newTxtUI.fontSharedMaterial.SetColor("_OutlineColor", Color.black);
        _newTxtUI.gameObject.SetActive(true);

        // Animate the text (only for non-critical texts)
        if (_txt != "Critical!") {
            _newTxtUI.transform.localScale = Vector3.zero;
            _newTxtUI.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).OnComplete(() => {
                _newTxtUI.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 2f);
            });
        }

        _newTxtUI.DOFade(1f, _dur - _dur * 0.75f).OnComplete(() => {
            _newTxtUI.DOFade(0f, _dur - _dur * 0.25f);
        });

        // Animate movement upwards in world space
        _newTxtUI.rectTransform.DOAnchorPosY(_newTxtUI.rectTransform.anchoredPosition.y + 4f, _dur).SetUpdate(true);

        // Add new InGameTxt to the list
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
        for (int i = txtList.Count - 1; i >= 0; i--) {
            InGameTxt _item = txtList[i];
            _item.dur -= Time.deltaTime;

            // Move text up over time in world space
            Vector3 newPos = _item.go.transform.position;
            newPos.y += 0.03f; // Adjust the speed of movement here
            _item.go.transform.position = newPos;

            // Check if the text has expired
            if (_item.dur <= 0) {
                Destroy(_item.go);
                txtList.RemoveAt(i);
            } else {
                txtList[i] = _item; // Update the list with the modified text
            }
        }
    }
}
