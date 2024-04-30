using UnityEngine;

public class SpinObject : MonoBehaviour
{
    public float rotationRate = 90f;

    void Update() {
        transform.Rotate(Vector3.forward, rotationRate * Time.deltaTime);
    }
}