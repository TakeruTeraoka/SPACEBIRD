using UnityEngine;

public class Bat : EnemyBase
{
    public float timeLimit = 3f;    //発射リミット

    private bool isBulletHit = false;   //プレイヤーの弾に当たったかどうか
    private float timer = 0;    //タイマー

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 3;
        enemyScore = 75;
        enemyAnimator = this.GetComponent<Animator>();
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

        if (this.transform.position.x >= -7.25 && !isBulletHit && timer < timeLimit)
        {
            timer += Time.deltaTime;
            transform.Translate(-0.08f, 0, 0);
        }
        else if (isBulletHit || timer >= timeLimit)
        {
            transform.Translate(0.08f, 0, 0);
        }
    }

    //当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerBullet")
        {
            isBulletHit = true;
            hp--;   //HPを1減らす
            //HPが０以下になった場合
            if (hp <= 0)
            {
                enemyAnimator.Play("Clear");
            }
            else
            {
                GameManager.addScore = enemyScore;
            }
        }
    }
}
