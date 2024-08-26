using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    public Text bulletCounterText;
    public string redBulletTag = "RedBullet";
    public string blueBulletTag = "BlueBullet";

    private void Update()
    {
        int redBulletCount = GameObject.FindGameObjectsWithTag(redBulletTag).Length;
        int blueBulletCount = GameObject.FindGameObjectsWithTag(blueBulletTag).Length;

        int totalBulletCount = redBulletCount + blueBulletCount;

        // Actualizar el contador en la UI
        bulletCounterText.text = "Total de Balas: " + totalBulletCount.ToString();
    }
}