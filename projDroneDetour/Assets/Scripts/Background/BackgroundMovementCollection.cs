using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovementCollection
{
    static MovementController[] MovementControllers;

    public static void AddControllers(MovementController[] controller)
    {
        MovementControllers = controller;
    }

    public static void SetMovementControllersState(bool state)
    {
        foreach (var control in MovementControllers) control.SetIsMoving(state);
    }
}
