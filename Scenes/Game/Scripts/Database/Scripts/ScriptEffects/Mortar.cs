using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mortar : MonoBehaviour {
    public Vector3 targetPoint;
    private InGameObject inGameObject;

    private float tweenDuration = 1;

    public void set_target_point(Vector3 _targetPoint, float _height) {
        inGameObject = GetComponent<InGameObject>();
        
        targetPoint = _targetPoint;
        transform.DOMove(targetPoint, tweenDuration).SetEase(Ease.Linear).OnComplete(target_reached);
        ContObj.I.set_jump(inGameObject, _height, tweenDuration);
    }

    private void target_reached() {
        Debug.Log("Mortar has reached the target point.");
        DealDamageToNearbyEnemies();
        Destroy(gameObject);
    }

    void DealDamageToNearbyEnemies() {
        // Define the radius for detecting nearby enemies
        float damageRadius = 5.0f; // Adjust as needed

        Collider[] hitColliders = Physics.OverlapSphere(targetPoint, damageRadius);
        foreach (var hitCollider in hitColliders) {
            InGameObject enemy = hitCollider.GetComponent<InGameObject>();
            if (enemy != null && enemy.owner != inGameObject.owner) {
                // Deal damage using ContDamage.I.damage
                ContDamage.I.damage(inGameObject, enemy, inGameObject.hitDam, new List<string>());
            }
        }
    }
}
