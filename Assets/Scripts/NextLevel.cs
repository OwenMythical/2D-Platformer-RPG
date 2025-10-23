using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
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
            GH.NextLevel();
        }
    }
}
