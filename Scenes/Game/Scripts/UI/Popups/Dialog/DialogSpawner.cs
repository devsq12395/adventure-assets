using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogSpawner : MonoBehaviour {
    public static DialogSpawner I;
	public void Awake(){ I = this; }
    
    public GameObject dialogPrefab;

    public void SpawnDialog(string text, Vector3 _pos) {
        GameObject dialogObject = Instantiate(dialogPrefab, _pos, Quaternion.identity);
        DialogObj dialog = dialogObject.GetComponent<DialogObj> ();

        if (dialog != null) {
            dialog.SetDialogText(text);
        }
        else {
            Debug.LogError("UIDialog component not found on the dialog prefab.");
            Destroy(dialogObject);
        }
    }
}