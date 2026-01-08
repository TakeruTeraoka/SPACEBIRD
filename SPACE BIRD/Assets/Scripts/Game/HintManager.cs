using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static string input = "Touch";

    public GameObject keyboardGuide;
    public GameObject touchGuide;
    public GameObject gamePadGuide;

    void Start()
    {
        SwitchGuide();
    }

    void Update()
    {
        if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) && !Input.GetButton("AnyButton"))
        {
            input = "Keyboard";
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            input = "Touch";
        }
        else if (Input.GetButton("AnyButton") || Input.GetAxisRaw("AnyButton") != 0)
        {
            input = "GamePad";
        }

        SwitchGuide();

        Debug.Log("Input:" + input);
    }

    private void SwitchGuide()
    {
        switch (input)
        {
            case "Keyboard":
                keyboardGuide.SetActive(true);
                touchGuide.SetActive(false);
                gamePadGuide.SetActive(false);
                break;

            case "Touch":
                keyboardGuide.SetActive(false);
                touchGuide.SetActive(true);
                gamePadGuide.SetActive(false);
                break;

            case "GamePad":
                keyboardGuide.SetActive(false);
                touchGuide.SetActive(false);
                gamePadGuide.SetActive(true);
                break;
        }
    }
}
