using Platformer;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int Health;
    public Rigidbody2D RB;
    public PlayerController PC;
    GameHandler GH;
    GameObject Spawn;

    void Awake()
    {
        GameObject HandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        GH = (GameHandler)HandlerObject.GetComponent("GameHandler");
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        gameObject.transform.position = Spawn.transform.position;
    }

    public void Respawn()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        gameObject.transform.position = Spawn.transform.position;
    }

    public void TakeDamage(int Damage,bool Launch, bool Knock, Vector3 Source)
    {
        Health -= Damage;
        if (Health < 1)
        {
            transform.position = Spawn.transform.position;
            Health = MaxHealth;
            GH.Died();
        }
        else if (Launch == true)
        {
            RB.linearVelocity = new Vector2(0, 0);
            RB.AddForce(transform.up * 10, ForceMode2D.Impulse);
        }
        else if (Knock == true)
        {
            RB.linearVelocity = new Vector2(0, 0);
            Vector2 DirectionalForce = (transform.position - Source);
            DirectionalForce = new Vector2(Math.Sign(DirectionalForce.x), 1);
            RB.linearVelocity = DirectionalForce * 5;
            StartCoroutine(Stun(0.5f));
        }

        IEnumerator Stun(float StunTime)
        {
            PC.Stunned = true;
            yield return new WaitForSeconds(StunTime);
            PC.Stunned = false;
        }
        GH.HealthChanged(Health);
    }
}