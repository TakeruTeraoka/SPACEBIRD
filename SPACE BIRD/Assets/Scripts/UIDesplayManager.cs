using UnityEngine;

public class UIDesplayManager : MonoBehaviour
{
    private GameObject playingPanel;

    private void Start()
    {
        playingPanel = GameObject.Find("PlayingPanel");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playingPanel.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playingPanel.SetActive(true);
        }
    }
}
