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
        float angleRadians = Mathf.Atan2(RB.linearVelocityY,RB.linearVelocityX);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
    }
}
