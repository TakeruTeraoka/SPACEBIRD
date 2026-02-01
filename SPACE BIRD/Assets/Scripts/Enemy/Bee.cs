using UnityEngine;

public class Bee : EnemyBase
{
    public bool isMoveMode = false; //Y座標への移動管理フラグ
    public float moveYLimit;    //Y座標の移動限界
    public float moveYSpeed = 0.01f;    //Y座標の移動速度

    private float initPosY;  //初期Y座標
    private float currPosY; //現在Y座標

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 2;
        enemyScore = 50;
        enemyAnimator = this.GetComponent<Animator>();
        initPosY = this.transform.position.y;
        speed = this.GetComponent<Animator>().speed;    //現在設定されているアニメーションの速度を取得
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームの状態がプレイ中でない時
        if (GameManager.gameState != "playing")
        {
            //アニメーションの速度を０にし、処理をスキップする
            this.GetComponent<Animator>().speed = 0;
            return;
        }
        this.GetComponent<Animator>().speed = speed;   //元の速度に戻す

        if (isMoveMode)
        {
            currPosY = this.transform.position.y;
            if (Mathf.Abs(currPosY - initPosY) >= moveYLimit)
            {
                moveYSpeed = -moveYSpeed;
            }
            transform.Translate(0, moveYSpeed, 0);
        }
    }
}
