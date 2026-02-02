using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    public float topLimit;
    public float bottomLimit;
    public float leftLimit;
    public float rightLimit;
    public float enemyBulletSpeed = 0.08f;  //弾の移動スピード

    private Vector2 differencePos; //プレイヤーと弾の座標の差
    private float enemyBulletTime;  //移動スピードの調整用

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //プレイヤーの座標を取得
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //敵の弾の座標を取得
        Vector2 enemyBulletPos = this.transform.position;
        //プレイヤーと弾の座標の差を代入
        differencePos = playerPos - enemyBulletPos;
        //座標の距離と移動スピードの商を代入
        enemyBulletTime = Vector2.Distance(playerPos, enemyBulletPos) / enemyBulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing") return;

        //移動
        transform.Translate(differencePos.x / enemyBulletTime, differencePos.y / enemyBulletTime, 0);

        //もしプレイヤーがミスした場合
        if (PlayerController.playerState != "alive")
        {
            //弾を破壊する
            Destroy(gameObject);
        }

        //もし指定範囲を超える場合
        if (transform.position.y > topLimit || transform.position.y < bottomLimit ||
            transform.position.x > rightLimit || transform.position.x < leftLimit)
        {
            //弾を破壊する
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //もし当たったオブジェクトがプレイヤーだった場合
        if (collision.gameObject.tag == "Player")
        {
            //弾を破壊する
            Destroy(gameObject);
        }
    }
}
