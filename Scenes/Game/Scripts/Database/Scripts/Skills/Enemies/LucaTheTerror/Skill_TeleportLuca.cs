using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Teleport : SkillTrig {

    public float alphaFadeDuration = 1f; // Duration for alpha tweening
    public float afterimageDistance = 1f; // Distance for afterimages to move left/right

    private GameObject CreateAfterimagePrefab() {
        // Create a temporary GameObject to hold the afterimage
        GameObject tempAfterimage = new GameObject("Afterimage");

        // Copy the SpriteRenderer component from the caster (owner of the skill)
        SpriteRenderer casterRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (casterRenderer != null) {
            SpriteRenderer afterimageRenderer = tempAfterimage.AddComponent<SpriteRenderer>();
            afterimageRenderer.sprite = casterRenderer.sprite;
            afterimageRenderer.color = casterRenderer.color; // Use the same color
        }

        return tempAfterimage;
    }

    public override void use_active() {
        if (!use_check()) return;

        InGameObject _ownerComp = gameObject.GetComponent<InGameObject>();
        if (!DB_Conditions.I.can_move(_ownerComp)) return;

        Vector2 mapSize = ContMap.I.details.size;
        Vector2 _pos = gameObject.transform.position;

        // Generate a random point within the map boundaries
        Vector2 randomPoint = new Vector2(
            Random.Range(0, mapSize.x),
            Random.Range(0, mapSize.y)
        );

        // Create afterimages
        GameObject afterimagePrefab = CreateAfterimagePrefab();
        if (afterimagePrefab != null) {
            Vector3 leftTargetPos = new Vector3(_pos.x - afterimageDistance, _pos.y, -1f);
            Vector3 rightTargetPos = new Vector3(_pos.x + afterimageDistance, _pos.y, -1f);

            CreateAfterimage(_pos, leftTargetPos, afterimagePrefab);  // Tween to the left
            CreateAfterimage(_pos, rightTargetPos, afterimagePrefab); // Tween to the right

            // Destroy the afterimage prefab to ensure it's not left in the scene
            Destroy(afterimagePrefab);
        }

        // Teleport the owner to the new random position
        ContEffect.I.create_effect("move-smoke", gameObject.transform.position);
        gameObject.transform.position = randomPoint;
        ContEffect.I.create_effect("move-smoke", randomPoint);
    }

    private void CreateAfterimage(Vector3 startPosition, Vector3 targetPosition, GameObject prefab) {
        if (prefab == null) return;

        GameObject afterimage = Instantiate(prefab, startPosition, Quaternion.identity);

        // Set the scale of the afterimage to match the owner
        afterimage.transform.localScale = gameObject.transform.localScale;

        StartCoroutine(MoveAndFadeAfterimage(afterimage, startPosition, targetPosition));
    }

    private IEnumerator MoveAndFadeAfterimage(GameObject afterimage, Vector3 startPosition, Vector3 targetPosition) {
        SpriteRenderer renderer = afterimage.GetComponent<SpriteRenderer>();
        if (renderer == null) yield break;

        Color color = renderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < alphaFadeDuration) {
            float t = elapsedTime / alphaFadeDuration;
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, t);
            afterimage.transform.position = currentPos;

            float alpha = Mathf.Lerp(0.5f, 0f, t);
            color.a = alpha;
            renderer.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha is 0
        color.a = 0f;
        renderer.color = color;

        // Remove the SpriteRenderer component to make sure the afterimage is invisible
        Destroy(renderer);

        // Optionally destroy the afterimage object
        Destroy(afterimage, 0.1f); // Delay to ensure renderer is destroyed
    }
}
