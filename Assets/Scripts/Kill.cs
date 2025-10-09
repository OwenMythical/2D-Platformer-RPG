using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Kill : MonoBehaviour
{
    public HealthManager HealthScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            HealthScript.TakeDamage(99);
        }
    }
}
