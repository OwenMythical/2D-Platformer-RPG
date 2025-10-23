using Unity.VisualScripting;
using UnityEngine;

public class CheckpointChanger : MonoBehaviour
{
    GameObject Spawn;
    private void Awake()
    {
        Spawn = GameObject.FindGameObjectWithTag("Spawn");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Spawn.transform.position = gameObject.transform.position;
        }
    }
}
