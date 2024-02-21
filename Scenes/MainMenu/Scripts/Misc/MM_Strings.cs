using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class MM_Strings : MonoBehaviour {
    private string jsonStringPath;
    private string jsonString;
    
    public static MM_Strings I;
    public void Awake () {
        I = this;
        jsonStringPath = $"{Application.dataPath}/json-db/strings.json";

        if (File.Exists(jsonStringPath)) {
            jsonString = File.ReadAllText(jsonStringPath);
        }
        else {
            Debug.LogError("JSON file not found at: " + jsonStringPath);
        }
    }

    public string get_str (string key) {
        JSONNode jsonObject = JSON.Parse(jsonString);
        JSONNode stringsObject = jsonObject["strings"];

        if (stringsObject != null) {
            if (stringsObject[key] != null) {
                return stringsObject[key];
            }
            else {
                Debug.LogError("Key not found: " + key);
            }
        }
        else {
            Debug.LogError("No 'strings' object found in JSON.");
        }

        return null;
    }
}