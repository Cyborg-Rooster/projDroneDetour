using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] MovementController[] Backgrounds;

    [SerializeField] Animator Cloudy;
    [SerializeField] Animator Rain;

    [SerializeField] Animator Sky;
    [SerializeField] Animator[] Clouds;
    [SerializeField] Animator[] Buildings;
    [SerializeField] Animator[] Decorations;

    public bool isMoving;

    // Start is called before the first frame update
    void Awake()
    {
        BackgroundMovementCollection.AddControllers(Backgrounds);
        BackgroundDayCicleCollection.AddAnimator(Sky, Clouds, Buildings, Decorations, Cloudy);
        CloudyController.AddControllers(Cloudy, Rain, this);
    }

    public void SetMovingState(bool state)
    {
        BackgroundMovementCollection.SetMovementControllersState(state);
    }

    public void SetDay(bool day)
    {
        BackgroundDayCicleCollection.ChangeDayCicle(day);
    }

    public void SetRain(bool rain, bool day)
    {
        CloudyController.SetRain(rain, day);
    }
}
