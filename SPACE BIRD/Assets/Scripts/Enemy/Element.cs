using UnityEngine;

public class GreenElement : EnemyBase
{
    public float boundPower = 0.01f;

    private Rigidbody2D rbody;
    private bool isBounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        hp = 3;
        enemyScore = 50;
        enemyAnimator = GetComponent<Animator>();
        speed = this.GetComponent<Animator>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームの状態がプレイ中でない時
        if (GameManager.gameState != "playing")
        {
            rbody.simulated = false;
            //アニメーションの速度を０にし、処理をスキップする
            this.GetComponent<Animator>().speed = 0;
            return;
        }

        rbody.simulated = true;
        this.GetComponent<Animator>().speed = speed;   //元の速度に戻す

        if (this.transform.position.y > -3 && !isBounded)
        {
            rbody.AddForceY(-boundPower, ForceMode2D.Impulse);
            if (this.transform.position.y <= -2.43)
            {
                rbody.linearVelocityY = 0;
                isBounded = true;
                if (hp > 0)
                {
                    enemyAnimator.Play("Bound");
                }
            }
        }
        else if (this.transform.position.y < 3)
        {
            rbody.AddForceY(boundPower * 0.65f, ForceMode2D.Impulse);
            if (this.transform.position.y >= 2.43)
            {
                isBounded = false;
            }
        }
    }

    private void PlayStopAnime()
    {
        if (hp > 0)
        {
            enemyAnimator.Play("Stop");
        }
    }
}
