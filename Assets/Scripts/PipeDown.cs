using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// most of the logic is handled in PipeUp.cs

public class PipeDown : MonoBehaviour {

    private Bird bird;                  // ref to bird in scene

    void Start () {
        bird = FindObjectOfType<Bird>();
	}
	

	void Update () {

        if (bird.transform.position.x - transform.position.x > PipeUp.availableLeftSpace)
            Destroy(gameObject);
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        // if pipe_up collides with Player tag
        if (collision.gameObject.tag == "Player")
            bird.GameOver();
            //Destroy(gameObject);
    }
}
