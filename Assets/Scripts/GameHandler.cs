using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public int GemsCollected;

    public void GemCollected(int Value)
    {
        GemsCollected += Value;
        Debug.Log($"Gems Collected: {GemsCollected}");
    }
}
