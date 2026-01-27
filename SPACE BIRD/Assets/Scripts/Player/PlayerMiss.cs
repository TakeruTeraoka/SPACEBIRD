using UnityEngine;

public class PlayerMiss : MonoBehaviour
{
    public float moveSpeedX = 3f;
    public float moveSpeedY = 5f;
    public float reMoveSpeedX = 0.5f;

    private Rigidbody2D rbody;
    private bool isFalled = false;
    private bool isTeleported = false;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerState != "miss" || GameManager.gameState != "playing") return;

        if (!isFalled && transform.position.y > -6f)
        {
            //transform.Translate(moveSpeedX, -moveSpeedY, 0);
            rbody.AddForce(new Vector2(moveSpeedX, -moveSpeedY));
        }
        else
        {
            isFalled = true;
            if (!isTeleported)
            {
                isTeleported = true;
                rbody.linearVelocity = Vector3.zero;
                transform.position = new Vector2(12f, 0f);
            }
            else if (transform.position.x > 6.87f)
            {
                transform.Translate(-reMoveSpeedX, 0, 0);

            }
            else
            {
                rbody.linearVelocity = Vector3.zero;
                isFalled = false;
                isTeleported = false;
                PlayerController.playerState = "alive";
                Invoke("PlayStopAnime", 1f);
            }
        }
    }

    private void PlayStopAnime()
    {
        if (PlayerController.playerState == "alive")
        {
            animator.Play("Stop");
        }
    }
}
