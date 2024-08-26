using UnityEngine;
using UnityEngine.UI;

public class BulletCounterIM : MonoBehaviour
{
    public Text bulletIM;
    public string blueBulletTag = "BlueBullet";

    private void Update()
    {
        int blueBulletCount = GameObject.FindGameObjectsWithTag(blueBulletTag).Length;

        int totalBulletCount = blueBulletCount;

        // Actualizar el contador en la UI
        bulletIM.text = "Balas Iron Man: " + totalBulletCount.ToString();
    }
}