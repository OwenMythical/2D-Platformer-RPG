using TMPro;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public int GemsCollected;
    public TMP_Text GemCounter;

    public void GemCollected(int Value)
    {
        GemsCollected += Value;
        GemCounter.text = $"Gems: {GemsCollected}";
    }
}
