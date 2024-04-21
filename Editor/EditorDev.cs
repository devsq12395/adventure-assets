using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EditorDev : EditorWindow {

    [MenuItem("Window/Dev Tools")]
    public static void ShowWindow() {
        GetWindow<EditorDev>("EditorDev");
    }

    void OnGUI() {
        EditorGUILayout.LabelField("Cheats - In Game", EditorStyles.boldLabel);
        if (GUILayout.Button("God Mode")) {
            List<InGameObject> inGameObjects = GameObject.FindObjectsOfType<InGameObject>().ToList ();

            inGameObjects.ForEach ((obj) => {
                if (obj.owner != 1) return;

                obj.hp = 1000;
                obj.hpMax = 1000;
                obj.statHP = 1000;
                obj.statAttack = 1000;
                obj.statSkill = 1000;
            });

        }

         if (GUILayout.Button("Reset Data")) {
            JsonSaving.I.OVERWRITE = true;
            JsonSaving.I.load_json ();

            JsonSaving.I.OVERWRITE = false;

            Debug.Log ("Json Override done! Please restart the game.");

        }
    }
}
