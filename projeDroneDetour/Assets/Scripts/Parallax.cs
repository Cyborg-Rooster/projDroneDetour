using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float velocity;

    GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.isAlive)
        {
            gameObject.transform.Translate(Vector2.right * velocity * Time.deltaTime);

            if (transform.position.x <= -2.28f)
                transform.position = new Vector3(4.92f, transform.position.y);
        }
    }
}
