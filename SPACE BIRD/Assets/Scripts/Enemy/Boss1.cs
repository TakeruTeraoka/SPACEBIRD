using UnityEngine;

public class Boss1 : EnemyBase
{
    //public GameObject hpMeter;
    public float span = 2.0f; //発射間隔

    private float delta = 0;    //加算用変数
    private bool isSpecial = false; //敵を飛ばしたかどうかを管理するフラグ

    private void Start()
    {
        //hpMeter.SetActive(false);
        hp = 100;
        enemyScore = 50;
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
                Attack();
            }
            else
            {
                //フレーム間の差を加算する
                delta += Time.deltaTime;
            }
        }

        //HpMeterChange();
    }


    //停止時アニメーション再生
    public void AnimationStop()
    {
        this.GetComponent<Animator>().Play("Stop");
    }

    //攻撃
    public void Attack()
    {

        //通常攻撃
        if (!isSpecial)
        {
            Debug.Log("通常攻撃");
            //弾幕を発生させる
            //ステージの子にする
        }
        //特別攻撃
        else
        {
            Debug.Log("特別攻撃");
            //Attackアニメーションを再生
            this.GetComponent<Animator>().Play("Attack");
            //敵を複製する
            //ステージの子にする
            //拡散させる
        }

        isSpecial = !isSpecial; //フラグを反転
    }

    private void OnBecameVisible()
    {
        //hpMeter.SetActive(true);
        //hpMeter.GetComponent<Animator>().Play("Init");
    }

    /*private void HpMeterChange()
    {
        hpMeter.GetComponent<RectTransform>().localScale =
            new Vector3(hpMeter.GetComponent<RectTransform>().localScale.x, hp / 100, hpMeter.GetComponent<RectTransform>().localScale.z);
    }*/
}
