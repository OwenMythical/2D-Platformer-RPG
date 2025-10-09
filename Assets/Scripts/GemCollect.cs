using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public GameHandler GH;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GH.GemCollected(1);
            gameObject.SetActive(false);
        }
    }
}
