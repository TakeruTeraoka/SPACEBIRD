using UnityEngine;
using UnityEngine.UI;

public class MovieManager : MonoBehaviour
{
    public Camera mainCamera;
    public Button skipButton;

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
        if (Input.GetButtonDown("Cancel"))
        {
            SkipButtonClick();
        }
    }

    public void SkipButtonClick()
    {
        skipButton.GetComponent<Animator>().Play("Press");
    }
}
