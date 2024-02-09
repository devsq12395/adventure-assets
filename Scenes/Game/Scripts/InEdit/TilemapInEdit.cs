using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TilemapInEdit : MonoBehaviour
{
    void Start (){
        if (!Application.isEditor) return;

        Vector3 _pos = gameObject.transform.position;
        _pos.z = (_pos.y - 100) / 100;
        gameObject.transform.position = _pos;
    }
}