using UnityEngine;
using System.Collections;

public class ArcherScript : MonoBehaviour
{
    public GameObject Arrow;
    public Rigidbody2D ArrowRB;
    System.Random RNG = new();
    public Rigidbody2D RB;
    public Animator Anim;
    public BoxCollider2D Collider;
    public SpriteRenderer SRenderer;
    public Transform AggroColl;
    int Aggro = 0;

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

        if (Found == true)
        {
            Aggro += 1;
        }
        else
        {
            Aggro -= 1;
        }
    }

    IEnumerator Fire()
    {
        Anim.SetBool("AggroStart", true);
        yield return new WaitForSeconds(0.5f);
        Anim.SetBool("Chasing", true);
        Anim.SetBool("AggroStart", false);
    }
}
