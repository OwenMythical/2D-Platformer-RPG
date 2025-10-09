using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int Health;

    public GameObject Spawn;

    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health < 1)
        {
            transform.position = Spawn.transform.position;
        }
    }
}
