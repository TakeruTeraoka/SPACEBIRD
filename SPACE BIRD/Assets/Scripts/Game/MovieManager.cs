using UnityEngine;

public class MovieManager : MonoBehaviour
{
    public Camera mainCamera;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 59;
        GameManager.gameState = "playing";
        animator = mainCamera.GetComponent<Animator>();
        animator.Play("Movie_st1");
    }

    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<ChangeScene>().SceneName = "Stage1";
            GetComponent<ChangeScene>().Load();
        }
    }
}
