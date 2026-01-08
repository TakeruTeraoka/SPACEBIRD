using UnityEngine;

public class Bat : EnemyBase
{
    private float speed = 1;    //アニメーションの再生速度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 2;
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
    }
}
