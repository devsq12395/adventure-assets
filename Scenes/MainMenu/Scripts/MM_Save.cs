using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class MM_Save : MonoBehaviour {
    public static MM_Save I;
	public void Awake(){ I = this; }

    private string FILEPATH;

    public string login;
    public Dictionary<string, object> playData;

    public void setup (){
        // TEST
        PlayerPrefs.SetString ("login", "tommy");

        login = PlayerPrefs.GetString ("login");

        FILEPATH = $"{Application.persistentDataPath}/playdata-{login}.json";
        load_json ();
    }

    private void load_json (){
        if (File.Exists(FILEPATH)) {
            string json = File.ReadAllText(FILEPATH);
            playData = JsonUtility.FromJson<Dictionary<string, object>>(json);
        } else {
            first_load ();
        }
    }

    private void save_json (){
        string json = JsonUtility.ToJson(playData);
        File.WriteAllText(FILEPATH, json);
    }

    private void first_load (){
        string _start = $"{Application.dataPath}/starting.json";
        string _json = File.ReadAllText(_start);
        playData = JsonUtility.FromJson<Dictionary<string, object>>(_json);

        save_json();
    }

    public void save (string _key, string _value){
        // Assuming _key is in the format "status.chars.tommy.level"
        string[] keys = _key.Split('.');
        Dictionary<string, object> nestedData = playData;

        for (int i = 0; i < keys.Length - 1; i++) {
            if (nestedData.ContainsKey(keys[i]) && nestedData[keys[i]] is Dictionary<string, object>) {
                nestedData = (Dictionary<string, object>)nestedData[keys[i]];
            } else {
                nestedData[keys[i]] = new Dictionary<string, object>();
                nestedData = (Dictionary<string, object>)nestedData[keys[i]];
            }
        }

        nestedData[keys[keys.Length - 1]] = _value;

        save_json();
    }

    public string load (string _key, string _value){
        // Assuming _key is in the format "status.chars.tommy.level"
        string[] keys = _key.Split('.');
        Dictionary<string, object> nestedData = playData;

        for (int i = 0; i < keys.Length - 1; i++) {
            if (nestedData.ContainsKey(keys[i]) && nestedData[keys[i]] is Dictionary<string, object>) {
                nestedData = (Dictionary<string, object>)nestedData[keys[i]];
            } else {
                return null; 
            }
        }

        if (nestedData.ContainsKey(keys[keys.Length - 1])) {
            return nestedData[keys[keys.Length - 1]].ToString();
        } else {
            return null;
        }
    }
}
