using UnityEngine;

public class HiscoreManager : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            BackButtonClick();
        }
    }

    public void BackButtonClick()
    {
        this.GetComponent<Animator>().Play("Press");
    }
}
