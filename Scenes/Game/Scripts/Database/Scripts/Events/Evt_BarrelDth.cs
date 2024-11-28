using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evt_BarrelDth : EvtTrig
{
    private bool hasSpawned = false;
    private List<string> spawnableItems = new List<string> { "coin", "coin-pack" };

    public override void use() {
        if (hasSpawned) return;

        for (int i = 0; i < 5; i++) {
            string toSpawn = spawnableItems[Random.Range(0, spawnableItems.Count)];
            GameObject effect = ContEffect.I.create_effect(toSpawn, gameObject.transform.position);
            InGameEffect inGameEffect = effect.GetComponent<InGameEffect>();

            // Apply random knockback
            var knockback = effect.AddComponent<ForcedMoveEffect>();
            float randomAngle = Random.Range(0f, 360f);
            knockback.setup_forced_move(Random.Range(3f, 6f), 1f, randomAngle, "knockback", "spawnKnockback");
            if (inGameEffect) inGameEffect.SetJump(2.5f, 0.5f);
        }

        hasSpawned = true;
    }
}
