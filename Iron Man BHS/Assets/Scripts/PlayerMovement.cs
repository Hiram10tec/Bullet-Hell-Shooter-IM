using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float normalSpeed = 10f;
    public float xMin, xMax, zMin, zMax;
    public float slowSpeed = 5f;
    public KeyCode slowKey = KeyCode.LeftShift;
    public int health = 3; // Salud del jugador

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveSpeed = Input.GetKey(slowKey) ? slowSpeed : normalSpeed;
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 move = new Vector3(moveX, 0f, moveZ);
        controller.Move(move * Time.deltaTime);

        // Limitar la posición del CharacterController dentro de los límites
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, xMin, xMax),
            transform.position.y,
            Mathf.Clamp(transform.position.z, zMin, zMax)
        );
        transform.position = clampedPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            TakeDamage(1); // Reduce la salud del jugador
            Destroy(other.gameObject); // Destruye la bala enemiga
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Lógica para manejar la muerte del jugador
        Debug.Log("Player has died!");
        Destroy(gameObject); // Destruye el objeto del jugador
    }
}