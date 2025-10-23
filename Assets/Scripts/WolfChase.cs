using System.Collections;
using UnityEngine;

public class WolfChase : MonoBehaviour
{
    public Rigidbody2D RB;
    public Animator Anim;
    public BoxCollider2D Collider;
    public SpriteRenderer SRenderer;
    public Transform AggroColl;
    bool Chasing = false;
    int Boredom = 0;

    void Update()
    {
        bool Found = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(AggroColl.position, AggroColl.localScale, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.name == "Player")
            {
                Found = true;
                break;
            }
        }
        gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        if (Chasing == false)
        {
            if (Found == true)
            {
                StartCoroutine(Chase());
            }
            else
            {
                Boredom += 1;
                if (Boredom > 1500)
                {
                    Boredom = 0;
                    gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, 1, 1);
                }
            }
        }
        else
        {
            if (Found == true)
            {
                RB.constraints = RigidbodyConstraints2D.None;
                RB.linearVelocity = new Vector2(gameObject.transform.localScale.x * -3, RB.linearVelocity.y);
            }
            else
            {
                Chasing = false;
                Anim.SetBool("Chasing", false);
                RB.constraints = RigidbodyConstraints2D.FreezePositionX;
                RB.linearVelocity = new Vector2(0, RB.linearVelocity.y);
            }
        }
    }

    IEnumerator Chase()
    {
        Anim.SetBool("AggroStart", true);
        yield return new WaitForSeconds(0.5f);
        Chasing = true;
        Anim.SetBool("Chasing", true);
        Anim.SetBool("AggroStart", false);
    }
}
