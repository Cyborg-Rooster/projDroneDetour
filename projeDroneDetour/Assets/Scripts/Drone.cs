using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    //variaveis publicas
    public float force;
    public float deviceForce;

    float touchRate = 0.3f;
    float nextTouch;
    int nClicks = 0;

    //variaveis privadas
    bool turnOn = true;

    //objetos
    [SerializeField]
    AudioClip[] audioCollection;

    Rigidbody2D rb;
    Animator anim;

    AudioSource audioSource;
    GameManager game;
    ScrollingBuilding predio;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void FixedUpdate()
    {
        //liga ou desliga a fisica do drone se o jogo está rodando ou não
        if (game.start)
            rb.bodyType = RigidbodyType2D.Dynamic;
        else
            rb.bodyType = RigidbodyType2D.Static;

        //se o jogador estiver vivo, vai sempre deixar a velocidade da animação normal
        if (game.isAlive)
        {
            anim.speed = 1;
        }

        //se o jogo estiver pausado, vai pausar a animação
        else if (!game.isAlive && game.isPause)
            anim.speed = 0;
        

        //checa o dispositivo que ele ta, se for no nada enquanto o jogo estiver rodando
        //ele vai saltar
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            if (Input.GetMouseButtonDown(0) && turnOn)
            {
                Vector3 pos = Input.mousePosition;
                Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
                if (game.start)
                {
                    if (hitCollider == null || !hitCollider.CompareTag("Button"))
                    {
                        rb.velocity = Vector2.zero;
                        rb.AddForce(Vector2.up * force);

                        nClicks++;
                        audioSource.clip = audioCollection[0];
                        audioSource.Play();
                    }
                }
            }
        }

        else if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount > 0 && turnOn)
            {
                Vector3 pos = Input.GetTouch(0).position;
                Collider2D hitCollider = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));

                if (game.start && Time.time > nextTouch )
                {
                    if (hitCollider == null || !hitCollider.CompareTag("Button"))
                    {
                        rb.velocity = Vector2.zero;
                        rb.AddForce(Vector2.up * deviceForce);
                        nextTouch = Time.time + touchRate;
                        
                        nClicks++;
                        audioSource.clip = audioCollection[0];
                        audioSource.Play();
                    }
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Predio")
        {
            PlayerPrefs.statistics[1] = (int.Parse(PlayerPrefs.statistics[1]) + 1).ToString();
            PlayerPrefs.statistics[3] = (int.Parse(PlayerPrefs.statistics[3]) + 1).ToString();
            StartCoroutine("Death");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Explode")
        {
            PlayerPrefs.statistics[1] = (int.Parse(PlayerPrefs.statistics[1]) + 1).ToString();
            PlayerPrefs.statistics[2] = (int.Parse(PlayerPrefs.statistics[2]) + 1).ToString();
            StartCoroutine("Death");
        }

        else if (collision.gameObject.tag == "TurnOff")
        {

            audioSource.clip = audioCollection[2];
            audioSource.Play();

            game.isAlive = false;

            game.buildVelocity = 0f;

            turnOn = false;
        }
    }

    IEnumerator Death()
    {
        rb.bodyType = RigidbodyType2D.Static;

        game.droneIsDestroyed = true;

        PlayerPrefs.statistics[4] = (int.Parse(PlayerPrefs.statistics[4]) + nClicks).ToString(); 

        game.isAlive = false;
        game.start = false;
        game.cannotBuildingScroll = false;

        anim.SetTrigger("morreu");

        audioSource.clip = audioCollection[1];
        audioSource.Play();

        yield return new WaitForSeconds(0.375f);
        anim.speed = 0;

        SpriteRenderer render = GetComponent<SpriteRenderer>();
        Color color = render.color;
        color.a = 0;
        render.color = color;

        yield return new WaitForSeconds(0.801f);

        Destroy(gameObject);
    }


}
