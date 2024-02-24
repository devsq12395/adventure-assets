using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ContCollision : MonoBehaviour {

    public static ContCollision I;
	public void Awake(){ I = this; }

    private struct collList {
        public int id1, id2;
        public float dur;

        public collList (int _id1, int _id2, float _dur) {
            id1 = _id1;
            id2 = _id2;
            dur = _dur;
        }
    }
    private List<collList> allList;

    void Start() {
        allList = new List<collList>();
    }

    void Update() {
        update_coll_dur ();
    }

    public void collision (InGameObject obj1, InGameObject obj2){
        if (
            coll_list_handle_and_chk (obj1, obj2) ||
            !DB_Conditions.I.coll_cond (obj1) ||
            !DB_Conditions.I.coll_cond (obj2) 
        ) return;

        ColTrig     script1 = obj1.GetComponent<ColTrig>(),
                    script2 = obj2.GetComponent<ColTrig>();

        if (script1 == null || script2 == null) return;

        if (obj1.owner == obj2.owner) {
            script1.on_hit_ally (obj2);
            script2.on_hit_ally (obj1);
        } else {
            script1.on_hit_enemy (obj2);
            script2.on_hit_enemy (obj1);
        }
    }
    public bool coll_list_handle_and_chk (InGameObject obj1, InGameObject obj2){
        collList _chkr;
        for (int i = 0; i < allList.Count; i++) {
            _chkr = allList [i];
            if ((_chkr.id1 == obj1.id && _chkr.id2 == obj2.id) || (_chkr.id1 == obj2.id && _chkr.id2 == obj1.id)) {
                return true;
            }
        }
        
        collList _new = new collList (obj1.id, obj2.id, GameConstants.COLL_DUR);
        allList.Add (_new);

        return false;
    }

    private void collision_ally (InGameObject obj1, InGameObject obj2){

    }

    private void collision_enemy (InGameObject obj1, InGameObject obj2){
        
    }

    private void update_coll_dur (){
        collList _coll;
        List<int> toRemove = new List<int> ();

        for (int i = 0; i < allList.Count; i++) {
            _coll = allList [i];

            _coll.dur -= Time.deltaTime;
            allList [i] = _coll;
            if (_coll.dur <= 0) {
                toRemove.Add (i);
            }
        }

        for (int i = toRemove.Count - 1; i >= 0; i--) {
            allList.RemoveAt (toRemove [i]);
        }
    }
}
