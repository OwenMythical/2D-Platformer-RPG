using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    int GemsCollected;
    int SceneIndex = 1;
    public TMP_Text GemCounter;
    GameObject[] Gems;

    string[] Scenes = {"Level1"};

    public static GameHandler instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GemCollected(int Value)
    {
        GemsCollected += Value;
        GemCounter.text = $"Gems: {GemsCollected}";
    }

    public void Died()
    {
        if (GemsCollected == 0)
        {
            SceneManager.LoadScene("Menu");
            Destroy(gameObject);
        }
        if (GemsCollected <= 10)
        {
            GemsCollected = 0;
        }
        else
        {
            GemsCollected /= 2;
        }
        GemCounter.text = $"Gems: {GemsCollected}";
    }

    public void NextLevel()
    {
        SceneIndex += 1;
        SceneManager.LoadScene(Scenes[SceneIndex]);
    }
}