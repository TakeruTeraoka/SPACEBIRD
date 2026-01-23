using UnityEngine;
using UnityEngine.UI;

public class CountScore : MonoBehaviour
{
    public GameObject stageClearHiscoreText;

    private bool isCount = false;
    private int cnt = 0;

    // Update is called once per frame
    void Update()
    {
        if (isCount && cnt != GameManager.totalScore)
        {
            cnt += (GameManager.totalScore / 100);
            stageClearHiscoreText.GetComponent<Text>().text = cnt.ToString("00000000");
        }
        else
        {
            isCount = false;
            cnt = 0;
        }
    }

    public void Count()
    {
        isCount = true;
    }
}
