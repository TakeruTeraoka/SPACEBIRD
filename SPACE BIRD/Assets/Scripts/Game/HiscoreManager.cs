using UnityEngine;
using UnityEngine.UI;

public class HiscoreManager : MonoBehaviour
{
    public GameObject namePanel;
    public GameObject scorePanel;

    private Text[] nameTexts = new Text[3];
    private Text[] scoreTexts = new Text[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 59;
        nameTexts = namePanel.GetComponentsInChildren<Text>();
        scoreTexts = scorePanel.GetComponentsInChildren<Text>();
        for (int i = 0; i < 3; i++)
        {
            nameTexts[i].text = PlayerPrefs.GetString("Name" + i.ToString());
            scoreTexts[i].text = PlayerPrefs.GetInt("Score" + i.ToString()).ToString("00000000");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Back"))
        {
            BackButtonClick();
        }
    }

    public void BackButtonClick()
    {
        this.GetComponent<Animator>().Play("Press");
    }
}
