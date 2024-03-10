using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterScene : MonoBehaviour {

    public static MasterScene I;
    public void Awake(){ I = this; }

    public string curMainScene = "";

    void Start(){
        add_scene ("GlobalScripts");
        change_main_scene ("MainMenu");
    }

    void Update(){
        
    }

    public void change_main_scene (string _scene) {
        if (curMainScene != "") remove_scene (curMainScene);
        add_scene (_scene);
        curMainScene = _scene;
    }

    public void add_scene (string _scene){
        SceneManager.LoadScene (_scene, LoadSceneMode.Additive);
    }

    public void remove_scene (string _scene){
        SceneManager.UnloadSceneAsync (_scene);
    }

    public void switch_camera(string _cameraName) {
        Camera[] cameras = Camera.allCameras;
        foreach (Camera camera in cameras) {
            camera.enabled = false;
        }

        GameObject cameraObject = GameObject.Find(_cameraName);
        if (cameraObject != null) {
            Camera targetCamera = cameraObject.GetComponent<Camera>();
            if (targetCamera != null) {
                targetCamera.enabled = true;
            }
        }
    }
}
