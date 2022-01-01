using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
class SoundController : MonoBehaviour
{
    public bool isMusic, restartOnLoad;
    public float startLoop, endLoop;

    public AudioClip[] Audio;
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (!restartOnLoad) source.time = PhaseConfiguration.lastTime;
        if (isMusic) source.Play();
    }

    private void FixedUpdate()
    {
        if (isMusic && source.time >= endLoop) 
        {
            Debug.Log("teste");
            source.time = startLoop;
        }
    }

    public void StartFade(float targetVolume, float duration)
    {
        StartCoroutine(Fade(targetVolume, duration));
    }

    public void Play(AudioClip audio)
    {
        source.PlayOneShot(audio);
    }

    public void PlayAsMusic()
    {
        isMusic = true;
        source.Play();
    }

    public void Stop()
    {
        source.Pause();
    }

    public float ReturnActualTime()
    {
        return source.time;
    }

    IEnumerator Fade(float targetVolume, float duration)
    {
        float start = source.volume;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        Stop();
        source.volume = start;
        source.time = 0;
        yield break;
    }
}
