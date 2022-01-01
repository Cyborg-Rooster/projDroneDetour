using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public float velocity;
    bool scored = false;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameManager gameManager;

    [SerializeField] Transform left;
    [SerializeField] Transform right;

    Rigidbody2D rgbd;

    void Awake()
    {
        Building.Sprites = sprites;
    }

    void Start()
    {
        transform.position = right.position;
        rgbd = GetComponent<Rigidbody2D>();
        //SetMovingState(false);
        //movementController = GetComponent<MovementController>();
        SetDefaultParameters();
    }

    void Update()
    {
        if(/*movementController.ReturnIfItIsInTargetPosition()*/transform.position.x <= left.position.x)
        {
            SetDefaultParameters();
            transform.position = new Vector3(right.position.x, transform.position.y, transform.position.z);
        }

        if(transform.position.x <= -0.245f && !scored)
        {
            gameManager.AddPoint();
            scored = true;
        }
    }

    void SetDefaultParameters()
    {
        GetComponent<SpriteRenderer>().sprite = Building.Sprite;
        transform.position = new Vector3(transform.position.x, Building.Y, transform.position.z);
        /*movementController.ChangeTargetPositionAndMove
        (
            new Vector3(left.position.x, transform.position.y, transform.position.z)
        );*/
        //SetMovingState(true);
        scored = false;
    }

    public void SetMovingState(bool state)
    {
        //movementController.SetIsMoving(state);
        if(state) rgbd.velocity = new Vector2(velocity, 0);
        else rgbd.velocity = new Vector2(0, 0);
    }
}
