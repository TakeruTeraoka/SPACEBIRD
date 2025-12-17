using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int hp;  //HPを管理する変数を宣言
    //継承先までの権限で、各敵のスコアを宣言
    protected int enemyScore;//

    private GameObject barrierShield;   //バリア
    private GameObject bombBoost;       //ボムブースト
    private GameObject scorePoint;      //スコアポイント
    //private GameObject zankiItem; //残基

    private void Start()
    {
        barrierShield = GameObject.Find("BarrierShield");
        bombBoost = GameObject.Find("BombBoost");
        scorePoint = GameObject.Find("ScorePoint");
    }


    //Updateの中で一番最後に実行される
    private void LateUpdate()
    {
        //敵オブジェクトのX座標が11以上の場合
        if (this.transform.position.x >= 11)
        {
            Destroy(gameObject);    //このゲームオブジェクトを破壊する
        }
    }

    //当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlayerBullet")
        {
            hp--;   //HPを1減らす

            Debug.Log("現在のHP：" + hp);

            //HPが０以下になった場合
            if (hp <= 0)
            {
                Destroy(gameObject);    //このゲームオブジェクトを破壊する
                //宣言した敵のスコアを加算する(addscoreに代入)
                //Scorepointのクローンを自分の位置に出す
                //50%の確率でアイテムを出す・その中でどのアイテムを出す
            }
        }
    }
}
