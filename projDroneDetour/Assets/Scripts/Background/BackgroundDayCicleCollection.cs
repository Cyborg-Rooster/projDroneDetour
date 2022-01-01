using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BackgroundDayCicleCollection
{
    static Animator Sky;
    static Animator Cloudy;
    static Animator[] Clouds;
    static Animator[] Buildings;
    static Animator[] Decorations;

    public static void AddAnimator(Animator sky, Animator[] clouds, Animator[] buildings, Animator[] decorations, Animator cloudy)
    {
        Sky = sky;
        Clouds = clouds;
        Cloudy = cloudy;
        Buildings = buildings;
        Decorations = decorations;
    }

    public static void ChangeDayCicle(bool cicle)
    {
        if (cicle) SetDay();
        else SetNight();
    }

    static void SetDay()
    {
        foreach (var building in Buildings) 
        {
            building.SetTrigger("day");
            building.Play("buildingTransition"); 
        }

        Cloudy.SetTrigger("day");

        Sky.SetTrigger("day");
        Sky.Play("skyTransition");

        foreach (var decoration in Decorations) decoration.Play("decorationDay");
        foreach (var cloud in Clouds) cloud.Play("cloudDay");
    }

    static void SetNight()
    {
        foreach (var building in Buildings)
        {
            building.SetTrigger("night");
            building.Play("buildingTransition");
        }

        Cloudy.SetTrigger("night");

        Sky.SetTrigger("night");
        Sky.Play("skyTransition");

        foreach (var decoration in Decorations) decoration.Play("decorationNight");
        foreach (var cloud in Clouds) cloud.Play("cloudNight");
    }
}
