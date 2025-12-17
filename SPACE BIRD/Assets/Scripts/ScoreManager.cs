using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int totalScore = 0;   //合計スコア
    public static int addScore = 0;     //加算するスコア
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<Text>().text = score.ToString("000000");
    }

    // Update is called once per frame
    void Update()
    {
        if (addScore != 0)
        {
            score += addScore;
            totalScore += addScore;
            if (score > 999999)
            {
                //残機を一つ増やす
                score = 0;
            }
            this.GetComponent<Text>().text = score.ToString("000000");
            addScore = 0;
        }

        Debug.Log("TotalScore:" + totalScore);
    }
}
