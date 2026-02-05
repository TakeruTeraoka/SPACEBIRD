using UnityEngine;

public class SnowMan : EnemyBase
{
    public float limit = 3f;

    private float delta = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = 3;
        enemyScore = 50;
        speed = GetComponent<Animator>().speed;
        enemyAnimator = GetComponent<Animator>();
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

        if (transform.position.x >= -6.98f)
        {
            if (delta < limit)
            {
                delta += Time.deltaTime;
                transform.Translate(-0.08f, 0, 0);
            }
            else
            {
                transform.Translate(0.24f, 0, 0);
            }
        }
    }
}
