using UnityEngine;
using UnityEngine.UI;

public class BulletCounterEnemy : MonoBehaviour
{
    public Text bulletenemy;
    public string redBulletTag = "RedBullet";
    private void Update()
    {
        int redBulletCount = GameObject.FindGameObjectsWithTag(redBulletTag).Length;
        int totalBulletCount = redBulletCount;

        // Actualizar el contador en la UI
        bulletenemy.text = "Balas enemigas: " + totalBulletCount.ToString();
    }
}