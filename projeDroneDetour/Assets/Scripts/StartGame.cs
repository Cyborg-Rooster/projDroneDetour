using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioCollection;
    AudioSource audioSource;


    //quando acordar, vai usar o metodo waked do sqlite
    void Awake()
    {
        SQLiteConstructor.Waked();
    }

    // Start is called before the first frame update
    void Start()
    {
        //le os arquivos salvos
        PlayerPrefs.LoadData();

        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.options[0] == "0")
            AudioListener.volume = 0f;

        else if (PlayerPrefs.options[0] == "1")
            AudioListener.volume = 1f;

        StartCoroutine("StartTheGame");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("sceMenu");
    }

    // Update is called once per frame
    IEnumerator StartTheGame()
    {
        yield return new WaitForSeconds(1f);
        audioSource.clip = audioCollection[0];
        audioSource.Play();

        yield return new WaitForSeconds(2.08f);
        audioSource.clip = audioCollection[1];
        audioSource.Play();

        yield return new WaitForSeconds(0.70f);
        SceneManager.LoadScene("sceMenu");
    }
}
