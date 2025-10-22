using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    int GemsCollected = 5;
    int SceneIndex = 1;
    public TMP_Text GemCounter;
    public TMP_Text HealthCounter;
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
        if (GemsCollected < 5)
        {
            GemCounter.color = new Color(1,0,0);
        }
        else
        {
            GemCounter.color = new Color(1, 0.8f, 1);
        }
    }

    public void Died()
    {
        if (GemsCollected < 5)
        {
            SceneManager.LoadScene("Menu");
            Destroy(gameObject);
        }
        else if (GemsCollected < 10)
        {
            GemsCollected = 0;
            GemCounter.color = new Color(1, 0, 0);
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

    public void HealthChanged(int Health)
    {
        HealthCounter.text = $"Health: {Health}/5";
    }
}