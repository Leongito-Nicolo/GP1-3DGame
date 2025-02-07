using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isGamePaused = false;             // boolean to check if the game is paused
    public bool hasPlayerWon;                                       // boolean to check if player has won

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private OrbSpawner orbSpawner;

    [SerializeField] private GameObject pauseMenu;                  // gameobject of the pause menu

    [SerializeField] private GameObject gameOverMenu;               // gameobject of the game over menu
    [SerializeField] private TMP_Text showStatus;                   // text gameobject of the status
    [SerializeField] private TMP_Text showScore;                    // text gameobject of the final score
    [SerializeField] private TMP_Text invincibleText;               // text gameobject of invincibility

    [SerializeField] private TMP_Text timer;                        // text gameobject of the timer
    [SerializeField] private float maxTime;                         // max timer

    [SerializeField] private TMP_Text score;                        // text gameobject of the score

    [SerializeField] private float powerDuration;                   // duration of the power up

    public int totalEnemies;
    public int totalOrbs;

    private void Start()
    {
        Time.timeScale = 1;
        enemySpawner.SpawnEnemies();
        orbSpawner.SpawnOrbs();
    }

    // Update is called once per frame
    void Update()
    {
        //if Esc is pressed it opens the pause menu, or close it if it was already pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            ResumeGame();
        }

        StartTimer();


        if (maxTime <= 0 || playerHealth.playerCurrentHealth <= 0 || hasPlayerWon)
        {
            GameOver();
        }
    }

    // function to stop time and open pause menu
    public void PauseGame()
    {
        isGamePaused = true;        // set to true
        Time.timeScale = 0;     // stop time
        Cursor.lockState = CursorLockMode.None;     // activate cursor to click buttons
        pauseMenu.SetActive(true);          // activate pause menu
    }

    // function to stop the game and open the game over menu
    public void GameOver()
    {
        Time.timeScale = 0;     // stop time
        Cursor.lockState = CursorLockMode.None;     // activate cursor to click buttons
        gameOverMenu.SetActive(true);
        ShowScoreAndStatus();
    }

    // function to show score and status
    public void ShowScoreAndStatus()
    {
        if (hasPlayerWon)
        {
            showStatus.text = "You won!";
            showScore.text = string.Format("Score: {0:D8}", playerController.playerScore + (int)maxTime);      // format the text to match score
        }
        else
        {
            showStatus.text = "You lost!";
            showScore.text = string.Format("Score: {0:D8}", playerController.playerScore);      // format the text to match score
        }
    }

    // function to resume time and close pause menu
    public void ResumeGame()
    {
        isGamePaused = false;       // set to false
        Time.timeScale = 1;     // resume time
        Cursor.lockState = CursorLockMode.Locked;       // lock the cursor again
        pauseMenu.SetActive(false);     // deactivate pause menu
    }

    // function to replay the game
    public void ReplayGame()
    {
        gameOverMenu.SetActive(false);
        SceneManager.LoadScene("Game");     // load the scene game
    }

    // function to go back to menu
    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");     // load scene Menu
    }

    // function to start a timer
    public void StartTimer()
    {
        maxTime -= Time.deltaTime;      // decrease timer
        int minutes = Mathf.FloorToInt(maxTime / 60f);      // format to minutes
        int seconds = Mathf.FloorToInt(maxTime % 60f);      // format to seconds
        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");     // set the text of the timer
    }

    // function to update score
    public void UpdateScore(int scoreToAdd)
    {
        playerController.playerScore += scoreToAdd;     // add the score to the player
        score.text = string.Format("Score: {0:D8}", playerController.playerScore);      // format the text to match score
    }

    // coroutine to apply invincibility to the player as a power up
    public IEnumerator Invincibility()
    {
        playerHealth.isInvincible = true;       // set isInvincible to true
        invincibleText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(powerDuration);     // wait for powerDuration seconds
        invincibleText.gameObject.SetActive(false);
        playerHealth.isInvincible = false;      // set isInvincible to false
    }
}
