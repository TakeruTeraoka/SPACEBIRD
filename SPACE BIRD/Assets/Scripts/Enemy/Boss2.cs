using UnityEngine;

public class Boss2 : EnemyBase
{
    public float span = 2f;   //発射間隔

    private float delta = 0;    //加算用変数
    private bool isSpecial = false; //攻撃用フラグ
    private bool isEyesOpened = false;    //第二形態移行完了フラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 100;
        enemyScore = 75;
        speed = this.GetComponent<Animator>().speed;
        enemyAnimator = this.GetComponent<Animator>();
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

        //元の速度に戻す
        this.GetComponent<Animator>().speed = speed;

        if (GameManager.isScrollStop)
        {
            if (hp > 50)
            {
                //一定時間に達した場合
                if (delta >= span)
                {
                    //加算用変数を０に戻す
                    delta = 0;
                    //攻撃処理
                    NormalAttack();
                }
                else
                {
                    //フレーム間の差を加算する
                    delta += Time.deltaTime;
                }
            }
            else if (!isEyesOpened)
            {
                enemyAnimator.Play("OpenEye");
            }
            else
            {
                //一定時間に達した場合
                if (delta >= span)
                {
                    //加算用変数を０に戻す
                    delta = 0;
                    //攻撃処理
                    SecondAttack();
                }
                else
                {
                    //フレーム間の差を加算する
                    delta += Time.deltaTime;
                }
            }
        }
    }

    public void NormalAttack()
    {
        if (isSpecial)
        {
            enemyAnimator.Play("Beam_Normal");
        }

        isSpecial = !isSpecial; //フラグを反転
    }

    public void SecondAttack()
    {
        if (isSpecial)
        {
            enemyAnimator.Play("Beam_Special");
        }

        isSpecial = !isSpecial; //フラグを反転
    }

    public void PlayStopAnime()
    {
        enemyAnimator.Play("Stop");
    }

    public void PlayStopOpenAnime()
    {
        enemyAnimator.Play("StopOpen");
    }

    public void EyesOpen()
    {
        isEyesOpened = true;
    }
}
