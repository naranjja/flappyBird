using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    public Rigidbody2D rb;              // ref to bird's rb
    public float moveSpeed;             // move speed (public for editor)
    public float flappingHeight;        // flapping height (public for editor)
    public GameObject pipe_up;          // ref to pipe_up game object
    public GameObject pipe_down;        // ref to pipe_down game object

	void Start () {
        // get ref from bird
        rb = GetComponent<Rigidbody2D>();
        BuildLevel();
	}

    // TODO: this is dirty, would be better to have a game manager script
    void BuildLevel () {
        Instantiate(pipe_down, new Vector3(5, 5), transform.rotation);
        Instantiate(pipe_up, new Vector3(5, -7), transform.rotation);
    }
	
	void Update () {
        // move the bird constantly to the right
        // keeping the y-coord the same
        rb.velocity = new Vector3(moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown("space"))
            // move the bird flapping height up
            // keeping the x-coord the same
            rb.velocity = new Vector2(rb.velocity.x, flappingHeight);

        // transform has the bird's position property
        // if y > 10 or < -10, then the player is out of bounds
        // TODO: find the height of the screen
        if (transform.position.y > 20 || transform.position.y < -20)
            GameOver();
	}

    // public means we can access it from other scripts
    public void GameOver() {
        // remove all velocity (to be stable when respawning)
        rb.velocity = Vector3.zero;

        // reset position back to 0,0
        transform.position = new Vector2(0, 0);

        BuildLevel();
    }
}
