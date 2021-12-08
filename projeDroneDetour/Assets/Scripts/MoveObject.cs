using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public bool isMoving;
    public bool isMovingRect;
    public float velocity;

    public bool keepX;
    public bool keepY;

    public Vector2 posEnd;

    float z;
    RectTransform rectTransform;    

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (keepX)
            posEnd.x = rectTransform.transform.position.x;

        if (keepY)
            posEnd.y = rectTransform.transform.position.y;

        z = rectTransform.localPosition.z;
    }
    
    void Update()
    {
        if (isMoving)
            rectTransform.transform.position = Vector2.MoveTowards(rectTransform.position, posEnd, velocity * Time.deltaTime);

        if (rectTransform.position == (Vector3)posEnd)
        {
            isMoving = false;

            if(tag == "Dialog Box")
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, z);
        }

        if (isMovingRect)
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, new Vector3(0, 0, z), velocity * Time.deltaTime);

        if (rectTransform.anchoredPosition == Vector2.zero)
            isMovingRect = false;

    }
}
