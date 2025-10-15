using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    int GemsCollected;
    int SceneIndex = 1;
    public TMP_Text GemCounter;
    GameObject[] Gems;

    string[] Scenes = {"Scene1","Scene2","Scene3"};

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

    public void NextLevel()
    {
        SceneIndex += 1;
        SceneManager.LoadScene(Scenes[SceneIndex]);
    }
}