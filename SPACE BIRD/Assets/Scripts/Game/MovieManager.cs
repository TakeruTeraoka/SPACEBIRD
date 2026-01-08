using UnityEngine;

public class MovieManager : MonoBehaviour
{
    private ChangeScene changeScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeScene = GetComponent<ChangeScene>();
        changeScene.SceneName = "Stage1";
        changeScene.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
