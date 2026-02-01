using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int hp;  //HPを管理する変数（継承時のみ使用可）
    protected int enemyScore;   //各敵のスコア（継承時のみ使用可）
    protected float speed = 1;  //アニメーションの速度（継承時のみ使用可）
    protected Animator enemyAnimator;    //各敵のアニメーター

    private bool isSpecialHit = false;  //ボム攻撃の被弾を一度だけ有効にするフラグ（ボス用）

    //Updateの中で一番最後に実行される
    private void LateUpdate()
    {
        if (GetComponent<Renderer>().isVisible && PlayerController.isEnemyDestory)
        {
            switch (gameObject.tag)
            {
                case "Enemy":
                    EnemyDestory();
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
            EnemyDestory();
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
                enemyAnimator.Play("Clear");
            }
            else
            {
                GameManager.addScore = enemyScore;
            }
        }
    }

    public void EnemyDestory()
    {
        Destroy(gameObject);    //このゲームオブジェクトを破壊する
    }

    public void StageClear()
    {
        GameManager.gameState = "stageclear";
    }
}
