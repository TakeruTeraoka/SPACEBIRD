using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static string playerState = "alive";     //プレイヤーの状態(alive, miss, recovery)
    public static bool isSpecial = false;   //スペシャル攻撃フラグ(ボム攻撃フラグ)
    public static bool isEnemyDestory = false;  //敵破壊フラグ（スペシャル攻撃用）

    public float baseSpeed = 3.0f;  //移動速度（通常）
    public float slowSpeed = 1.0f;  //移動速度（スロー）

    //移動範囲制限
    public float UpLimit;
    public float DownLimit;
    public float LeftLimit;
    public float RightLimit;

    private float speed;    //移動速度切替用
    private Rigidbody2D rbody;  //物理演算用
    private float axisH;    //入力用変数（水平）
    private float axisV;    //入力用変数（垂直）
    private bool isSlow = false;    //スロー移動フラグ
    private bool isMoving = false;  //タッチ操作フラグ
    private Animator animator;  //アニメーター(アニメーションの遷移を管理)
    private int clickCnt = 0;   //クリック回数（ダブルクリック用）
    private Vector3 currentPos; //プレイヤーの現在の座標（スペシャル攻撃用）

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
        isSpecial = false;
        isEnemyDestory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing")
        {
            rbody.linearVelocity = Vector2.zero;
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }

        if (playerState == "miss") return;

        PlayerAlive();
    }

    void FixedUpdate()
    {
        if (GameManager.gameState != "playing" || playerState == "miss") return;

        if (!isMoving)
        {
            speed = !isSlow ? baseSpeed : slowSpeed;
            rbody.linearVelocity = new Vector2(axisH * speed, axisV * speed);
        }
    }

    private void PlayerAlive()
    {
        float posX = Mathf.Clamp(transform.position.x, LeftLimit, RightLimit);
        float posY = Mathf.Clamp(transform.position.y, DownLimit, UpLimit);
        transform.position = new Vector2(posX, posY);

        if (!isMoving)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetButton("Slow"))
        {
            isSlow = true;
        }
        else if (Input.GetButtonUp("Slow"))
        {
            isSlow = false;
        }

        if (Input.GetButtonDown("Special") && GameManager.isChargeMax && playerState == "alive")
        {
            playerState = "special";
            currentPos = transform.position;
            transform.position = Vector3.zero;
            animator.Play("Bomb");
        }
    }

    private void EnemyDestory()
    {
        isEnemyDestory = true;
    }

    private void InitPos()
    {
        transform.position = currentPos;
    }

    private void PlayFadeInAnime()
    {
        animator.Play("FadeIn");
    }

    private void PlayRecoveryAnime()
    {
        isEnemyDestory = false;
        animator.Play("Recovery");
        Invoke("PlayStopAnime", 1f);
    }

    private void PlayStopAnime()
    {
        playerState = "alive";
        isSpecial = true;
        animator.Play("Stop");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Boss") && playerState != "recovery")
        {
            GameManager.addZanki = -1;
            rbody.linearVelocity = Vector2.zero;
            isSlow = false;
            playerState = "miss";
            animator.Play("Miss");
        }
    }

    //------------------------------タッチ操作関係のメソッド（マウスカーソル操作と共通）------------------------------------------------

    void OnMouseDrag()
    {
        if (GameManager.gameState != "playing" || playerState == "miss") return;

        isMoving = true;
        PlayerShoot.isPlayerTouch = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = mouseWorldPosition;
    }

    void OnMouseUp()
    {
        PlayerShoot.isPlayerTouch = false;
        isMoving = false;
    }

    void OnMouseDown()
    {
        if (GameManager.gameState != "playing" || playerState == "miss") return;

        clickCnt++;
        Invoke("DoubleClick", 0.3f);
    }

    private void DoubleClick()
    {
        if (clickCnt != 2) { clickCnt = 0; return; }
        else
        {
            clickCnt = 0;
        }

        if (GameManager.isChargeMax)
        {
            playerState = "special";
            currentPos = transform.position;
            transform.position = Vector3.zero;
            animator.Play("Bomb");
        }
    }

    //-------------------------------------------------------------------------------------------------
}
