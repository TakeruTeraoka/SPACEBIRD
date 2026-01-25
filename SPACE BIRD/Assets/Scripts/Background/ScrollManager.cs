using UnityEngine;

public class ScrollManager : MonoBehaviour
{

    public float scrollSpeed = 0.01f;
    private float speed;

    void Start()
    {
        speed = scrollSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing") return;

        if (GameManager.isScrollStop)
        {
            if (scrollSpeed > 0)
            {
                scrollSpeed -= 0.001f;
            }
            else if (scrollSpeed <= 0)
            {
                scrollSpeed = 0;
            }
        }
        else
        {
            scrollSpeed = speed;
        }

        transform.Translate(scrollSpeed, 0, 0);
    }
}
