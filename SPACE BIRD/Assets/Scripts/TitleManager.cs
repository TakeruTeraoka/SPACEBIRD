using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    private GameObject StartButton;
    private GameObject HiscoreButton;
    private bool isMove = false;
    private bool isSelected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 59;
        StartButton = GameObject.Find("StartButton");
        HiscoreButton = GameObject.Find("HiscoreButton");
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && !isMove && !isSelected)
        {
            isMove = true;
            if (this.GetComponent<RectTransform>().localPosition == new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z))
            {
                this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, HiscoreButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
            }
            else
            {
                this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            isMove = false;
        }

        if (Input.GetButtonDown("Submit"))
        {
            isSelected = true;
            this.GetComponent<Animator>().Play("Blick");
            if (this.GetComponent<RectTransform>().localPosition == new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z))
            {
                StartButton.GetComponent<Animator>().Play("Blick");
            }
            else
            {
                HiscoreButton.GetComponent<Animator>().Play("Blick");
            }
        }
    }

    public void StartButtonClick()
    {
        HiscoreButton.GetComponent<Button>().interactable = false;
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
        StartButton.GetComponent<Animator>().Play("Blick");
        this.GetComponent<Animator>().Play("Blick");
    }

    public void HiscoreButtonClick()
    {
        StartButton.GetComponent<Button>().interactable = false;
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, HiscoreButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
        HiscoreButton.GetComponent<Animator>().Play("Blick");
        this.GetComponent<Animator>().Play("Blick");
    }
}
