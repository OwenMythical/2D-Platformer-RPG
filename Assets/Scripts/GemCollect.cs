using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public int GemValue;
    public GameHandler GH;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GH.GemCollected(GemValue);
            gameObject.SetActive(false);
        }
    }
}
