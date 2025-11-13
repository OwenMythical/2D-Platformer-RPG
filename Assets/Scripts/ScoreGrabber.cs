using TMPro;
using UnityEngine;

public class ScoreGrabber : MonoBehaviour
{
    GameHandler GH;
    public TMP_Text ScoreDisplay;

    void Start()
    {
        GameObject HandlerObject = GameObject.FindGameObjectWithTag("GameHandler");
        GH = (GameHandler)HandlerObject.GetComponent("GameHandler");

        int Score = GH.Score;
        if (Score < 7500)
        {
            ScoreDisplay.text = $"Final Score: {Score} (Literally Impossible???)";
        }
        else if (Score < 10000)
        {
            ScoreDisplay.text = $"Final Score: {Score} (Not That Great...)";
        }
        else if (Score < 15000)
        {
            ScoreDisplay.text = $"Final Score: {Score} (Good Enough.)";
        }
        else if (Score < 20000)
        {
            ScoreDisplay.text = $"Final Score: {Score} (Decent!)";
        }
        else if (Score >= 20000)
        {
            ScoreDisplay.text = $"Final Score: {Score} (Amazing!)";
        }
    }
}
