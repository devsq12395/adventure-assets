using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContEffect : MonoBehaviour {

    public static ContEffect I;
	public void Awake(){ I = this; }

    public int curID = 0;

    void Start() {
        
    }

    void Update() {
        
    }

    public GameObject create_effect (string _name, Vector2 _pos) {
        GameObject _obj = DB_Objects.I.get_game_obj (_name);
        InGameObject _comp = _obj.GetComponent <InGameObject>();

        _obj.transform.position = new Vector3 (_pos.x, _pos.y, -9);

        return _obj;
    }
}
