using UnityEngine;

public class BulletMov : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10; // Daño que inflige la bala

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.gameObject.GetComponent<Health>(); // Busca el componente Health en el objeto con el que colisionó
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage); // Aplica daño al objetivo
        }
        Destroy(gameObject);
    }
}
