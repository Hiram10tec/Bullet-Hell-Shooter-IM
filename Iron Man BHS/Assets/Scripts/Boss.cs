using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 2f;
    public float amplitude = 6f; // Amplitud del movimiento
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