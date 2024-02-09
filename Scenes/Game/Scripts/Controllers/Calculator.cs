using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {

    public static Calculator I;
	public void Awake(){ I = this; }

    public Vector2 get_pos_on_dist (Vector2 _pos, float _ang, float _dist){
        float _x = _pos.x + _dist * Mathf.Cos(_ang * Mathf.Deg2Rad);
        float _y = _pos.y + _dist * Mathf.Sin(_ang * Mathf.Deg2Rad);

        return new Vector2(_x, _y);
    }

    public float get_ang_from_2_points_deg (Vector2 _p1, Vector2 _p2){
        Vector2 _dir = _p2 - _p1;
        return Mathf.Atan2 (_dir.y, _dir.x) * Mathf.Rad2Deg;
    }
    
    public float get_ang_from_point_and_mouse (Vector2 _p){
        Vector2 _mousePos = InGameCamera.I.get_mouse_pos (),
                _mousePos_scrn = Input.mousePosition,
                _dir = _mousePos - _p;
        
        return Mathf.Atan2 (_dir.y, _dir.x) * Mathf.Rad2Deg;
    }
    
    public bool is_mouse_left_of_object (InGameObject _go){
        Vector2 _pos = _go.transform.position,
                _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return (_mousePos.x <= _pos.x);
    }

    public float get_ang_from_2_points_rad (Vector2 _p1, Vector2 _p2){
        Vector2 _dir = _p2 - _p1;
        return Mathf.Atan2 (_dir.y, _dir.x);
    }

    public Vector2 get_dir_from_2_points (Vector2 _p1, Vector2 _p2){
        Vector2 _ret = _p1 - _p2;
        return _ret;
    }

    public float get_dist_from_2_points (Vector2 _p1, Vector2 _p2){
        return Vector2.Distance (_p1, _p2);
    }

    public Vector2 get_next_point_in_direction (Vector2 _pos, float _ang, float _dist){
        float _angRad = _ang * Mathf.Deg2Rad;
        Vector2 _ret = new Vector2 (_pos.x + Mathf.Cos (_angRad) * _dist, _pos.y + Mathf.Sin (_angRad) * _dist);
        
        return _ret;
    }

    public string generate_id (){
        string _cL = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    
        int length = 8;
        System.Text.StringBuilder idBuilder = new System.Text.StringBuilder();

        for (int i = 0; i < length; i++) {
            int randomIndex = Random.Range (0, _cL.Length);
            char randomChar = _cL [randomIndex];
            idBuilder.Append (randomChar);
        }

        return idBuilder.ToString();
    }
}
