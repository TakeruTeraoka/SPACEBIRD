using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private GameObject StartButton;
    private GameObject HiscoreButton;
    private bool isMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartButton = GameObject.Find("StartButton");
        HiscoreButton = GameObject.Find("HiscoreButton");
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetAxisRaw("Vertical") != 0 && !isMove)
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
        else if(Input.GetButtonUp("Vertical") || Input.GetAxisRaw("Vertical") == 0)
        {
            isMove = false;
        }

        if(Input.GetButtonDown("Submit"))
        {
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
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, StartButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
        StartButton.GetComponent<Animator>().Play("Blick");
        this.GetComponent<Animator>().Play("Blick");
    }

    public void HiscoreButtonClick()
    {
        this.GetComponent<RectTransform>().localPosition = new Vector3(this.GetComponent<RectTransform>().localPosition.x, HiscoreButton.GetComponent<RectTransform>().localPosition.y, this.GetComponent<RectTransform>().localPosition.z);
        HiscoreButton.GetComponent<Animator>().Play("Blick");
        this.GetComponent<Animator>().Play("Blick");
    }
}
