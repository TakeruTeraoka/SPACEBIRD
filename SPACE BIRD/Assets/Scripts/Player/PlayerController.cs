using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static string playerState = "alive";
    public static bool isSpecial = false;
    public static bool isEnemyDestory = false;

    public float baseSpeed = 3.0f;
    public float slowSpeed = 1.0f;
    public float UpLimit;
    public float DownLimit;
    public float LeftLimit;
    public float RightLimit;

    public GameObject barrier;

    private float speed;
    private Rigidbody2D rbody;
    private float axisH;
    private float axisV;
    private bool isSlow = false;
    private bool isMoving = false;
    private Animator animator;
    private int clickCnt = 0;
    private Vector3 currentPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        barrier.SetActive(false);
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
        animator.Play("Miss");
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
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Boss")
        {
            GameManager.addZanki = -1;
            rbody.linearVelocity = Vector2.zero;
            playerState = "miss";
            animator.Play("Miss");
        }
    }

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
            animator.Play("Bomb");
        }
    }
}
