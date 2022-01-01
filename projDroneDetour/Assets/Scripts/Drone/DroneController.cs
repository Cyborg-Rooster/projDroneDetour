using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public float jumpForce;
    bool canJump = true;

    [SerializeField] GameManager gameManager;
    [SerializeField] Transform topCollisionCheck;
    [SerializeField] Transform downCollisionCheck;
    [SerializeField] SoundController soundController;

    Rigidbody2D rgbd;
    Animator animator;

    PhysicsManager physicsManager;
    AnimatorManager animatorManager;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        physicsManager = new PhysicsManager()
        {
            Rigidbody2D = rgbd,
            JumpForce = jumpForce
        };

        animatorManager = new AnimatorManager()
        {
            Animator = animator
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canJump)
        {
            if (InputObserver.ReturnObjectTouched() == null && physicsManager.State)
            {
                Statistics.Clicks++;
                soundController.Play(soundController.Audio[0]);
                physicsManager.Jump();
            }
        }
        if (transform.position.y < downCollisionCheck.position.y && canJump) 
        {
            Statistics.Falls++;
            StartCoroutine(Die()); 
        }
        if (transform.position.y > topCollisionCheck.position.y) TurnOff();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Statistics.Crashes++;
        StartCoroutine(Die());
    }

    private void TurnOff()
    {
        soundController.Play(soundController.Audio[2]);
        gameManager.TurnOffDrone();
    }

    IEnumerator Die()
    {
        canJump = false;
        soundController.Play(soundController.Audio[1]);
        Statistics.Death++;
        gameManager.StopGame();
        SetPhysicalState(false);
        yield return animatorManager.Die();
        Destroy(gameObject);
    }

    public void SetPhysicalState(bool state)
    {
        physicsManager.SetComponentState(state);
    }
}
