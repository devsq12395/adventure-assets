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

    public Vector2 get_next_point_in_direction (Vector2 position, float angle, float distance, bool checkMapBounds = true) {
        float angleRad = angle * Mathf.Deg2Rad;
        Vector2 nextPoint = new Vector2(position.x + Mathf.Cos(angleRad) * distance, position.y + Mathf.Sin(angleRad) * distance);

        if (checkMapBounds)
        {
            Vector2 mapSize = ContMap.I.details.size;

            if (nextPoint.x < 0 || nextPoint.y < 0 || nextPoint.x > mapSize.x || nextPoint.y > mapSize.y)
            {
                // Calculate the direction vector from the position to the next point
                Vector2 direction = (nextPoint - position).normalized;

                // Calculate the distance from the position to the closest boundary
                float distanceToBoundaryX = Mathf.Abs(nextPoint.x < 0 ? position.x : mapSize.x - position.x);
                float distanceToBoundaryY = Mathf.Abs(nextPoint.y < 0 ? position.y : mapSize.y - position.y);

                // Calculate the maximum distance allowed to bring the point within bounds
                float maxDistanceX = Mathf.Abs(distanceToBoundaryX / Mathf.Cos(angleRad));
                float maxDistanceY = Mathf.Abs(distanceToBoundaryY / Mathf.Sin(angleRad));

                // Calculate the maximum distance that can be used without going out of bounds
                float maxDistance = Mathf.Min(maxDistanceX, maxDistanceY);

                // Modify the distance parameter to ensure the next point is within bounds
                distance = Mathf.Clamp(maxDistance - 0.5f, 0f, distance);

                // Recalculate the next point with the modified distance
                nextPoint = position + direction * distance;
            }
        }

        return nextPoint;
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
