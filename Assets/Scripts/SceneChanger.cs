using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string Name)
    {
        SceneManager.LoadScene(Name);
    }
}
