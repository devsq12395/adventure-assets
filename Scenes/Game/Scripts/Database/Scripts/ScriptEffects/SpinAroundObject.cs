using UnityEngine;

public class SpinAroundObject : MonoBehaviour
{
    // Target to orbit around
    public Transform target;

    // The distance between the object and the target
    public float orbitDistance = 2f;

    // The speed of the orbit
    public float orbitSpeed = 50f;

    // Initial angle in degrees
    public float initialAngle = 0f;

    // Private variable to keep track of the current angle
    private float currentAngle;

    void Start()
    {
        // Convert the initial angle to radians and set the initial position
        currentAngle = initialAngle * Mathf.Deg2Rad;
        SetInitialPosition();

        if (!target) {
            Destroy (this);
        }
    }

    void Update()
    {
        // Rotate the object around the target based on the orbit speed
        OrbitAroundTarget();

        if (!target) {
            Destroy (gameObject);
        }
    }

    // Set the initial position based on the input angle and distance
    public void SetInitialPosition()
    {
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject>(),
            targetComp = target.GetComponent <InGameObject> ();
        
        float x = Mathf.Cos(currentAngle) * orbitDistance;
        float y = Mathf.Sin(currentAngle) * orbitDistance;
        _ownerComp.curPos = new Vector2(targetComp.curPos.x + x, targetComp.curPos.y + y);
    }

    // Rotate the object around the target
    public void OrbitAroundTarget()
    {
        InGameObject _ownerComp = gameObject.GetComponent <InGameObject> (),
            targetComp = target.GetComponent <InGameObject> ();
        
        // Increase the angle over time based on the orbit speed
        currentAngle += orbitSpeed * Time.deltaTime * Mathf.Deg2Rad;

        // Calculate the new position
        float x = Mathf.Cos(currentAngle) * orbitDistance;
        float y = Mathf.Sin(currentAngle) * orbitDistance;

        // Set the position of the object
        _ownerComp.curPos = new Vector2(targetComp.curPos.x + x, targetComp.curPos.y + y);
    }
}
