using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public int GemValue;
    GameHandler GH;

    void Awake()
    {
        GameObject HandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        GH = (GameHandler)HandlerObject.GetComponent("GameHandler");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GH.GemCollected(GemValue);
            GH.AddScore(GemValue*50);
            gameObject.SetActive(false);
        }
    }
}
