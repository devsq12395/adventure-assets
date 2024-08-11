using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EditorDev : EditorWindow {

    private string itemName = ""; // Declare the itemName variable

    [MenuItem("Window/Dev Tools")]
    public static void ShowWindow() {
        GetWindow<EditorDev>("EditorDev");
    }

    void OnGUI() {
        EditorGUILayout.LabelField("Cheats - In Game", EditorStyles.boldLabel);
        
        if (GUILayout.Button("God Mode")) {
            List<InGameObject> inGameObjects = GameObject.FindObjectsOfType<InGameObject>().ToList();

            inGameObjects.ForEach((obj) => {
                if (obj.owner != 1) return;

                obj.hp = 1000;
                obj.hpMax = 1000;
                obj.statHP = 1000;
                obj.statAttack = 1000;
                obj.statSkill = 1000;
            });
        }

        if (GUILayout.Button("Reset Data (Press in Title Screen)")) {
            SaveHandler.I.reset_all_saves ();
            SaveHandler.I.check_save ();
            Debug.Log("Z-Player-Prefs reset! Please restart the game.");
        }

        // Give Item Section
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Give Item (Format: like-this)", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("- Run while the game is running (main menu)");
        
        itemName = EditorGUILayout.TextField("Item Name", itemName);
        
        if (GUILayout.Button("Give")) {
            Inv2.I.add_item(itemName);
            Debug.Log($"Item {itemName} is added.");
        }
    }
}
