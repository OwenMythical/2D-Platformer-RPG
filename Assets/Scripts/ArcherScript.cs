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
    int Aggro = 0;

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
            if (Aggro > 1250)
            {
                Anim.SetBool("Aiming", true);
            }
            Aggro += 1;
            if (Aggro > 1750)
            {
                Aggro = 0;
                Anim.SetBool("Aiming", false);
                StartCoroutine(Fire());
            }
        }
        else
        {
            Anim.SetBool("Aiming", false);
            Aggro -= 1;
            if (Aggro < 0)
            {
                Aggro = 0;
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
}
