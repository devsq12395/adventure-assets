using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Upgrade : MonoBehaviour {

    public static DB_Upgrade I;
	public void Awake(){ I = this; }

    public struct upgrade {
        public string name, desc;

        public upgrade (string _name){
            name = _name;
            desc = "";
        }
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public upgrade get_upgrade (){
        upgrade _new = new upgrade ("");
        int NUM_OF_UPGS = 5,
            rand = Random.Range (0, NUM_OF_UPGS);

        switch (rand) {
            case 0:
                _new.name = "test1";
                _new.desc = "asd";
                break;
            case 1:
                _new.name = "test2";
                _new.desc = "asd";
                break;
            case 2:
                _new.name = "test3";
                _new.desc = "asd";
                break;
            case 3:
                _new.name = "test4";
                _new.desc = "asd";
                break;

            
            default:
                _new.name = "test5";
                _new.desc = "asd";
                break;
        }

        return _new;
    }
}
