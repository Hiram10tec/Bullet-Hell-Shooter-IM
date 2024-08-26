using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 0.5f;
    public float amplitude = 3f; // Amplitud del movimiento
    private float initialX;

    void Start()
    {
        initialX = transform.position.x;
    }

    void Update()
    {
        float newX = initialX + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}