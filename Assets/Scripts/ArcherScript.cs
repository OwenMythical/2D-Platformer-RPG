using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ArcherScript : MonoBehaviour
{
    public GameObject Arrow;
    ProjectileScript ArrowS;
    Transform Player;
    public Rigidbody2D ArrowRB;
    System.Random RNG = new();
    public Rigidbody2D RB;
    public Animator Anim;
    public BoxCollider2D Collider;
    public SpriteRenderer SRenderer;
    public Transform AggroColl;
    bool Aggroing = false;

    private void Awake()
    {
        ArrowS = (ProjectileScript)Arrow.GetComponent("ProjectileScript");
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

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

        if (Found == true && Aggroing == false)
        {
            Aggroing = true;
            StartCoroutine(AggroStart());
        }
        if (Found == true)
        {
            float DistanceX = (Player.position.x - transform.position.x);
            if (DistanceX < 0)
            {
                SRenderer.flipX = true;
            }
            else if (DistanceX > 0)
            {
                SRenderer.flipX = false;
            }
        }
    }

    IEnumerator Fire()
    {
        float DistanceX = (Player.position.x - transform.position.x) * ((float)RNG.Next(13,18)/10);
        float DistanceY = (Player.position.y - transform.position.y) * ((float)RNG.Next(13,18)/10);
        StartCoroutine(ArrowS.Fired(gameObject.transform.position,DistanceY+7,DistanceX));
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator AggroStart()
    {
        yield return new WaitForSeconds(2.5f);

        bool Found2 = false;
        Collider2D[] colliders2 = Physics2D.OverlapBoxAll(AggroColl.position, AggroColl.localScale, 0f);
        foreach (Collider2D collider in colliders2)
        {
            if (collider.gameObject.name == "Player")
            {
                Found2 = true;
                break;
            }
        }

        if (Found2 == true)
        {
            Anim.SetBool("Aiming", true);
            yield return new WaitForSeconds(1f);

            bool Found3 = false;
            Collider2D[] colliders3 = Physics2D.OverlapBoxAll(AggroColl.position, AggroColl.localScale, 0f);
            foreach (Collider2D collider in colliders3)
            {
                if (collider.gameObject.name == "Player")
                {
                    Found3 = true;
                    break;
                }
            }

            if (Found3 == true)
            {
                StartCoroutine(Fire());
            }
            Anim.SetBool("Aiming", false);
            Aggroing = false;
        }
        else
        {
            Anim.SetBool("Aiming", false);
            Aggroing = false;
        }
    }
}
