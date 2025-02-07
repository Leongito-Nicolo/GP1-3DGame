using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;           // instance of the health slider

    [SerializeField] private PlayerHealth playerHealth;     // instance of player health

    [SerializeField] private GameManager gameManager;       // instance of game manager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.maxValue = playerHealth.playerMaxHealth;       // set the max value of the slider to the player max health
        healthSlider.value = playerHealth.playerMaxHealth;          // set the current value of the slider to the player max health
    }

    // function to update player health every time he takes damage
    public void UpdateHealthBar()
    {
        healthSlider.value = playerHealth.playerCurrentHealth;      // set the slider value to the current player health
    }

    // function for resume button
    public void ResumeButton()
    {
        gameManager.ResumeGame();
    }

    // function to replay the game
    public void Replay()
    {
        gameManager.ReplayGame();
    }

    // function for exit button
    public void ExitButton()
    {
        gameManager.ExitGame();
    }
}
