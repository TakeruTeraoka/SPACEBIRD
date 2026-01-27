using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject enemyBullet;  //敵の弾
    public float shotStartPos;  //弾の発射開始位置
    public float span = 2.0f;   //発射の間隔

    private float delta = 0;    //経過時間のカウント

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing") return;

        if (this.transform.position.x >= shotStartPos)
        {
            this.delta += Time.deltaTime; //フレーム間の時間差を加算
                                          //経過時間が発射間隔に達した場合
            if (this.delta > this.span)
            {
                this.delta = 0; //経過時間を０に戻す
                                //弾のクローンを作成
                GameObject bulletClone = Instantiate(enemyBullet, this.transform.position, Quaternion.identity);
                //クローンの名前を変更
                bulletClone.name = "EnemyBullet";
            }
        }
    }
}
