using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 9f;
    public int bulletsPerShot = 11;
    public float shootInterval = 1f; // Intervalo entre disparos

    public float totalDuration = 120f; // Duración total del modo de disparo
    public float modeSwitchInterval = 10f; // Intervalo para cambiar de modo de disparo (10 segundos)
    private bool isAlive = true;
    private float elapsedTime = 0f;

    public enum FireMode
    {
        CurvedSpiral,
        FanBurst,
        Laser,
        StarPattern // Nuevo patrón de disparo
    }

    public FireMode currentFireMode;

    void Start()
    {
        // Inicia con un modo de disparo aleatorio
        SwitchFireMode();
        StartCoroutine(ShootSequence());
        StartCoroutine(SwitchFireModeAutomatically());
    }

    void OnDisable()
    {
        isAlive = false;
    }

    IEnumerator SwitchFireModeAutomatically()
    {
        while (isAlive && elapsedTime < totalDuration)
        {
            yield return new WaitForSeconds(modeSwitchInterval);
            SwitchFireMode();
        }
    }

    void SwitchFireMode()
    {
        // Escoge un modo de disparo aleatorio
        int randomModeIndex = Random.Range(0, System.Enum.GetValues(typeof(FireMode)).Length);
        currentFireMode = (FireMode)randomModeIndex;
        Debug.Log("Modo de disparo cambiado a: " + currentFireMode);
    }

    IEnumerator ShootSequence()
    {
        while (isAlive)
        {
            Debug.Log("Disparando en modo: " + currentFireMode);
            switch (currentFireMode)
            {
                case FireMode.StarPattern: // Nuevo patrón de disparo
                    yield return StartCoroutine(ShootStarPattern());
                    break;
                case FireMode.CurvedSpiral:
                    yield return StartCoroutine(ShootCurvedSpiralContinuously());
                    break;
                case FireMode.FanBurst:
                    yield return StartCoroutine(ShootFanBurst());
                    break;
                case FireMode.Laser:
                    yield return StartCoroutine(ShootLaser());
                    break;

            }
            yield return new WaitForSeconds(shootInterval);
        }
    }
    IEnumerator ShootCurvedSpiralContinuously()
    {
        Debug.Log("Disparando en modo CurvedSpiral");
        float duration = 10f; // Duración del modo de disparo
        float elapsed = 0f;
        float angleStep = 360f / bulletsPerShot;
        float angle = 0f;
        float radius = 5f; // Radio inicial del óvalo

        while (elapsed < duration && elapsedTime < totalDuration)
        {
            for (int i = 0; i < bulletsPerShot; i++)
            {
                // Calcula la posición de la primera bala
                float bulletDirX1 = firePoint.position.x + radius * Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulletDirZ1 = firePoint.position.z + radius * Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulletMoveVector1 = new Vector3(bulletDirX1, firePoint.position.y, bulletDirZ1);
                Vector3 bulletDir1 = (bulletMoveVector1 - firePoint.position).normalized;

                GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb1 = bullet1.GetComponent<Rigidbody>();
                rb1.velocity = bulletDir1 * bulletSpeed;

                // Calcula la posición de la segunda bala (opuesta a la primera)
                float bulletDirX2 = firePoint.position.x - radius * Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulletDirZ2 = firePoint.position.z - radius * Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulletMoveVector2 = new Vector3(bulletDirX2, firePoint.position.y, bulletDirZ2);
                Vector3 bulletDir2 = (bulletMoveVector2 - firePoint.position).normalized;

                GameObject bullet2 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
                rb2.velocity = bulletDir2 * bulletSpeed;

                angle += angleStep;
            }
            elapsed += shootInterval;
            elapsedTime += shootInterval;
            yield return new WaitForSeconds(shootInterval);
        }
    }

    IEnumerator ShootFanBurst()
    {
        Debug.Log("Disparando en modo FanBurst");
        int burstCount = 3;
        float burstInterval = 0.2f;

        for (int i = 0; i < burstCount && elapsedTime < totalDuration; i++)
        {
            for (int j = 0; j < bulletsPerShot; j++)
            {
                float angle = j * (360f / bulletsPerShot);
                float bulletDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulletDirZ = firePoint.position.z + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulletMoveVector = new Vector3(bulletDirX, firePoint.position.y, bulletDirZ);
                Vector3 bulletDir = (bulletMoveVector - firePoint.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bulletDir * bulletSpeed;
            }
            elapsedTime += burstInterval;
            yield return new WaitForSeconds(burstInterval);
        }
    }

    IEnumerator ShootLaser()
    {
        Debug.Log("Disparando en modo Laser");
        while (currentFireMode == FireMode.Laser && elapsedTime < totalDuration)
        {
            GameObject laser = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = laser.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * bulletSpeed;
            elapsedTime += 0.1f;
            yield return new WaitForSeconds(0.1f); // Intervalo muy corto entre disparos para un láser continuo
        }
    }

    IEnumerator ShootStarPattern()
    {
        Debug.Log("Disparando en modo StarPattern");
        int points = 3; // Número de puntos de la estrella
        float angleStep = 360f / points;
        float angle = 0f;

        while (isAlive && elapsedTime < totalDuration)
        {
            for (int i = 0; i < points; i++)
            {
                float bulletDirX = firePoint.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulletDirZ = firePoint.position.z + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulletMoveVector = new Vector3(bulletDirX, firePoint.position.y, bulletDirZ);
                Vector3 bulletDir = (bulletMoveVector - firePoint.position).normalized;

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                rb.velocity = bulletDir * bulletSpeed;

                angle += angleStep;
            }
            elapsedTime += Time.deltaTime;
            yield return null; // No delay, se ejecuta en cada frame
        }
    }
}