using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string gameState = "";
    public static int totalScore = 0;   //合計スコア
    public static int addScore = 0;     //加算するスコア
    public static int zanki = 5;        //残機
    public static int addZanki = 0;     //加算する残機（マイナスを含む）
    public static bool isChargeMax = false;  //ボムチャージ完了フラグ
    public static int charge = 0;   //ボムチャージ
    public float chargeSpan = 1;    //チャージ間隔

    public GameObject scoreText;
    public GameObject zankiText;
    public GameObject specialPanel;
    public GameObject pausedPanel;
    public GameObject guidePanel;
    public Button pauseButton;
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;
    public GameObject arrow_L;
    public GameObject arrow_R;

    private int score = 0;  //スコア
    private float delta = 0;    //加算用変数
    private bool isMove = false;    //カーソル移動フラグ
    private string selectButton = "continue";   //選択されているボタン

    private ChangeScene changeScene;
    private Transform[] chargeMeters = new Transform[6];
    private Animator[] animators = new Animator[6];

    private void Start()
    {
        Application.targetFrameRate = 59;
        changeScene = this.GetComponent<ChangeScene>();
        chargeMeters = specialPanel.GetComponentsInChildren<Transform>();
        animators = specialPanel.GetComponentsInChildren<Animator>();
        foreach (Transform meter in chargeMeters)
        {
            meter.gameObject.SetActive(false);
        }
        PlayerController.playerState = "alive";
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
        //通常画面からポーズ画面に切り替える
        if (Input.GetButtonDown("Cancel"))
        {
            InitPosition();
            SwitchPanel();
        }

        //ポーズ処理
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
        //通常処理
        else
        {
            //アニメーション速度を等倍に戻す
            foreach (Animator animator in animators)
            {
                animator.speed = 1;
            }

            //スコアの増加
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


            //残機の増減
            if (addZanki != 0)
            {
                if (zanki < 99) zanki += addZanki;
                addZanki = 0;

                zankiText.GetComponent<Text>().text = zanki.ToString("00");
            }

            //ボムチャージ
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

    //パネル切替
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

    //カーソルの位置の初期化
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

    //ゲームを続ける
    public void ContinueGame()
    {
        InitPosition();
    }

    //ゲームをリスタート
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

    //タイトルに戻る
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
