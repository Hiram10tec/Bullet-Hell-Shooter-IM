using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossshooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 9f;
    public int bulletsPerShot = 10; // Reducir la cantidad de balas por disparo
    public float shootInterval = 1f; // Aumentar el intervalo entre disparos

    public float totalDuration = 120f; // Duración total del modo de disparo
    public float modeSwitchInterval = 10f; // Intervalo para cambiar de modo de disparo (10 segundos)
    private bool isAlive = true;
    private float elapsedTime = 0f;
    private bool isVisible = true; // Variable para controlar la visibilidad del jefe
    private float startDelay = 50f; // Tiempo de espera antes de empezar a disparar

    public enum FireMode
    {
        CurvedSpiral,
        FanBurst,
        Laser,
        StarPattern, // Nuevo patrón de disparo
        RapidFire,
        RandomSpray
    }

    private FireMode currentMode;

    void Start()
    {
        StartCoroutine(SwitchFireMode());
    }

    void Update()
    {
        if (isAlive)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= startDelay && isVisible)
            {
                if (elapsedTime % shootInterval < Time.deltaTime)
                {
                    Shoot();
                }
            }
        }
    }

    IEnumerator SwitchFireMode()
    {
        while (isAlive)
        {
            currentMode = (FireMode)Random.Range(0, System.Enum.GetValues(typeof(FireMode)).Length);
            yield return new WaitForSeconds(modeSwitchInterval);
        }
    }

    void Shoot()
    {
        if (!isVisible) return; // Verifica si el jefe está visible antes de disparar

        switch (currentMode)
        {
            case FireMode.CurvedSpiral:
                StartCoroutine(ShootCurvedSpiral());
                break;
            case FireMode.FanBurst:
                StartCoroutine(ShootFanBurst());
                break;
            case FireMode.Laser:
                StartCoroutine(ShootLaser());
                break;
            case FireMode.StarPattern:
                StartCoroutine(ShootStarPattern());
                break;
            case FireMode.RapidFire:
                StartCoroutine(ShootRapidFire());
                break;

        }
    }

    IEnumerator ShootCurvedSpiral()
    {
        float duration = 10f;
        float elapsed = 0f;
        float angleStep = 360f / bulletsPerShot;

        while (elapsed < duration && isAlive && isVisible)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                float angle = i * angleStep;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * firePoint.forward;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = direction * bulletSpeed;
            }
            elapsed += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ShootFanBurst()
    {
        float duration = 10f;
        float elapsed = 0f;

        while (elapsed < duration && isAlive && isVisible)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                float angle = (i - bulletsPerShot / 2) * 10f;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * firePoint.forward;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = direction * bulletSpeed;
            }
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ShootLaser()
    {
        float duration = 10f;
        float elapsed = 0f;

        while (elapsed < duration && isAlive && isVisible)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
            elapsed += 2f;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ShootStarPattern()
    {
        float duration = 10f;
        float elapsed = 0f;

        while (elapsed < duration && isAlive && isVisible)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                float angle = i * (360f / bulletsPerShot);
                Vector3 direction = Quaternion.Euler(0, angle, 0) * firePoint.forward;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = direction * bulletSpeed;
            }
            elapsed += 1f;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ShootRapidFire()
    {
        float duration = 10f;
        float elapsed = 0f;

        while (elapsed < duration && isAlive && isVisible)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
            elapsed += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }

}