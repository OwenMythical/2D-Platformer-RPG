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
            //Anim.SetBool("Aiming", true);
            Aggro += 1;
            if (Aggro > 2000)
            {
                Aggro = 0;
                StartCoroutine(Fire());
            }
        }
        else
        {
            //Anim.SetBool("Aiming", false);
            Aggro -= 1;
        }
    }

    IEnumerator Fire()
    {
        //Anim.SetBool("Fired", true);
        float DistanceX = (Player.position.x - transform.position.x) * ((float)RNG.Next(9,16)/10);
        StartCoroutine(ArrowS.Fired(gameObject.transform.position,DistanceX, RNG.Next(7,9)));
        yield return new WaitForSeconds(0.5f);
        //Anim.SetBool("Fired", false);
    }
}
