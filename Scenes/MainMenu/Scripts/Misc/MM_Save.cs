using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class MM_Save : MonoBehaviour {
    public static MM_Save I;
    public void Awake(){ I = this; }

    private string JSON_SAVE, JSON_START;

    public string login;
    public JSONObject playData;

    public void setup(){
        // TEST
        PlayerPrefs.SetString("login", "tommy");

        login = PlayerPrefs.GetString("login");

        JSON_SAVE = $"{Application.persistentDataPath}/playdata-{login}.json";
        JSON_START = $"{Application.dataPath}/starting-{login}.json";

        load_json();
    }

    private void load_json(){
        if (File.Exists(JSON_SAVE)){
            string _json = File.ReadAllText(JSON_SAVE);
            playData = JSON.Parse(_json).AsObject;

            // Check JSON_START if something is missing from JSON_SAVE
            string _jsonStart = File.ReadAllText(JSON_START);
            JSONObject _start = JSON.Parse(_jsonStart).AsObject;
            merge_dictionary(playData, _start);

            save_json();
        }
        else{
            first_load();
        }
    }

    private void save_json(){
        string json = playData.ToString();
        File.WriteAllText(JSON_SAVE, json);
    }

    private void first_load(){
        string _json = File.ReadAllText(JSON_START);
        playData = JSON.Parse(_json).AsObject;

        save_json();
    }

    public void save(string _key, string _value){
        // Assuming _key is in the format "status.chars.tommy.level"
        string[] keys = _key.Split('.');
        JSONObject nestedData = playData;

        for (int i = 0; i < keys.Length - 1; i++){
            if (!nestedData.HasKey(keys[i])){
                nestedData[keys[i]] = new JSONObject();
            }
            nestedData = nestedData[keys[i]].AsObject;
        }

        nestedData[keys[keys.Length - 1]] = _value;

        save_json();
    }

    public string load(string _key){
        // Assuming _key is in the format "status.chars.tommy.level"
        string[] keys = _key.Split('.');
        JSONObject nestedData = playData;

        for (int i = 0; i < keys.Length - 1; i++){
            if (nestedData.HasKey(keys[i])){
                nestedData = nestedData[keys[i]].AsObject;
            }
            else{
                return null;
            }
        }

        return nestedData[keys[keys.Length - 1]];
    }

    private void merge_dictionary(JSONObject target, JSONObject source){
        foreach (var entry in source){
            if (!target.HasKey(entry.Key)){
                target[entry.Key] = entry.Value;
            }
        }
    }
}
