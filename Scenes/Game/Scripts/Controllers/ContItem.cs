using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public class ContItem : MonoBehaviour {

    public static ContItem I;
	public void Awake(){ I = this; }

    public List<DB_Items.Item> items;
    private string FILEPATH;

    public void setup (){
        /*FILEPATH = Application.persistentDataPath + "/items.json";
        load_json ();*/
    }

    private void load_json (){
        if (File.Exists(FILEPATH)) {
            string json = File.ReadAllText(FILEPATH);
            items = JsonUtility.FromJson<List<DB_Items.Item>>(json);
        } else {
            save_json ();
        }
    }

    private void save_json (){
        string json = JsonUtility.ToJson(items);
        File.WriteAllText(FILEPATH, json);
    }

    public void add_item (){
        
    }
}