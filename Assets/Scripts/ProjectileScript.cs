using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public BoxCollider2D Collider;
    public SpriteRenderer Renderer;
    HealthManager HM;
    bool Cooldown;

    void Awake()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        HM = (HealthManager)Player.GetComponent("HealthManager");
    }

    public IEnumerator Fired(Vector3 StartPos,float Height,float Speed)
    {
        gameObject.transform.position = StartPos;
        Renderer.enabled = true;
        Collider.enabled = true;
        RB.linearVelocity = new Vector2(Speed, Height);
        RB.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(2f);
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        Renderer.enabled = false;
        Collider.enabled = false;
    }

    void Update()
    {
        float angleRadians = Mathf.Atan2(RB.linearVelocityY,RB.linearVelocityX);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Cooldown == false)
            {
                StartCoroutine(Wait(0.5f));
                HM.TakeDamage(1, false, true, transform.position);
                Renderer.enabled = false;
                Collider.enabled = false;
                RB.constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
        if (collision.gameObject.name == "Collide")
        {
            Renderer.enabled = false;
            Collider.enabled = false;
            RB.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    IEnumerator Wait(float Time)
    {
        Cooldown = true;
        yield return new WaitForSeconds(Time);
        Cooldown = false;
    }
}
