using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private string selectButton = "start"; //選択中のボタン
    private RectTransform rect; //矢印の座標移動
    private bool isDown = false;    //スティック入力を管理するフラグ
    private bool isSelect = false;  //ボタンの選択を管理するフラグ

    //各オブジェクトのアニメーター
    public Animator arrowAnimator;
    public Animator startButtonAnimator;
    public Animator hiscoreButtonAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        arrowAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //もしボタンが選択されていたら、処理をスキップする
        if (isSelect) return;

        //もし縦移動ボタンが入力された場合（スティックの場合、一回だけ入力されたとき）
        if (Input.GetButtonDown("Vertical") || Mathf.Abs(Input.GetAxisRaw("L_Stick_V")) != 0 && isDown == false)
        {
            //もしスタートボタンを選択している場合、ハイスコアボタンを選択
            if (selectButton.Equals("start"))
            {
                selectButton = "hiscore";
                rect.localPosition = new Vector3(-181.96f, -205f, 0);
            }
            //そうでなければ、スタートボタンを選択
            else
            {
                selectButton = "start";
                rect.localPosition = new Vector3(-181.96f, -126f, 0);
            }
            //入力管理フラグを立てる
            isDown = true;
        }
        //スティック入力がされていない場合
        else if (Input.GetAxisRaw("L_Stick_V") == 0)
        {
            //入力管理フラグを折る
            isDown = false;
        }

        //もし決定ボタンが入力された場合
        if (Input.GetButtonDown("Submit"))
        {
            //シーンをロード
            SceneLoad();
        }
    }

    //スタートボタンがクリックされた
    public void StartButtonClick()
    {
        //もしボタンが選択されていたら、処理をスキップする
        if (isSelect) return;
        rect.localPosition = new Vector3(-181.96f, -126f, 0);
        selectButton = "start";
        SceneLoad();
    }

    //ハイスコアボタンがクリックされた
    public void HiscoreButtonClick()
    {
        //もしボタンが選択されていたら、処理をスキップする
        if (isSelect) return;
        rect.localPosition = new Vector3(-181.96f, -205f, 0);
        selectButton = "hiscore";
        SceneLoad();
    }

    //ボタンの選択アニメーションが終了次第、次のシーンをロード
    public void SceneLoad()
    {
        //ボタン選択フラグを立てる
        isSelect = true;
        arrowAnimator.Play("Blick");
        if (selectButton.Equals("start"))
        {
            startButtonAnimator.Play("Blick_bt_s");
        }
        else
        {
            hiscoreButtonAnimator.Play("Blick_bt_h");
        }
    }
}
