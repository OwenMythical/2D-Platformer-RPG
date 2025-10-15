using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int Health;
    public Rigidbody2D RB;

    public GameObject Spawn;

    public void TakeDamage(int Damage,bool Launch)
    {
        Health -= Damage;
        if (Health < 1)
        {
            transform.position = Spawn.transform.position;
            Health = MaxHealth;
        }
        else if (Launch == true)
        {
            RB.linearVelocity = new Vector2(0, 0);
            RB.AddForce(transform.up * 10, ForceMode2D.Impulse);
        }
    }
}
