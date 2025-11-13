using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour
{
    public SpriteRenderer SRenderer;
    public int Health;
    public int Score;
    GameHandler GH;
    int MHealth;

    void Awake()
    {
        GameObject HandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        GH = (GameHandler)HandlerObject.GetComponent("GameHandler");
        MHealth = Health;
    }
    public IEnumerator TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            GH.AddScore(Score);
            Destroy(gameObject);
            if (gameObject.name == "Wizard")
            {
                yield return new WaitForSeconds(1f);
                GH.BossDefeat();
            }
        }
        else
        {
            SRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            SRenderer.color = new Color(1f, 1f, 1f);
        }
    }

    public void Heal()
    {
        Health = MHealth;
    }
}
