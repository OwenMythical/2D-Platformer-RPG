using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour
{
    public SpriteRenderer SRenderer;
    public int Health;
    public IEnumerator TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            SRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            SRenderer.color = new Color(1f, 1f, 1f);
        }
    }
}
