using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JsonSaving : MonoBehaviour {
    public static JsonSaving I;

    public string login;
    public JSONObject playData;

    public bool OVERWRITE;

    public void Awake(){
        I = this;

        OVERWRITE = false;
        if (OVERWRITE) Debug.Log("DEBUG WARNING: Overwrite is true, will overwrite all data.");

        // TEST
        PlayerPrefs.SetString("login", "tommy");

        login = PlayerPrefs.GetString("login");

        load_json();
    }

    /*
        SAVED VALUES JSON SCRIPTS
    */

    public void gain_gold(int _inc) {
        
    }

    /*
        JSON INTERACTION SCRIPTS
    */

    public void load_json() {
        if (PlayerPrefs.GetString("v.1") == "" || OVERWRITE) {
            first_load();
        } else {
            // Load existing data
            string jsonString = PlayerPrefs.GetString("v.1");
            playData = JSON.Parse(jsonString).AsObject;
        }
    }

    public void first_load() {
        
    }
    
    public void save(string _key, string _value) {
        if (playData == null) {
            playData = new JSONObject();
        }

        playData[_key] = _value;
        PlayerPrefs.SetString("v.1", playData.ToString());
    }

    public void add_array_val(string _key, string _value) {
        if (playData == null) {
            playData = new JSONObject();
        }

        if (!playData.HasKey(_key)) {
            playData[_key] = new JSONArray();
        }

        playData[_key].AsArray.Add(_value);
        PlayerPrefs.SetString("v.1", playData.ToString());
    }

    public string load(string _key) {
        if (playData == null) {
            string jsonString = PlayerPrefs.GetString("v.1");
            if (!string.IsNullOrEmpty(jsonString)) {
                playData = JSON.Parse(jsonString).AsObject;
            } else {
                playData = new JSONObject();
            }
        }

        if (playData.HasKey(_key)) {
            return playData[_key];
        } else {
            return "0";
        }
    }
}
