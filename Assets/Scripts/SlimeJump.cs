using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class SlimeJump : MonoBehaviour
{
    public Rigidbody2D RB;
    public Animator Anim;
    public Transform groundCheck;
    bool isGrounded;
    private void Awake()
    {
        WaitForGround();
    }

    void Update()
    {
        if (isGrounded == true)
        {
            Jump(5);
        }
    }

    IEnumerator Jump(float Time)
    {
        yield return new WaitForSeconds(5f);
        Anim.SetBool("Jumping", true);
        yield return new WaitForSeconds(0.4f);
        RB.AddForce(transform.up * 8, ForceMode2D.Impulse);
        WaitForGround();
    }

    IEnumerator WaitForGround()
    {
        while (isGrounded == false)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheck.localScale, 0f);
            bool Found = false;
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
                Debug.Log("hit");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
