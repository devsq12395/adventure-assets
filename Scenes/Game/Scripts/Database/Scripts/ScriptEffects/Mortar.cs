using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mortar : MonoBehaviour {
    public Vector3 targetPoint;
    private InGameObject inGameObject;
    private GameObject crosshair;

    public void set_target_point(Vector3 _targetPoint, float _height, float _duration) {
        inGameObject = GetComponent<InGameObject>();
        
        targetPoint = _targetPoint;
        transform.DOMove(targetPoint, _duration).SetEase(Ease.Linear).OnComplete(target_reached);
        ContObj.I.set_jump(inGameObject, _height, _duration);
    }

    public void set_crosshair(GameObject _crosshair) {
        crosshair = _crosshair;
    }

    private void target_reached() {
        SoundHandler.I.play_sfx ("explosion");
        DealDamageToNearbyEnemies();

        if (crosshair != null) Destroy(crosshair);
        Destroy(gameObject);
    }

    void DealDamageToNearbyEnemies() {
        // Define the radius for detecting nearby enemies
        float damageRadius = 5.0f; // Adjust as needed
        ContEffect.I.create_effect ("explosion1", gameObject.transform.position);

        InGameObject[] allObjects = FindObjectsOfType<InGameObject>();
        foreach (var enemy in allObjects) {
            if (enemy.owner != inGameObject.owner) {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= damageRadius) {
                    // Deal damage using ContDamage.I.damage
                    Debug.Log("Dealing damage to " + enemy.name);
                    ContDamage.I.damage(inGameObject, enemy, inGameObject.hitDam, new List<string>());
                }
            }
        }
    }
}
