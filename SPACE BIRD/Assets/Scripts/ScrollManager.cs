using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public float scrollSpeed = 0.01f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(scrollSpeed, 0, 0);
    }
}
