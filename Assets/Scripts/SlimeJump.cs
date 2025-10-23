using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SlimeJump : MonoBehaviour
{
    System.Random RNG = new();
    public Rigidbody2D RB;
    public Animator Anim;
    public Transform groundCheck;
    Transform Player;
    public BoxCollider2D Collider;
    public SpriteRenderer SRenderer;
    bool isGrounded = true;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isGrounded == true)
        {
            isGrounded = false;
            StartCoroutine(Jump());
        }
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds((float)RNG.Next(30,40)/10);
        Anim.SetBool("Jumping", true);
        yield return new WaitForSeconds(1f);
        RB.constraints = RigidbodyConstraints2D.FreezeRotation;
        Collider.excludeLayers = LayerMask.GetMask("Characters");
        RB.AddForce(transform.up * 10, ForceMode2D.Impulse);
        float DistanceX = Player.position.x - transform.position.x;
        float DistanceY = Player.position.y - transform.position.y;
        if (Math.Abs(DistanceX) < 5 && Math.Abs(DistanceY) < 5)
        {
            RB.AddForce(transform.right * DistanceX, ForceMode2D.Impulse);
            if (DistanceX < 0)
            {
                SRenderer.flipX = false;
            }
            else if (DistanceX > 0)
            {
                SRenderer.flipX = true;
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(WaitForGround());
    }

    IEnumerator WaitForGround()
    {
        bool Found = false;
        while (Found == false)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheck.localScale, 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.name == "Collide")
                {
                    Found = true;
                    break;
                }
            }
            if (Found == true)
            {
                isGrounded = true;
                RB.constraints = RigidbodyConstraints2D.FreezeAll;
                Anim.SetBool("Jumping", false);
                Collider.excludeLayers = LayerMask.GetMask("Nothing");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
