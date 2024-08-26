using UnityEngine;
using System.Collections; // Agrega esta línea para usar IEnumerator

public class EnemyManager : MonoBehaviour
{
    public GameObject boss; // Arrastra el objeto del jefe aquí desde el Inspector
    public float invisibilityDuration = 30f; // Duración de la invisibilidad al inicio
    private bool isVisible = false; // Estado de visibilidad del jefe
    private float nextPatternTime; // Tiempo para el próximo cambio de patrón

    void Start()
    {
        // Configura el tiempo para el primer cambio de patrón
        nextPatternTime = Time.time + 10f;

        // Iniciar la invisibilidad del jefe
        StartCoroutine(InvisibilityCoroutine());
    }

    IEnumerator InvisibilityCoroutine()
    {
        while (true)
        {
            // Desactivar todos los renderizadores del jefe y sus hijos
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            // Desactivar otros componentes si es necesario, por ejemplo:
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }

            isVisible = false; // El jefe está invisible

            // Espera el tiempo de invisibilidad
            yield return new WaitForSeconds(invisibilityDuration);

            // Reactivar todos los renderizadores para hacer visible el jefe
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }

            // Reactivar los colliders o cualquier otro componente
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }

            isVisible = true; // El jefe está visible

            // Espera el tiempo de visibilidad antes de volver a ser invisible
            yield return new WaitForSeconds(invisibilityDuration);
        }
    }
}