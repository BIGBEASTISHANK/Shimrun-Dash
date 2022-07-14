using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // VARIABLES
    private float ogSpeed;
    private bool isGrounded, isControlling, lvlComplete, alreadyPaused;

    private Vector2 move;

    [SerializeField] private float jumpHeight, moveSpeed, gameOverSpeed;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private GameObject deathParticle, finishParticle, gameOverTxt, levelCompleteTxt, pauseMenu;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Rigidbody2D rb2d;

    // FUNCTIONS
    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();

        ogSpeed = moveSpeed;
        isControlling = true;

        gameOverTxt.SetActive(false);
        finishParticle.SetActive(false);
        levelCompleteTxt.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Move player
        if (isControlling) Move();
        if(Input.GetKeyDown(KeyCode.Escape)) PauseGame();
    }

    // Move Script
    private void Move()
    {
        // Slowdown
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveSpeed = ogSpeed / 4;
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) moveSpeed = ogSpeed;

        // Fastforward
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveSpeed = ogSpeed * 1.2f;
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) moveSpeed = ogSpeed;

        // Lowjump
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move.y = jumpHeight / 2;
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) move.y = 0;

        // Move
        move = new Vector2(1, 0);
        move.x *= moveSpeed;

        // Jump
        if (isGrounded) Jump();

        // Apply Force
        rb2d.AddForce(move * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Jump Function
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            move.y = 1;
            move.y *= jumpHeight;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) move.y = 0;
    }

    // Gameover Function
    public void GameOver()
    {
        isControlling = false;
        moveSpeed = gameOverSpeed;

        sr.enabled = false;
        tr.enabled = false;

        deathParticle.SetActive(true);
        gameOverTxt.SetActive(true);

        Invoke("ReloadScene", 1.5f);
    }

    // Reloading Scene Function
    private void ReloadScene()
    {
        transform.position = respawnPoint.position;

        isControlling = true;
        moveSpeed = ogSpeed;

        sr.enabled = true;
        tr.enabled = true;

        deathParticle.SetActive(false);
        gameOverTxt.SetActive(false);
    }

    // Levelcomplete Function
    private void LevelComplete()
    {
        isControlling = false;
        lvlComplete = true;

        levelCompleteTxt.SetActive(true);
        finishParticle.SetActive(true);

        Invoke("NextScene", 1.5f);
    }

    private void NextScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

    // Pause Menu
    private void PauseGame()
    {
        if(alreadyPaused)
        {
            pauseMenu.SetActive(false);
            alreadyPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            alreadyPaused = true;
        }
    }

    // Is Player Grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") isGrounded = true;

        if (collision.gameObject.tag == "obsticle" && lvlComplete == false) GameOver();

        if (collision.gameObject.tag == "Finish") LevelComplete();
    }

    // Has Player exited ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") isGrounded = false;
    }
}
