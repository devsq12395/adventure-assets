using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class JsonReading : MonoBehaviour
{
    public static JsonReading I;

    private Dictionary<string, string> jsonStrings;

    public void Awake()
    {
        I = this;
        List<string> _jsonNames = new List<string>(){
            "items", "missions", "dialogs", "strings", "chars"
        };
        jsonStrings = new Dictionary<string, string>();

        string _jsonString;
        foreach (string _jsonName in _jsonNames)
        {
            string _path = $"{Application.streamingAssetsPath}/{_jsonName}.json";
            StartCoroutine(LoadJsonFile(_path, _jsonName));
        }
    }

    IEnumerator LoadJsonFile(string path, string jsonName)
    {
        using (WWW www = new WWW(path))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                jsonStrings.Add(jsonName, www.text);
            }
            else
            {
                Debug.LogError($"Failed to load JSON file '{jsonName}' from path: {path}");
            }
        }
    }

    /*
        When reading an array, values will be joined by ",".
        This will not be able to read objects.
    */
    public string read(string _jsonName, string _key)
    {
        if (jsonStrings.ContainsKey(_jsonName))
        {
            // Assuming _key is in the format "status.chars.tommy.level"
            string _jsonString = jsonStrings[_jsonName];
            JSONNode _node = JSON.Parse(_jsonString);

            string[] _keys = _key.Split('.');

            JSONNode _currentNode = _node;
            foreach (string _subKey in _keys)
            {
                if (_currentNode[_subKey] == null)
                {
                    Debug.LogError($"Key '{_key}' not found in JSON '{_jsonName}'.");
                    return null;
                }
                _currentNode = _currentNode[_subKey];
            }

            if (_currentNode.IsArray)
            {
                JSONArray _dataArray = _currentNode.AsArray;
                List<string> _resultList = new List<string>();

                foreach (JSONNode _value in _dataArray)
                {
                    _resultList.Add(_value.Value);
                }

                return string.Join(",", _resultList);
            }
            else
            {
                return _currentNode.Value;
            }
        }
        else
        {
            Debug.LogError($"JSON does not exist: {_jsonName}");
            return null;
        }
    }

    public string get_str(string _key)
    {
        return read("strings", $"strings.{_key}");
    }
}
