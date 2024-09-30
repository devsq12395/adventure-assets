using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EditorDev : EditorWindow {

    private string itemName = "", toMission = "", toMenuMap = "", gainGold = "";

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

        gainGold = EditorGUILayout.TextField("Gain Gold (only works on overworld):", gainGold);
        if (GUILayout.Button("Gain Gold")) {
            MainMenu.I.update_gold(Convert.ToInt32(gainGold));
        }

        EditorGUILayout.LabelField("Shortcuts", EditorStyles.boldLabel);
        toMission = EditorGUILayout.TextField("To Mission:", toMission);
        if (GUILayout.Button("To Mission")) {
            ZPlayerPrefs.SetString ("missionCur", toMission);
            PlayerPrefs.SetInt ("cur-map-lvl", 0);
            MasterScene.I.change_main_scene ("Game");
        }
        toMenuMap = EditorGUILayout.TextField("To Menu Map:", toMenuMap);
        if (GUILayout.Button("To Menu")) {
            ZPlayerPrefs.SetString("main-menu-map", toMenuMap);
            MasterScene.I.change_main_scene ("MainMenu");
        }

        // Log Checkers
        EditorGUILayout.LabelField("Log Checkers", EditorStyles.boldLabel);
        if (GUILayout.Button("Log all items")) {
            Inv2.I.log_all_items ();
        }
    }
}
