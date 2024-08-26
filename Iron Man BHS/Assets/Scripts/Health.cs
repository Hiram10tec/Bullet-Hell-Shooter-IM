using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del objeto
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la vida actual con la vida máxima
    }

    // Método para aplicar daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para morir o destruir el objeto
    void Die()
    {
        Destroy(gameObject); // Destruye el objeto del juego
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Asegura que la salud no supere la vida máxima
        }
    }

    void OnGUI()
    {
        // Muestra la vida del jugador y del jefe en la pantalla
        if (gameObject.CompareTag("Player"))
        {
            GUI.Label(new Rect(10, 10, 150, 20), "Vida Jugador: " + currentHealth);
        }
        else if (gameObject.CompareTag("Boss"))
        {
            GUI.Label(new Rect(10, 40, 150, 20), "Vida Boss: " + currentHealth);
        }
    }
}