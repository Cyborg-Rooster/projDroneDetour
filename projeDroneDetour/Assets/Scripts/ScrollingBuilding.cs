using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBuilding : MonoBehaviour
{
    bool canScore = true; 

    public Sprite[] sprite = new Sprite[4];
    SpriteRenderer sRenderer;

    Rigidbody2D rb;

    GameManager game;

    Vector2 drone;

    AudioSource audioSource;


    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

        sRenderer = GetComponent<SpriteRenderer>();
        sRenderer.sprite = sprite[Random.Range(0, 4)];

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(game.buildVelocity, 0);

        drone = GameObject.Find("Drone").transform.position;

        transform.position = new Vector3(transform.position.x, Random.Range(-0.46f, 0.46f));
    }

    // Update is called once per frame
    void Update()
    {
        //faz os predios se mexerem ou não se o jogo começou
        if (game.start && game.isAlive)
        {
            game.buildVelocity = -0.7f;
            rb.velocity = new Vector2(game.buildVelocity, 0);
        }
        else if(game.cannotBuildingScroll || !game.start || !game.isAlive)
        {
            game.buildVelocity = 0f;
            rb.velocity = new Vector2(game.buildVelocity, 0);
        }

        //se o predio pode pontuar, quando chegar no mesmo x drone ira pontuar
        if(canScore && transform.position.x <= drone.x)
        {
            game.score++;
            game.canRain = true;
            game.canChange = true;
            canScore = false;
            audioSource.Play();
        }

        //quando chegar no x especifico, voltará para o lugar inicial com outro sprite e podendo
        //pontuar
        if(gameObject.transform.position.x <= -0.941f)
        {
            transform.position = new Vector3(2.059f, Random.Range(-0.46f, 0.46f));
            sRenderer.sprite = sprite[Random.Range(0, 4)];
            canScore = true;
        }
    }
}
