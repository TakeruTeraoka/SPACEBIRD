using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing")
        {
            this.GetComponent<Animator>().speed = 0;
            return;
        }

        this.GetComponent<Animator>().speed = 1;
    }
}
