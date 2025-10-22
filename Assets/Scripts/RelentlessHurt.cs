using UnityEngine;
using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Kill : MonoBehaviour
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
                StartCoroutine(Wait(0.1f));
                HM.TakeDamage(2, LaunchOnHurt, KnockbackOnHurt, transform.position);
            }
        }
    }

    IEnumerator Wait(float Time)
    {
        Cooldown = true;
        yield return new WaitForSeconds(Time);
        Cooldown = false;
    }
}
