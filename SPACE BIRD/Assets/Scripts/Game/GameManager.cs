using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string gameState = "";    //ゲームの状態
    public static int totalScore = 0;   //合計スコア
    public static int addScore = 0;     //加算するスコア
    public static int subBossHp = 0;    //減算するボスの体力
    public static int zanki = 5;        //残機
    public static int addZanki = 0;     //加算する残機（マイナスを含む）
    public static bool isChargeMax = false;  //ボムチャージ完了フラグ
    public static bool isScrollStop = false;    //スクロール停止フラグ
    public static int charge = 0;   //ボムチャージ
    public static int stageNum = 1; //ステージ番号

    public float chargeSpan = 1;    //チャージ間隔

    //UIパネル・ボタン・矢印・スコアカウントクラス
    public GameObject scrollStopWall;
    public GameObject scoreText;
    public GameObject zankiText;
    public GameObject specialPanel;
    public GameObject pausedPanel;
    public GameObject gameOCPanel;
    public GameObject gameOCEntryPanel;
    public GameObject gameOCButtonPanel;
    public GameObject stageClearPanel;
    public GameObject guidePanel;
    public GameObject gameOCNames;
    public GameObject gameOCHiscoreText;
    public Button pauseButton;
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;
    public Button gameOCContinueButton;
    public Button gameOCRestartButton;
    public Button gameOCExitButton;
    public Button stageClearNextButton;
    public GameObject gameOCBack_Key;
    public GameObject gameOCBack_Pad;
    public GameObject arrow_L;
    public GameObject arrow_R;
    public GameObject gameOCArrow_L;
    public GameObject gameOCArrow_R;
    public CountScore countScore;
    public Slider bossHpMeter;

    public string nextSceneName = "";   //次にロードされるシーン名
    public bool isDebug = false;    //デバッグ切替用
    public string currentStage = ""; //現在のステージ名
    public bool isScrollStopOn = false; //ステージのスクロール停止のオンオフ管理フラグ

    private GameObject boss;
    private int score = 0;  //スコア
    private float delta = 0;    //加算用変数

    //カーソル移動フラグ
    private bool isMove = false;
    private bool isGameOCMove = false;

    private bool isGameOCButtonSwitched = false;    //ゲームオーバー・クリアパネルのボタン表示切替フラグ
    private bool isStageClearPanelSwitched = false; //ステージクリアパネル表示切替フラグ
    private string selectButton = "continue";   //選択されているボタン
    private string gameOCSelectButton = "continue"; //ゲームオーバー・クリアパネルの選択されているボタン
    private bool isBossHpMeterMax = false;  //ボスの体力メーターが満タンになったかを管理するフラグ

    private ChangeScene changeScene;    //シーン切替フラグ
    private Transform[] chargeMeters = new Transform[7];    //ボムチャージメーター×６（取得する際、親オブジェクトも同時に取得するため７枠）
    private Animator[] animators = new Animator[7]; //ボムチャージメーターのアニメーション×６（取得する際、親オブジェクトも同時に取得するため７枠）
    private Transform[] names = new Transform[7];   //ゲームオーバー・クリアパネルの名前登録部分のボタン×３（取得する際、親オブジェクトも同時に取得するため７枠）

    //名前登録に使用する文字配列
    private string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    private int[] namesChar = { 0, 0, 0 };  //各ボタンの文字配列のインデックスの管理用配列

    //ハイスコア情報を管理するための構造体
    private struct Hiscore
    {
        public string name;
        public int score;
    }

    private void Start()
    {
        //ゲームのフレームレートを59fpsに固定（※WebGLでビルドする際に60fpsで固定しようとするとfps固定がスキップされるので注意※）
        Application.targetFrameRate = 59;

        //ゲームオブジェクトからコンポーネントを取得（thisはこのスクリプトがアタッチされているオブジェクトから）
        changeScene = this.GetComponent<ChangeScene>();
        chargeMeters = specialPanel.GetComponentsInChildren<Transform>();
        animators = specialPanel.GetComponentsInChildren<Animator>();
        names = gameOCNames.GetComponentsInChildren<Transform>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossHpMeter.gameObject.SetActive(false);

        //ボムチャージメーターをすべて非表示に設定
        foreach (Transform meter in chargeMeters)
        {
            meter.gameObject.SetActive(false);

        }
        PlayerController.playerState = "alive";
        gameState = "playing";
        currentStage = SceneManager.GetActiveScene().name;
        pausedPanel.SetActive(false);
        gameOCPanel.SetActive(false);
        gameOCButtonPanel.SetActive(false);
        stageClearPanel.SetActive(false);
        addScore = 0;
        scoreText.GetComponent<Text>().text = score.ToString("000000");
        zankiText.GetComponent<Text>().text = zanki.ToString("00");
    }

    private void Update()
    {
        //デバッグ用
        DebugFunc(isDebug);

        //通常画面からポーズ画面に切り替える
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchPanel();
        }

        switch (gameState)
        {
            case "playing":
                GamePlay();
                break;
            case "paused":
                GamePause();
                break;
            case "gameover":
            case "gameclear":
                GameOver();
                break;
            case "stageclear":
                if (!isStageClearPanelSwitched)
                {
                    isStageClearPanelSwitched = true;
                    SwitchPanel();
                }
                StageClear();
                break;
        }

        if (boss.GetComponent<Renderer>().isVisible)
        {
            if (!isBossHpMeterMax)
            {
                bossHpMeter.gameObject.SetActive(true);
                StartCoroutine(InitBossHpMeter());
            }
            else if (isBossHpMeterMax && subBossHp != 0 && bossHpMeter.value >= bossHpMeter.minValue)
            {
                ChangeBossHpMeter();
            }
        }
    }

    private void GamePlay()
    {
        if (scrollStopWall.GetComponent<Transform>().position.x > -9.01f && isScrollStopOn)
        {
            isScrollStop = true;
        }

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
            if (zanki < 99 && zanki >= 1) zanki += addZanki;
            else if (zanki <= 0)
            {
                gameState = "gameover";
                SwitchPanel();
                foreach (Animator animator in animators)
                {
                    animator.speed = 0;
                }
            }
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

    //ポーズ処理
    private void GamePause()
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
                switch (selectButton)
                {
                    case "continue":
                        ReturnTitle();
                        break;
                    case "restart":
                        ContinueGame();
                        break;
                    default:
                        RestartGame();
                        break;
                }
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                switch (selectButton)
                {
                    case "continue":
                        RestartGame();
                        break;
                    case "restart":
                        ReturnTitle();
                        break;
                    default:
                        ContinueGame();
                        break;
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
                    isScrollStop = false;
                    if (zanki < 5)
                    {
                        zanki = 5;
                    }
                    addScore = 0;
                    subBossHp = 0;
                    changeScene.SceneName = currentStage;
                    changeScene.Load();
                    break;
                case "exit":
                    InitGame();
                    changeScene.SceneName = "Title";
                    changeScene.Load();
                    break;
            }
        }
    }

    //ゲームオーバー処理
    private void GameOver()
    {
        foreach (Animator animator in animators)
        {
            animator.speed = 0;
        }

        if (!isGameOCButtonSwitched)
        {
            if ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && !isGameOCMove)
            {
                isGameOCMove = true;
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    if (gameOCSelectButton == "next")
                    {
                        GameOCName0();
                    }
                    else
                    {
                        switch (gameOCSelectButton)
                        {
                            case "name0":
                                SwitchName(0);
                                break;
                            case "name1":
                                SwitchName(1);
                                break;
                            case "name2":
                                SwitchName(2);
                                break;
                        }
                    }
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && gameOCSelectButton != "next")
                {
                    switch (gameOCSelectButton)
                    {
                        case "name0":
                            ReSwitchName(0);
                            break;
                        case "name1":
                            ReSwitchName(1);
                            break;
                        case "name2":
                            ReSwitchName(2);
                            break;
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    switch (gameOCSelectButton)
                    {
                        case "name0":
                            GameOCName1();
                            break;
                        case "name1":
                            GameOCName2();
                            break;
                        case "name2":
                            GameOCNext();
                            break;
                    }
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    switch (gameOCSelectButton)
                    {
                        case "name1":
                            GameOCName0();
                            break;
                        case "name2":
                            GameOCName1();
                            break;
                        case "next":
                            GameOCName2();
                            break;
                    }
                }
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            {
                isGameOCMove = false;
            }

            if (Input.GetButtonDown("Submit"))
            {
                switch (gameOCSelectButton)
                {
                    case "name0":
                        GameOCName1();
                        break;
                    case "name1":
                        GameOCName2();
                        break;
                    case "name2":
                        GameOCNext();
                        break;
                    case "next":
                        SwitchGameOCButton();
                        break;
                }
            }
            else if (Input.GetButtonDown("Back"))
            {
                switch (gameOCSelectButton)
                {
                    case "name1":
                        GameOCName0();
                        break;
                    case "name2":
                        GameOCName1();
                        break;
                    case "next":
                        GameOCName2();
                        break;
                }
            }
        }
        else
        {
            gameOCBack_Key.SetActive(false);
            gameOCBack_Pad.SetActive(false);

            if (Input.GetAxisRaw("Vertical") != 0 && !isGameOCMove)
            {
                isGameOCMove = true;
                if (Input.GetAxisRaw("Vertical") > 0)
                {
                    switch (gameOCSelectButton)
                    {
                        case "continue":
                            GameOCReturnTitle();
                            break;
                        case "restart":
                            GameOCContinueGame();
                            break;
                        case "exit":
                            GameOCRestartGame();
                            break;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") < 0)
                {
                    switch (gameOCSelectButton)
                    {
                        case "continue":
                            GameOCRestartGame();
                            break;
                        case "restart":
                            GameOCReturnTitle();
                            break;
                        case "exit":
                            GameOCContinueGame();
                            break;
                    }
                }
            }
            else if (Input.GetAxisRaw("Vertical") == 0)
            {
                isGameOCMove = false;
            }

            if (Input.GetButtonDown("Submit"))
            {
                switch (gameOCSelectButton)
                {
                    case "continue":
                        isScrollStop = false;
                        zanki = 5;
                        addScore = 0;
                        subBossHp = 0;
                        changeScene.SceneName = currentStage;
                        break;

                    case "restart":
                        InitGame();
                        changeScene.SceneName = "Stage1";
                        break;

                    case "exit":
                        InitGame();
                        changeScene.SceneName = "Title";
                        break;
                }
                changeScene.Load();
            }
        }
    }

    //ステージクリア処理
    private void StageClear()
    {
        if (Input.GetButtonDown("Submit"))
        {
            StageClearNext();
        }
    }

    //パネル切替
    public void SwitchPanel()
    {
        if (gameState == "playing")
        {
            InitPosition();
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
        else if (gameState == "gameover")
        {
            gameOCPanel.SetActive(true);
            guidePanel.SetActive(false);
            InitGameOCPosition();
            gameOCHiscoreText.GetComponent<Text>().text = totalScore.ToString("00000000");
            gameOCPanel.GetComponent<Animator>().Play("FadeIn");
        }
        else if (gameState == "gameclear")
        {
            gameOCPanel.SetActive(true);
            guidePanel.SetActive(false);
            InitGameOCPosition();
            gameOCHiscoreText.GetComponent<Text>().text = totalScore.ToString("00000000");
            gameOCPanel.GetComponent<Animator>().Play("FadeInClear");
        }
        else if (gameState == "stageclear")
        {
            stageClearPanel.SetActive(true);
            guidePanel.SetActive(false);
            stageClearPanel.GetComponent<Animator>().Play("SlideIn");
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
        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                      continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                                      arrow_L.GetComponent<RectTransform>().localPosition.z);
        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                          continueButton.GetComponent<RectTransform>().localPosition.y,
                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
        selectButton = "continue";
    }

    //ゲームをリスタート
    public void RestartGame()
    {
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
        arrow_L.GetComponent<RectTransform>().localPosition = new Vector3(arrow_L.GetComponent<RectTransform>().localPosition.x,
                                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                                          arrow_L.GetComponent<RectTransform>().localPosition.z);
        arrow_R.GetComponent<RectTransform>().localPosition = new Vector3(arrow_R.GetComponent<RectTransform>().localPosition.x,
                                                                          exitButton.GetComponent<RectTransform>().localPosition.y,
                                                                          arrow_R.GetComponent<RectTransform>().localPosition.z);
        selectButton = "exit";
    }

    //セーブ処理
    public void SaveGame()
    {
        Hiscore[] hiscores = new Hiscore[4];
        Hiscore temp;

        hiscores[3].name = chars[namesChar[0]] + chars[namesChar[1]] + chars[namesChar[2]];
        hiscores[3].score = totalScore;

        for (int i = 0; i < 3; i++)
        {
            hiscores[i].name = PlayerPrefs.GetString("Name" + i.ToString());
            hiscores[i].score = PlayerPrefs.GetInt("Score" + i.ToString());
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = i + 1; j < 4; j++)
            {
                if (hiscores[i].score <= hiscores[j].score)
                {
                    temp = hiscores[i];
                    hiscores[i] = hiscores[j];
                    hiscores[j] = temp;
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetString("Name" + i.ToString(), hiscores[i].name);
            PlayerPrefs.SetInt("Score" + i.ToString(), hiscores[i].score);
        }
    }

    //static変数の初期化
    public void InitGame()
    {
        stageNum = 1;
        totalScore = 0;
        zanki = 5;
        charge = 0;
        isChargeMax = false;
        isScrollStop = false;
    }

    //ゲームオーバー・クリアパネルのボタン切替処理
    public void SwitchGameOCButton()
    {
        SaveGame();
        if (gameState == "gameover")
        {
            gameOCEntryPanel.SetActive(false);
            gameOCButtonPanel.SetActive(true);
            isGameOCButtonSwitched = true;
            InitGameOCPosition();
        }
        else if (gameState == "gameclear")
        {
            InitGame();
            changeScene.SceneName = "Title";
            changeScene.Load();
            Debug.Log("Playing Movie_en");
        }
    }

    //ゲームオーバー・クリアパネルのカーソルの位置の初期化処理
    public void InitGameOCPosition()
    {
        if (!isGameOCButtonSwitched)
        {
            GameOCName0();
        }
        else
        {
            gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(-232f,
                                                                          25f,
                                                                          gameOCArrow_L.GetComponent<RectTransform>().localPosition.z);
            gameOCArrow_L.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
            gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(229f,
                                                                              25f,
                                                                              gameOCArrow_R.GetComponent<RectTransform>().localPosition.z);
            gameOCArrow_R.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
            gameOCSelectButton = "continue";
        }
    }

    //名前登録の各ボタンで選択している名前を進める処理
    public void SwitchName(int num)
    {
        if (namesChar[num] + 1 >= chars.Length) namesChar[num] = 0;
        else namesChar[num]++;
        names[(num + 1) * 2].GetComponent<Text>().text = chars[namesChar[num]];
    }

    //名前登録の各ボタンで選択している名前を戻す処理
    private void ReSwitchName(int num)
    {
        if (namesChar[num] - 1 < 0) namesChar[num] = chars.Length - 1;
        else namesChar[num]--;
        names[(num + 1) * 2].GetComponent<Text>().text = chars[namesChar[num]];
    }

    //------------------------------------各UIボタンの選択処理---------------------------------------------------------

    public void GameOCName0()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(62f, 97f, 0);
        gameOCArrow_L.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90f);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(55f, -17f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90f);
        gameOCSelectButton = "name0";
    }

    public void GameOCName1()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(174f, 97f, 0);
        gameOCArrow_L.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90f);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(165f, -17f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90f);
        gameOCSelectButton = "name1";
    }

    public void GameOCName2()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(289f, 97f, 0);
        gameOCArrow_L.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90f);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(280f, -17f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, -90f);
        gameOCSelectButton = "name2";
    }

    public void GameOCNext()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(-232.73f, -202.17f, 0);
        gameOCArrow_L.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(229.24f, -202.17f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
        gameOCSelectButton = "next";
    }

    public void GameOCContinueGame()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(-232.73f, 24.825f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(229.24f, 24.825f, 0);
        gameOCContinueButton.GetComponent<ChangeScene>().SceneName = currentStage;
        gameOCSelectButton = "continue";
    }

    public void GameOCRestartGame()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(-232.73f, -78.498f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(229.24f, -78.498f, 0);
        gameOCSelectButton = "restart";
    }

    public void GameOCReturnTitle()
    {
        gameOCArrow_L.GetComponent<RectTransform>().localPosition = new Vector3(-232.73f, -181.82f, 0);
        gameOCArrow_R.GetComponent<RectTransform>().localPosition = new Vector3(229.24f, -181.82f, 0);
        gameOCSelectButton = "exit";
    }

    public void StageClearNext()
    {
        isScrollStop = false;
        addScore = 0;
        subBossHp = 0;
        if (stageNum != 5)
        {
            stageNum++;
            nextSceneName = "Stage" + stageNum;
            changeScene.SceneName = nextSceneName;
            changeScene.Load();
        }
    }

    //---------------------------------------------------------------------------------------------------------------

    public IEnumerator InitBossHpMeter()
    {
        while (bossHpMeter.value <= bossHpMeter.maxValue)
        {
            if (bossHpMeter.value == bossHpMeter.maxValue)
            {
                isBossHpMeterMax = true;
                yield break;
            }
            bossHpMeter.value += 0.001f;
            yield return null;
        }
    }

    public void ChangeBossHpMeter()
    {
        if (bossHpMeter.value <= bossHpMeter.minValue)
        {
            bossHpMeter.gameObject.SetActive(false);
        }
        else
        {
            bossHpMeter.value -= subBossHp * 0.01f;
            subBossHp = 0;
        }
    }

    //デバッグ処理
    public void DebugFunc(bool b)
    {
        if (!b) return;
        if (Input.GetKeyDown(KeyCode.E) && gameState == "playing")
        {
            gameState = "gameover";
            SwitchPanel();
        }
        else if (Input.GetButtonDown("Shot") && gameState == "playing")
        {
            addScore = 1000;
        }
        else if (Input.GetKeyDown(KeyCode.C) && gameState == "playing")
        {
            gameState = "stageclear";
            SwitchPanel();
        }
        else if (Input.GetKeyDown(KeyCode.C) && gameState == "stageclear")
        {
            stageClearPanel.SetActive(false);
            guidePanel.SetActive(true);
            gameState = "playing";
        }
        else if (Input.GetKeyDown(KeyCode.F) && gameState == "playing")
        {
            zanki = 0;
            zankiText.GetComponent<Text>().text = zanki.ToString("00");
        }
        else if (Input.GetKeyDown(KeyCode.R) && gameState == "playing")
        {
            PlayerPrefs.DeleteAll();
            for (int i = 0; i < 3; i++)
            {
                if (PlayerPrefs.GetString("Name" + i.ToString()) == "")
                {
                    PlayerPrefs.SetString("Name" + i.ToString(), "???");
                }
            }
            Debug.Log("PlayerPrefs Delete All!");
        }
        else if (Input.GetKeyDown(KeyCode.T) && gameState == "playing")
        {
            subBossHp = 10;
        }
    }
}
