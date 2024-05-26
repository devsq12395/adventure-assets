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

    public string get_str(string _key) {
        switch (_key) {
            case "date":
                return "Date: ";
            case "jan-2225":
                return "Jan 2225";

            case "name":
                return "Name: ";
            case "lvl":
                return "Lvl";

            case "hp":
                return "HP";
            case "mp":
                return "MP";
            case "attack":
                return "Attack";
            case "skill":
                return "Skill";
            case "speed":
                return "Speed";
            case "armor":
                return "Armor";
            case "crit-dam":
                return "Crit Dam";
            case "crit-rate":
                return "Crit Rate";

            case "gold":
                return "Gold";

            case "requires":
                return "Requires";

            // UI-main-menu strings
            case "UI-main-menu.equip":
                return "Equip";
            case "UI-main-menu.buy":
                return "Buy";
            case "UI-main-menu.sell":
                return "Sell";
            case "UI-main-menu.check-equipped":
                return "Change Equipped";
            case "UI-main-menu.reputation":
                return "Reputation";
            case "UI-main-menu.stranger":
                return "Stranger";

            // UI-in-game strings
            case "UI-in-game.mission-failed":
                return "MISSION FAILED!";
            case "UI-in-game.all-chars-dead":
                return "Your team is wiped out!";
            case "UI-in-game.mission-success":
                return "MISSION SUCCESS!";
            case "UI-in-game.all-enemies-dead":
                return "All enemies are beaten!";
            case "UI-in-game.rewards-gold":
                return "Gold Gained: ";
            case "UI-in-game.rewards-item":
                return "You Found: ";

            // Character names
            case "char-names.kitsune-boss":
                return "Kitsune Boss";
                
            case "tommy-name": return "Tommy"
            case "tommy-name-full": return "Tommy Carter";

            case "kazuma-name": return "Kazuma";
            case "kazuma-name-full": return "Kazuma";

            case "anastasia-name": return "Anastasia";
            case "anastasia-name-full": return "Anastasia";

            case "sylphine-name": return "Sylphine";
            case "sylphine-name-full": return "Sylphine";

            case "miguel-name": return "Miguel";
            case "miguel-name-full": return "Miguel";

            case "anthony-name": return "Anthony";
            case "anthony-name-full": return "Anthony";

            case "new-haven-name": return "New Haven";
            case "new-haven-welcome": return "Welcome to New Haven!";

            default: return "";
        }
    }
}
