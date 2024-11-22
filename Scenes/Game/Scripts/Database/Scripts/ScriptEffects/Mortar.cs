using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mortar : MonoBehaviour {
    public Vector3 targetPoint;
    private InGameObject inGameObject;

    void Start() {
        inGameObject = GetComponent<InGameObject>();
        if (inGameObject == null) {
            Debug.LogError("InGameObject component is missing.");
            enabled = false;
            return;
        }

        // Calculate the duration based on the distance to the target point
        float distance = Vector3.Distance(transform.position, targetPoint);
        float duration = distance / inGameObject.speed;

        // Use DOTween to synchronize the jump and movement duration
        transform.DOMove(targetPoint, duration).SetEase(Ease.Linear);
        ContObj.I.set_jump(inGameObject, 3, duration);
    }

    void Update() {
        if (inGameObject == null) return;

        // Check if the object has reached the target point
        if (Vector3.Distance(transform.position, targetPoint) <= 0.1f) {
            DealDamageToNearbyEnemies();
            enabled = false; // Disable the script once the target is reached and damage is dealt
        }
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
