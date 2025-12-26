using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string gameState = "";
    public static int totalScore = 0;   //合計スコア
    public static int addScore = 0;     //加算するスコア
    public static int zanki = 5;
    public static bool isChargeMax = false;  //ボムチャージ完了フラグ
    public static int charge = 0;
    public GameObject scoreText;
    public GameObject zankiText;
    public float chargeSpan = 1;
    public GameObject specialPanel;
    public GameObject pausedPanel;
    public GameObject guidePanel;
    public Button pauseButton;
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;
    public GameObject arrow_L;
    public GameObject arrow_R;

    private int score = 0;
    private ChangeScene changeScene;
    private bool isMove = false;
    private string selectButton = "continue";
    private Transform[] chargeMeters = new Transform[6];
    private Animator[] animators = new Animator[6];
    private float delta = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 59;
        changeScene = this.GetComponent<ChangeScene>();
        chargeMeters = specialPanel.GetComponentsInChildren<Transform>();
        animators = specialPanel.GetComponentsInChildren<Animator>();
        foreach (Transform meter in chargeMeters)
        {
            meter.gameObject.SetActive(false);
        }
        gameState = "playing";
        InitPosition();
        pausedPanel.SetActive(false);
        totalScore = 0;
        addScore = 0;
        scoreText.GetComponent<Text>().text = score.ToString("000000");
        zanki = 5;
        zankiText.GetComponent<Text>().text = zanki.ToString("00");
        charge = 0;
        isChargeMax = false;
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
            foreach (Animator animator in animators)
            {
                animator.speed = 0;
            }

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
        else
        {
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
            }

            if (addScore != 0)
            {
                score += addScore;
                totalScore += addScore;
                addScore = 0;
                if (score > 999999)
                {
                    zanki++;
                    score = 0;
                }
                scoreText.GetComponent<Text>().text = score.ToString("000000");
            }

            if (charge < 6)
            {
                delta += Time.deltaTime;
                if (delta >= chargeSpan)
                {
                    delta = 0;
                    charge++;
                }
            }
            else
            {
                isChargeMax = true;
                foreach (Animator animator in animators)
                {
                    animator.Play("Blick");
                }
            }

            for (int i = 0; i <= charge; i++)
            {
                chargeMeters[i].gameObject.SetActive(true);
            }

            if (isChargeMax && PlayerController.isSpecial)
            {
                PlayerController.isSpecial = false;
                isChargeMax = false;
                charge = 0;
                foreach (Transform meter in chargeMeters)
                {
                    meter.gameObject.SetActive(false);
                }
                foreach (Animator animator in animators)
                {
                    animator.Play("Stop");
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
            guidePanel.SetActive(false);
            pauseButton.gameObject.SetActive(false);
        }
        else if (gameState == "paused")
        {
            gameState = "playing";
            pausedPanel.SetActive(false);
            guidePanel.SetActive(true);
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
