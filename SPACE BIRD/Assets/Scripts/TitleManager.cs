using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static string SceneName = "Movie_st1";
    private ChangeScene changeScene;
    private GameObject StartButton;
    private GameObject HiscoreButton;
    private bool isMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changeScene = GetComponent<ChangeScene>();
        StartButton = GameObject.Find("StartButton");
        HiscoreButton = GameObject.Find("HiscoreButton");
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical") || Input.GetAxisRaw("Vertical") != 0 && !isMove)
        {
            isMove = true;
            if (this.GetComponent<RectTransform>().localPosition == new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z))
            {
                this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, HiscoreButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
                SceneName = "Hiscore";
            }
            else
            {
                this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
                SceneName = "Movie_st1";
            }
        }
        else if(Input.GetButtonUp("Vertical") || Input.GetAxisRaw("Vertical") == 0)
        {
            isMove = false;
        }

        if(Input.GetButtonDown("Submit"))
        {
            changeScene.SceneName = SceneName;
            changeScene.Load();
        }
    }
}
