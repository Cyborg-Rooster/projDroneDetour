using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionController : MonoBehaviour
{
    [SerializeField] SoundController soundController;
    Coroutine startGame;
    private void Awake()
    {
        DatabaseController.InitiateDatabase();
        Configuration.SetSounds();
    }

    private void Start()
    {
        StartCoroutine(PlaySounds());
        startGame = StartCoroutine(StartGame());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StopCoroutine(startGame);
            SceneManager.GoToScene("sceMain");
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2.2f);
        SceneManager.GoToScene("sceMain");
    }

    IEnumerator PlaySounds()
    {
        soundController.Play(soundController.Audio[0]);
        yield return new WaitForSeconds(1.9f);
        soundController.Play(soundController.Audio[1]);
    }

}
