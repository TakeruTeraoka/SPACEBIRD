using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static string input = "Touch";

    // Update is called once per frame
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

        Debug.Log("Input:" + input);
    }
}
