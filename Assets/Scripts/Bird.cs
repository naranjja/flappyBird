using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour {

    public Rigidbody2D rb;              // ref to bird's rb
    public float moveSpeed;             // move speed (public for editor)
    public float flappingHeight;        // flapping height (public for editor)
    public GameObject pipe_up;          // ref to pipe_up game object
    public GameObject pipe_down;        // ref to pipe_down game object
    public int screenHeight;
    private int score;
    public Text scoreText;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private bool isGameOver;

    void Start () {
        // get ref from bird
        rb = GetComponent<Rigidbody2D>();
        BuildLevel();
        score = 0;
        SetScoreText();
        isGameOver = false;
        gameOverPanel.SetActive(false);
    }

    void SetScoreText() {
        scoreText.text = "Score: " + score.ToString();
    }

    // TODO: this is dirty, would be better to have a game manager script
    void BuildLevel () {
        float randomPipe1Y = Random.Range(3f, 5f);
        float randomPipe2Y = Random.Range(-1.5f, 4f);

        Instantiate(pipe_down, new Vector3(6, randomPipe1Y), transform.rotation);
        Instantiate(pipe_up, new Vector3(6, randomPipe1Y-12), transform.rotation);

        Instantiate(pipe_down, new Vector3(14, randomPipe2Y), transform.rotation);
        Instantiate(pipe_up, new Vector3(14, randomPipe2Y-12), transform.rotation);
    }
	
	void Update () {

        if (isGameOver) {
            if (Input.GetKeyDown("space"))
            {
                // reset position back to 0,0
                transform.position = new Vector2(0, 0);

                // reload scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
                
        } else {
            score = (int)(transform.position.x * 5);
            SetScoreText();

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
            if (transform.position.y > screenHeight || transform.position.y < -screenHeight)
                GameOver();
        }

	}

    // public means we can access it from other scripts
    public void GameOver() {

        isGameOver = true;

        gameOverPanel.SetActive(true);
        gameOverText.text = score.ToString();

        // remove all velocity (to be stable when respawning)
        rb.velocity = Vector3.zero;

        //gameObject.GetComponent<Renderer>().enabled = false;

    }
}
