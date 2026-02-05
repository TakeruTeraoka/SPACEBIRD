using UnityEngine;

public class SnowBalls : EnemyBase
{
    public GameObject snowBallsCenter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 2;
        enemyScore = 25;
        speed = GetComponent<Animator>().speed;
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        //ゲームの状態がプレイ中でない時
        if (GameManager.gameState != "playing")
        {
            //アニメーションの速度を０にし、処理をスキップする
            this.GetComponent<Animator>().speed = 0;
            snowBallsCenter.GetComponent<Animator>().speed = 0;
            return;
        }
        this.GetComponent<Animator>().speed = speed;   //元の速度に戻す
        snowBallsCenter.GetComponent<Animator>().speed = speed;
    }
}
