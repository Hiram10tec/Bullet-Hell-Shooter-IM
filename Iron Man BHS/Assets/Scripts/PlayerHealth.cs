using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes agregar lo que debe suceder cuando el jugador muera
        Debug.Log("Player Dead!");
        gameObject.SetActive(false); // O usa Destroy(gameObject) si prefieres eliminar el objeto
    }
}
