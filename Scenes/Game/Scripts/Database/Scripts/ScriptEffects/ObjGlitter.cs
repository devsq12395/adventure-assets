using UnityEngine;

public class ObjGlitter : MonoBehaviour
{
    public string colorHex;
    public int spawnAmount = 5;
    public float circleDiameter = 1f;
    public float tweenDur = 1f;
    public float targetDist = 3f;

    public float spawnIntervalTarg = 0.25f;
    private float spawnInterval = 0;

    void Update() {
        spawnInterval -= Time.deltaTime;

        if (spawnInterval <= 0) {
            spawnInterval = spawnIntervalTarg;

            ContEffect.I.spawn_multi_effect_with_fade_tweens (
                colorHex,
                spawnAmount,
                gameObject.transform.position,
                targetDist,
                circleDiameter,
                tweenDur
            );
        }
    }
}