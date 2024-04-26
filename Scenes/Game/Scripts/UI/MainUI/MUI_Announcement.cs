using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MUI_Announcement : MonoBehaviour
{
    public static MUI_Announcement I;
    public void Awake() { I = this; }

    public GameObject go;
    public TextMeshProUGUI text;

    public float dur;

    public bool isShow;

    void Update (){
        if (!isShow) return;

        dur -= Time.deltaTime;
        if (dur <= 0){
            isShow = false;
            go.transform.DOScale(new Vector3 (0.8f, 0.8f, 0.8f), 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>  go.SetActive(false));
        }
    }

    public void show (string _announce) {
        go.SetActive (true);
        isShow = true;

        text.text = _announce;
        dur = 2.5f;

        go.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        go.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

}