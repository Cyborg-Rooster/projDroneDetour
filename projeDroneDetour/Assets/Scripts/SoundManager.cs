using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public bool isPlaying;
    float currentTime = 0;
    float start;
    public float duration;

    public float startLooping;

    [SerializeField]
    AudioClip[] music;
    AudioSource audioSource;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Sound Manager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    public void StartMusic()
    {
        audioSource = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "sceMenu")
        {
            startLooping = 1.71f; 
            audioSource.clip = music[0];
            audioSource.Play();
        }
        else if(SceneManager.GetActiveScene().name == "sceGame")
        {
            startLooping = 75.28f;
            audioSource.clip = music[1];
            audioSource.Play();
        }

        isPlaying = true;
    }

    public IEnumerator StartFade(float targetVolume)
    {
        start = audioSource.volume;
        currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "sceMenu" && audioSource.time > 22.28f)
        {
            audioSource.time = startLooping;
            audioSource.Play();
        }
        else if(SceneManager.GetActiveScene().name == "sceGame" && audioSource.time > 155.73f)
        {
            audioSource.time = startLooping;
            audioSource.Play();
        }

    }
}
