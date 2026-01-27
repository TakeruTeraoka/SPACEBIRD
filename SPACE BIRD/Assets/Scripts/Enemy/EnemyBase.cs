using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int hp;  //HPを管理する変数を宣言
    protected int enemyScore;   //継承先までの権限で、各敵のスコアを宣言
    protected float speed = 1;

    private bool isSpecialHit = false;

    private void Start()
    {

    }

    //Updateの中で一番最後に実行される
    private void LateUpdate()
    {
        if (GetComponent<Renderer>().isVisible && PlayerController.isEnemyDestory)
        {
            switch (gameObject.tag)
            {
                case "Enemy":
                    Destroy(gameObject);
                    break;
                case "Boss":
                    if (!isSpecialHit)
                    {
                        isSpecialHit = true;
                        hp = (int)(hp * 0.7);
                        Debug.Log(gameObject.name + ":" + hp);
                    }
                    break;
            }
        }
        else if (!PlayerController.isEnemyDestory)
        {
            isSpecialHit = false;
        }

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

            //HPが０以下になった場合
            if (hp <= 0)
            {
                Destroy(gameObject);    //このゲームオブジェクトを破壊する
            }
            else
            {
                GameManager.addScore = enemyScore;
            }
        }
    }
}
