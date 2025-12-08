using UnityEngine;

public class NearBackgroundManager : MonoBehaviour
{
    public float scrollSpeed;
    public float cloneStartPoint;
    public float destroyPoint;
    public float clonePoint;
    public GameObject nearObject;
    public GameObject stageObject;

    private bool isClone = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != "playing") return;

        transform.Translate(scrollSpeed, 0, 0);

        if (this.transform.position.x >= cloneStartPoint && !isClone)
        {
            GameObject cloneObject = Instantiate(nearObject, new Vector3(clonePoint, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            cloneObject.name = nearObject.name;
            cloneObject.transform.parent = stageObject.transform;
            isClone = true;
        }
        else if (this.transform.position.x >= destroyPoint)
        {
            Destroy(this.gameObject);
        }
    }
}
