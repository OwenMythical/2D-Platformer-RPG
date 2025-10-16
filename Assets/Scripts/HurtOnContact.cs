using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class HurtOnContact : MonoBehaviour
{
    public bool LaunchOnHurt;
    public bool KnockbackOnHurt;
    HealthManager HM;
    bool Cooldown;

    void Awake()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        HM = (HealthManager)Player.GetComponent("HealthManager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Cooldown == false)
            {
                Wait(1);
                HM.TakeDamage(1, LaunchOnHurt, KnockbackOnHurt, transform.position);
            }
        }
    }

    IEnumerator Wait(int Time)
    {
        Cooldown = true;
        yield return new WaitForSeconds(Time);
        Cooldown = false;
    }
}
