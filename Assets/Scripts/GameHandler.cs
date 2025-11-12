using Platformer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    int GemsCollected = 10;
    int SceneIndex = 0;
    int Score = 0;
    public TMP_Text GemCounter;
    public TMP_Text HealthCounter;
    public TMP_Text ScoreCounter;
    GameObject[] Gems;
    HealthManager PlayerM;

    string[] Scenes = {"Level1","Level2","Level3"};

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
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerM = (HealthManager)PlayerObj.GetComponent("HealthManager");
    }

    public void GemCollected(int Value, int LifeValue)
    {
        GemsCollected += Value;
        GemCounter.text = $"Gems: {GemsCollected}";
        if (GemsCollected < 10)
        {
            GemCounter.color = new Color(1,0,0);
        }
        else
        {
            GemCounter.color = new Color(1, 0.8f, 1);
        }
        if (LifeValue > 0)
        {
            PlayerM.TakeDamage(-LifeValue,false,false,new Vector3(0,0,0));
        }
    }

    public void Died()
    {
        if (GemsCollected < 10)
        {
            SceneManager.LoadScene("Menu");
            Destroy(gameObject);
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            Destroy(Player);
        }
        else if (GemsCollected < 25)
        {
            GemsCollected = 0;
            GemCounter.color = new Color(1, 0, 0);
        }
        else
        {
            GemsCollected /= 5;
            if (GemsCollected < 10)
            {
                GemCounter.color = new Color(1, 0, 0);
            }
        }
        GemCounter.text = $"Gems: {GemsCollected}";
    }
    public void NextLevel()
    {
        SceneIndex += 1;
        SceneManager.LoadScene(Scenes[SceneIndex]);
        StartCoroutine(PlayerM.Respawn());
    }

    public void HealthChanged(int Health)
    {
        HealthCounter.text = $"Health: {Health}/10";
    }

    public void AddScore(int ExtraScore)
    {
        Score += ExtraScore;
        ScoreCounter.text = $"Score: {Score}";
    }
}