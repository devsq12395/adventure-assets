using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.IO;

public class JsonSaving : MonoBehaviour {
    public static JsonSaving I;

    public string login;
    public JSONObject playData;

    public bool OVERWRITE;

    private string saveFilePath;

    public void Awake(){
        I = this;

        OVERWRITE = false;
        if (OVERWRITE) Debug.Log("DEBUG WARNING: Overwrite is true, will overwrite all data.");

        // TEST
        PlayerPrefs.SetString("login", "tommy");

        login = PlayerPrefs.GetString("login");

        // Define path for the saved data file
        saveFilePath = $"{Application.persistentDataPath}/playerData.json";

        first_load ();
        StartCoroutine(load_json());
    }

    /*
        SAVED VALUES JSON SCRIPTS
    */

    public void gain_gold(int _inc) {
        // Your implementation
    }

    /*
        JSON INTERACTION SCRIPTS
    */

    public IEnumerator load_json() {
        if (!File.Exists(saveFilePath) || OVERWRITE) {
            first_load();
        } else {
            using (UnityWebRequest webRequest = UnityWebRequest.Get("file://" + saveFilePath))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.ConnectionError || 
                    webRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError($"Failed to load JSON file from path: {saveFilePath}. Error: {webRequest.error}");
                }
                else
                {
                    playData = JSON.Parse(webRequest.downloadHandler.text).AsObject;
                }
            }
        }
    }

    public void first_load() {
        // Initialize playData with default values
        Debug.Log ("first load...");
        playData = new JSONObject();
        playData["gold"] = 0;
        playData["items"] = new JSONArray();
        // Add more default initializations if needed

        // Save the initial data
        save_json();
    }
    
    public void save(string _key, string _value) {
        if (playData == null) {
            playData = new JSONObject();
        }

        playData[_key] = _value;
        save_json();
    }

    public void add_array_val(string _key, string _value) {
        if (playData == null) {
            playData = new JSONObject();
        }

        if (!playData.HasKey(_key)) {
            playData[_key] = new JSONArray();
        }

        playData[_key].AsArray.Add(_value);
        save_json();
    }

    public string load(string _key) {
        if (playData == null) {
            StartCoroutine(load_json());
        }

        if (playData.HasKey(_key)) {
            return playData[_key];
        } else {
            return "0";
        }
    }

    private void save_json() {
        File.WriteAllText(saveFilePath, playData.ToString());
        Debug.Log ("Json saving...");
    }
}
