using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;

    public void Load()
    {
        SceneManager.LoadScene(SceneName);
    }
}
