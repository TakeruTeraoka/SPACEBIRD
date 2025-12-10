using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected int hp;  //HPを管理する変数を宣言

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
            }
        }
    }
}
