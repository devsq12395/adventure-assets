using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class JsonReading : MonoBehaviour {
    public static JsonReading I;

    private Dictionary<string, string> jsonStrings;

    public void Awake () {
        I = this;
        List<string> _jsonNames = new List<string>(){
            "items", "missions"
        };
        jsonStrings = new Dictionary<string, string>();

        string _path, _jsonString;
        foreach (string _jsonName in _jsonNames) {
            _path = $"{Application.dataPath}/json-db/{_jsonName}.json";
            if (File.Exists(_path)) {
                _jsonString = File.ReadAllText(_path); Debug.Log (_jsonString);
                jsonStrings.Add (_jsonName, _jsonString);
            } else {
                Debug.LogError ($"JSON file not found at: {_path}");
            }
        }
    }

    public string read (string _jsonName, string _key) {
        if (jsonStrings.ContainsKey(_jsonName)) {
            // Assuming _key is in the format "status.chars.tommy.level"
            string _jsonString = jsonStrings[_jsonName];
            JSONNode _node = JSON.Parse(_jsonString);

            string[] _keys = _key.Split('.');

            JSONNode _currentNode = _node;
            foreach (string _subKey in _keys) {
                if (_currentNode[_subKey] == null) {
                    Debug.LogError($"Key '{_key}' not found in JSON '{_jsonName}'.");
                    return null;
                }
                _currentNode = _currentNode[_subKey];
            }

            if (_currentNode.IsArray) {
                JSONArray _dataArray = _currentNode.AsArray;
                List<string> _resultList = new List<string>();

                foreach (JSONNode _value in _dataArray) {
                    _resultList.Add(_value.Value);
                }

                return string.Join(",", _resultList);
            } else {
                return _currentNode.Value;
            }
        } else {
            Debug.LogError ($"JSON does not exist: {_jsonName}");
            return null;
        }
    }
}