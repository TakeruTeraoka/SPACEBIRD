using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static bool isPlayerTouch = false;

    public float span = 0.2f;
    public GameObject playerBullet;

    private float delta = 0.2f;
    private bool isShot = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing") return;

        isShot = false;

        if (Input.GetButton("Shot") && !isShot && !isPlayerTouch)
        {
            isShot = true;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.gameState != "playing") return;

        if (isShot || isPlayerTouch)
        {
            delta += Time.deltaTime;
            if (delta > span)
            {
                ScoreManager.addScore = 1000;   //1000“_‰ÁŽZ‚·‚é
                delta = 0;
                GameObject clone = Instantiate(playerBullet, this.transform.position, Quaternion.identity);
                clone.name = playerBullet.name;
            }
        }
        else
        {
            delta = 0;
        }
    }
}
