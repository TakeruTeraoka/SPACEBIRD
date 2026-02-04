using UnityEngine;

public class Boss3 : EnemyBase
{
    public float span = 3.0f;   //攻撃間隔

    private float delta = 0;    //加算用変数
    private bool isSpecial = false; //攻撃用フラグ
    private string[] attacks = { "Desert", "Noize", "Drop" };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 100;
        enemyScore = 80;
        enemyAnimator = this.GetComponent<Animator>();
        speed = this.GetComponent<Animator>().speed;
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
            //一定時間に達した場合
            if (delta >= span)
            {
                //加算用変数を０に戻す
                delta = 0;
                //攻撃処理
                enemyAnimator.Play("Attack");
            }
            else
            {
                //フレーム間の差を加算する
                delta += Time.deltaTime;
            }
        }
    }

    public void Attack()
    {
        //特別攻撃
        if (isSpecial)
        {
            int ran = Random.Range(0, attacks.Length);
            switch (attacks[ran])
            {
                case "Desert":
                    //下から砂を隆起させる
                    break;
                case "Noize":
                    //砂嵐をオンにして敵を横から飛ばす
                    break;
                case "Drop":
                    //上から物理演算で敵を落とす
                    break;
            }
        }

        isSpecial = !isSpecial; //フラグを反転
    }

    public void PlayStopAnime()
    {
        enemyAnimator.Play("Stop");
    }
}
