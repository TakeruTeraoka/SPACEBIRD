using UnityEngine;

public class PlayerBulletMove : MonoBehaviour
{
    public float playerBulletSpeed = 0.2f;  //弾の発射スピード

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing") return;

        transform.Translate(-playerBulletSpeed, 0, 0);
        if (transform.position.x < -9.153)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
