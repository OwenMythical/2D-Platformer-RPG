using Platformer;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int Health;
    public Rigidbody2D RB;
    public PlayerController PC;
    public Animator ANM;
    public BoxCollider2D BC;
    GameHandler GH;
    GameObject Spawn;

    void Awake()
    {
        GameObject HandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        GH = (GameHandler)HandlerObject.GetComponent("GameHandler");
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        gameObject.transform.position = Spawn.transform.position;
    }

    public void TakeDamage(int Damage, bool Launch, bool Knock, Vector3 Source)
    {
        if (PC.Stunned == false)
        {
            Health -= Damage;
        }
        if (Launch == true)
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
        if (Health < 1)
        {
            StartCoroutine(Die());
        }
        if (Health < 4)
        {
            GameObject GV = GameObject.FindGameObjectWithTag("GlobalVolume");
            Volume V = (Volume)GV.GetComponent("Volume");
            ColorAdjustments CA;
            V.profile.TryGet<ColorAdjustments>(out CA);
            CA.colorFilter.overrideState = true;
            CA.postExposure.overrideState = true;
        }
        else
        {
            GameObject GV = GameObject.FindGameObjectWithTag("GlobalVolume");
            Volume V = (Volume)GV.GetComponent("Volume");
            ColorAdjustments CA;
            V.profile.TryGet<ColorAdjustments>(out CA);
            CA.colorFilter.overrideState = false;
            CA.postExposure.overrideState = false;
        }

        IEnumerator Stun(float StunTime)
        {
            PC.Stunned = true;
            yield return new WaitForSeconds(StunTime);
            PC.Stunned = false;
        }
        IEnumerator Die()
        {
            ANM.SetBool("Dead", true);
            BC.enabled = false;
            yield return new WaitForSeconds(1f);
            BC.enabled = true;
            ANM.SetBool("Dead", false);
            RB.linearVelocity = new Vector2(0, 0);
            transform.position = Spawn.transform.position;
            Health = MaxHealth;
            GH.HealthChanged(Health);
            GH.Died();
        }
        GH.HealthChanged(Health);
    }
   public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
        gameObject.transform.position = Spawn.transform.position;
    }
}