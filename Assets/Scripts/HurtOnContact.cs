using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class HurtOnContact : MonoBehaviour
{
    public bool LaunchOnHurt;
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
                Cooldown = true;
                HM.TakeDamage(1, LaunchOnHurt);
                Wait(1);
                Cooldown = false;
            }
        }
    }

    IEnumerator Wait(int Time)
    {
        yield return new WaitForSeconds(Time);
    }
}
