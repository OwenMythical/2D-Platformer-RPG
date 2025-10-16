using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using System;

public class SlimeJump : MonoBehaviour
{
    public Rigidbody2D RB;
    public Animator Anim;
    public Transform groundCheck;
    public Transform Player;
    public BoxCollider2D Collider;
    bool isGrounded = true;
    private void Awake()
    {
        
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
        yield return new WaitForSeconds(5f);
        Anim.SetBool("Jumping", true);
        yield return new WaitForSeconds(1);
        Collider.excludeLayers = LayerMask.GetMask("Player");
        RB.AddForce(transform.up * 8, ForceMode2D.Impulse);
        float DistanceX = Player.position.x - transform.position.x;
        float DistanceY = Player.position.y - transform.position.y;
        if (Math.Abs(DistanceX) < 5 && Math.Abs(DistanceY) < 5)
        {
            RB.AddForce(transform.right * DistanceX, ForceMode2D.Impulse);
            if (DistanceX < 0)
            {
            }
            else if (DistanceX > 0)
            {
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
                Anim.SetBool("Jumping", false);
                Collider.excludeLayers = LayerMask.GetMask("Nothing");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
