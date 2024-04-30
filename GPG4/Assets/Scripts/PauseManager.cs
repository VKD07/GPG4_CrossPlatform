using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [Header("PAUSE")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] Button pauseBtn;
    [SerializeField] Button resumeBtn;
    [SerializeField] Button optionsBtn;
    [SerializeField] Button exitBtn;

    [Header("OPTIONS")]
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Button goBackBtn;
    void Start()
    {
        pauseBtn.onClick.AddListener(PauseGame);
        resumeBtn.onClick.AddListener(ResumeGame);
        exitBtn.onClick.AddListener(ExitGame);

        //Options
        optionsBtn.onClick.AddListener(OptionsBtn);
        goBackBtn.onClick.AddListener(GoBack);
    }

    void PauseGame()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
        pausePanel.SetActive(true);
    }

    void ResumeGame()
    {
        if (Time.timeScale <= 0)
        {
            Time.timeScale = 1;
        }
        pausePanel.SetActive(false);
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void OptionsBtn()
    {
        optionsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    void GoBack()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
