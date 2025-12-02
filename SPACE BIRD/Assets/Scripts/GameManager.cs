using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string gameState = "";
    public GameObject pausedPanel;
    public Button pauseButton;
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;
    public GameObject arrow_L;
    public GameObject arrow_R;

    private ChangeScene changeScene;
    private bool isMove = false;
    private string selectButton = "continue";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 59;
        changeScene = this.GetComponent<ChangeScene>();
        gameState = "playing";
        InitPosition();
        pausedPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            InitPosition();
            SwitchPanel();
        }

        if (gameState == "paused")
        {
            if (Input.GetAxisRaw("Vertical") != 0 && !isMove)
            {
                isMove = true;
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (arrow_L.GetComponent<RectTransform>().localPosition.y == continueButton.GetComponent<RectTransform>().localPosition.y)
                    {
                        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
                        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
                        selectButton = "exit";
                    }
                    else if (arrow_L.GetComponent<RectTransform>().localPosition.y == restartButton.GetComponent<RectTransform>().localPosition.y)
                    {
                        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
                        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
                        selectButton = "continue";
                    }
                    else
                    {
                        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          restartButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
                        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                                          restartButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
                        selectButton = "restart";
                    }
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    if (arrow_L.GetComponent<RectTransform>().localPosition.y == continueButton.GetComponent<RectTransform>().localPosition.y)
                    {
                        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          restartButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
                        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                                          restartButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
                        selectButton = "restart";
                    }
                    else if (arrow_L.GetComponent<RectTransform>().localPosition.y == restartButton.GetComponent<RectTransform>().localPosition.y)
                    {
                        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
                        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
                        selectButton = "exit";
                    }
                    else
                    {
                        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
                        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
                        selectButton = "continue";
                    }
                }
            }
            else if (Input.GetAxisRaw("Vertical") == 0)
            {
                isMove = false;
            }

            if (Input.GetButtonDown("Submit"))
            {
                switch (selectButton)
                {
                    case "continue":
                        SwitchPanel();
                        break;
                    case "restart":
                        RestartGame();
                        changeScene.SceneName = "Stage1";
                        changeScene.Load();
                        break;
                    case "exit":
                        ReturnTitle();
                        changeScene.SceneName = "Title";
                        changeScene.Load();
                        break;
                }
            }
        }
    }

    public void SwitchPanel()
    {
        if (gameState == "playing")
        {
            gameState = "paused";
            pausedPanel.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }
        else if (gameState == "paused")
        {
            gameState = "playing";
            pausedPanel.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }
    }

    public void InitPosition()
    {
        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
        selectButton = "continue";
    }

    public void ContinueGame()
    {
        InitPosition();
    }

    public void RestartGame()
    {
        gameState = "playing";
        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          restartButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                          restartButton.GetComponent<RectTransform>().localPosition.y,
                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
        selectButton = "restart";
    }

    public void ReturnTitle()
    {
        gameState = "";
        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
        selectButton = "exit";
    }
}
