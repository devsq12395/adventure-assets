using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContClouds : MonoBehaviour {

    public static ContClouds I;
    public void Awake() { I = this; }

    public List<InGameEffect> clouds = new List<InGameEffect>();

    private float spawnRadius;
    private int maxClouds;
    private float cloudDistanceThreshold;

    void Start() { 
        spawnRadius = 50f;
        maxClouds = 4;
        cloudDistanceThreshold = 60f;
    }

    void Update() {
        if (!ContPlayer.I.player) return;
        return;
        
        GameObject player = ContPlayer.I.player.gameObject;
        Vector2 playerPosition = player.transform.position;

        // Count clouds within spawn radius
        int cloudCount = 0;
        foreach (Cloud cloud in FindObjectsOfType<Cloud>())
        {
            if (Vector2.Distance(cloud.transform.position, playerPosition) < spawnRadius)
            {
                cloudCount++;
            }
        }

        // Spawn clouds if needed
        while (cloudCount < maxClouds)
        {
            Vector2 spawnPosition;
            bool validPosition;
            do {
                validPosition = true;
                spawnPosition = playerPosition + Random.insideUnitCircle.normalized * 30f; // 30 units away
                foreach (Cloud cloud in FindObjectsOfType<Cloud>())
                {
                    if (Vector2.Distance(cloud.transform.position, spawnPosition) < 10f) // Check if within 10 units
                    {
                        validPosition = false;
                        break;
                    }
                }
            } while (!validPosition);
            
            int cloudType = Random.Range(1, 6); // Randomly select cloud type from 1 to 5
            ContEffect.I.create_effect($"cloud{cloudType}", spawnPosition);
            cloudCount++;
        }
    }
}