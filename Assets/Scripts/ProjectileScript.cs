using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public BoxCollider2D Collider;
    public SpriteRenderer Renderer;

    public IEnumerator Fired(Vector3 StartPos,float Height,float Speed)
    {
        gameObject.transform.position = StartPos;
        Renderer.enabled = true;
        Collider.enabled = true;
        if (Height >= 0)
        {
            RB.linearVelocity = new Vector2(Speed, Height);
        }
        else
        {
            RB.linearVelocity = new Vector2(-Speed, -Height);
        }
        RB.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(2f);
        RB.constraints = RigidbodyConstraints2D.FreezePosition;
        Renderer.enabled = false;
        Collider.enabled = false;
    }

    void Update()
    {
        //gameObject.transform.rotation = Quaternion.LookRotation();
    }
}
