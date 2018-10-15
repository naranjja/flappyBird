using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeUp : MonoBehaviour {

    private Bird bird;                  // ref to bird in scene
    public GameObject pipe_down;        // ref to pipe_down game object
    public static float availableLeftSpace = 3;

	void Start () {
        bird = FindObjectOfType<Bird>();
	}
	
	void Update () {
        // if the distance between bird and pipe is more than 30 pixels,
        // meaning the pipe's are off the screen
        // TODO: find real width of screen to the left
        if (bird.transform.position.x - transform.position.x > availableLeftSpace)
        {
            // TODO: make function to generate these random values and return Vector2
            //float randomX = Random.Range(0, 10);
            float randomY = Random.Range(-5, 5);
            float randomGap = Random.Range(0, 3);

            // when using "gameObject", Unity refers to the game object that this script is attached to
            // this will instantiate a pipe_up at x + 30 + randomX, -11 + randomY
            Instantiate(gameObject, new Vector2(bird.transform.position.x + 8, -11 + randomY), transform.rotation);

            // instantiate pipe_down as well so that it aligns at its bottom
            // TODO: this is dirty, better to use a pipe manager to spawn pipes
            Instantiate(pipe_down, new Vector2(bird.transform.position.x + 8, randomGap + randomY), transform.rotation);

            // Destroy the old pipe_up when out of screen
            Destroy(gameObject);

        }
        
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        // if pipe_up collides with Player tag
        if (collision.gameObject.tag == "Player")
            bird.GameOver();
            Destroy(gameObject);
    }
}
