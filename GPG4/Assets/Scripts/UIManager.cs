using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("=== SCORE AND LIFE ===")]
    public GameObject scorePanel, lifePanel;
    public TextMeshProUGUI scoreVal, lifeVal;
    public int health = 10;
    public int score;
    public int currentHealth;

    [Header("=== PLAY GAME ===")]
    public GameObject startPanel;
    public Button playBtn;

    [Header("=== GAME OVER ===")]
    public GameObject GameOverPanel;
    public Button restartButton;
    public Button exitBtn;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        Time.timeScale = 0;
        scorePanel.SetActive(false);
        lifePanel.SetActive(false);
        lifeVal.text = health.ToString();
        scoreVal.text = score.ToString();
        restartButton.onClick.AddListener(RestartGame);
        playBtn.onClick.AddListener(PlayButton);
        exitBtn.onClick.AddListener(ExitGame);
    }

    public void ReduceHealth()
    {
        if(health <= 0)
        {
            GameOver();
        }
        else
        {
            health--;
            lifeVal.text = health.ToString();
        }
    }

    public void AddScore()
    {
        score++;
        scoreVal.text = score.ToString();
    }

    void GameOver()
    {
        scorePanel.SetActive(false);
        lifePanel.SetActive(false);
        GameOverPanel.SetActive(true);

        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
    }

    void PlayButton()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
        scorePanel.SetActive(true);
        lifePanel.SetActive(true);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
