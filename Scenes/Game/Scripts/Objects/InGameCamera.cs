using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCamera : MonoBehaviour {

    public static InGameCamera I;
	public void Awake(){ I = this; }

    public bool IS_LOCK;

    public Transform target;
    public Camera cam;
    private Vector3 pos;

    void Start() {
        cam = gameObject.GetComponent <Camera> ();
    }

    void Update() {
        if (target && IS_LOCK) {
            point_to_target ();
        }
    }
    
    public Vector2 get_mouse_pos () {
        Vector2 _pos = Input.mousePosition;
        Vector2 _ret = cam.ScreenToWorldPoint(new Vector3(_pos.x, _pos.y, 0));

        return _ret;
    }

    public void point_to_target (){
        DB_Maps.mapDetails _details = ContMap.I.details;
        float _targX, _targY;
        float CAMERA_SIZE_X = 18, CAMERA_SIZE_Y = 9.5f;

        _targX = target.position.x;
        _targX = Mathf.Clamp(_targX, -_details.size.x + CAMERA_SIZE_X, _details.size.x - CAMERA_SIZE_X);

        _targY = target.position.y;
        _targY = Mathf.Clamp(_targY, -_details.size.y + CAMERA_SIZE_Y, _details.size.y - CAMERA_SIZE_Y);

        pos.x = _targX;
        pos.y = _targY;
        pos.z = -10;

        transform.position = pos;
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void set_target (Transform _target) {
        target = _target;
    }
}
