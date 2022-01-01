using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudyController
{
    static Animator Cloudy;
    static Animator Rain;
    static BackgroundController BackgroundController;

    public static void AddControllers(Animator cloudy, Animator rain, BackgroundController controller)
    {
        Cloudy = cloudy;
        Rain = rain;
        BackgroundController = controller;
    }

    public static void SetRain(bool rain, bool day)
    {
        if (rain) BackgroundController.StartCoroutine(StartCloudy(day));
        else BackgroundController.StartCoroutine(StopCloudy(day));
    }

    static IEnumerator StartCloudy(bool day)
    {
        Cloudy.gameObject.SetActive(true);
        
        if(day)
        {
            Cloudy.SetTrigger("day");
            Cloudy.Play("startRainDay");
        }
        else
        {
            Cloudy.SetTrigger("night");
            Cloudy.Play("startRainNight");
        }

        yield return new WaitForSeconds(1.4f);
        StartRain();
    }

    static IEnumerator StopCloudy(bool day)
    {
        yield return StopRain();

        if (day) Cloudy.SetTrigger("day");
        else Cloudy.SetTrigger("night");

        Cloudy.SetBool("stop", true);

        yield return new WaitForSeconds(1.4f);
        Cloudy.SetBool("stop", false);
        Cloudy.gameObject.SetActive(false);
    }

    static void StartRain()
    {
        Rain.gameObject.SetActive(true);
        Rain.Play("start");
    }

    static IEnumerator StopRain()
    {
        Rain.SetTrigger("end");
        yield return new WaitForSeconds(0.53f);
        Rain.gameObject.SetActive(false);
    }
}
