using UnityEngine;

public class CurvedBullet : MonoBehaviour
{
    public float curveStrength = 1f; // Strength of the curve
    public float speed = 10f; // Speed of the bullet

    private Vector3 direction;

    void Start()
    {
        direction = transform.forward;
    }

    void Update()
    {
        // Apply a curve to the bullet's trajectory
        direction = Quaternion.Euler(0, curveStrength * Time.deltaTime, 0) * direction;
        transform.position += direction * speed * Time.deltaTime;
    }
}
