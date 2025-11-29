using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;    //読み込むシーン名

    //シーンを読み込む
    public void Load()
    {
        SceneManager.LoadScene(SceneName);
    }
}
