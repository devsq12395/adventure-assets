using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ContEffect : MonoBehaviour
{
    public static ContEffect I;
    public void Awake() { I = this; }

    public int curID = 0;

    private List<GameObject> objectPool = new List<GameObject>();
    private Sprite circleSprite;

    void Start() {
        circleSprite = CreateCircleSprite(128, 128);
    }

    void Update() { 

    }

    public void update_effect (InGameEffect _obj){

    }

    public GameObject create_effect(string _name, Vector2 _pos)
    {
        GameObject _obj = DB_Objects.I.get_game_obj(_name);
        InGameEffect _comp = _obj.GetComponent<InGameEffect>();

        _obj.transform.position = new Vector3(_pos.x, _pos.y, -9);
        _comp.curPos = _obj.transform.position;

        return _obj;
    }

    public void spawn_multi_effect_with_fade_tweens
    (string gameObjectString, int spawnAmount, Vector2 startPosition, float targetDistance, float circleDiameter = 1f,
        float tweenDur = 1f)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject instance;
            if (IsHexColor(gameObjectString))
            {
                instance = GetPooledObject();
                instance.GetComponent<SpriteRenderer>().sprite = circleSprite;
                instance.transform.localScale = new Vector3(circleDiameter, circleDiameter, 1f);

                Color color;
                if (ColorUtility.TryParseHtmlString("#" + gameObjectString, out color)) {
                    instance.GetComponent<SpriteRenderer>().color = color;
                }
            }
            else
            {
                instance = create_effect(gameObjectString, startPosition);
            }

            // Set the initial position
            instance.transform.position = new Vector3(startPosition.x, startPosition.y, -9);
            InGameEffect _comp = instance.GetComponent<InGameEffect>();
            if (_comp != null) {
                _comp.curPos = instance.transform.position;
            }

            // Calculate random angle and target position
            float angle = Random.Range(0f, 360f);
            Vector2 targetPosition = startPosition + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * targetDistance;

            // Tween curPos
            if (_comp != null) {
                DOTween.To(() => _comp.curPos, x => _comp.curPos = x, new Vector3(targetPosition.x, targetPosition.y, -9), tweenDur)
                    .OnComplete(() => ReturnToPool(instance))
                    .SetUpdate(true);
            }

            // Tween scale from 1 to 0
            instance.transform.DOScale(Vector3.zero, tweenDur).SetUpdate(true);

            // Tween alpha from 1 to 0.5
            SpriteRenderer sr = instance.GetComponent<SpriteRenderer>();
            if (sr != null) {
                sr.DOFade(0.5f, tweenDur).SetUpdate(true);
            }
        }
    }

    public void effect_tweens(){

    }

    private bool IsHexColor(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length != 6) return false;
        foreach (char c in str)
        {
            if (!"0123456789ABCDEFabcdef".Contains(c)) return false;
        }
        return true;
    }

    private Sprite CreateCircleSprite(int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                float xCoord = (float)x / texture.width * 2 - 1;
                float yCoord = (float)y / texture.height * 2 - 1;
                if (xCoord * xCoord + yCoord * yCoord <= 1)
                {
                    texture.SetPixel(x, y, Color.white);
                }
                else
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    private GameObject GetPooledObject()
    {
        foreach (var obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = new GameObject("PooledObject");
        newObj.AddComponent<SpriteRenderer>();
        objectPool.Add(newObj);
        return newObj;
    }

    private void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
