using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool isGameOver = false;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject newHSImg;
    [SerializeField] private GameObject countDownPanel;
    public Button pauseButton;
    private GameManager scoreCounter;
    private int gameOverHS;
    private int timer = 3;
    [SerializeField] GameObject[] disableObjects;
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private TextMeshProUGUI gameOverHighScore;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI pauseScore;
    [SerializeField] private TextMeshProUGUI pauseHighScore;
    [Header ("Sounds")]
    [SerializeField] private AudioSource countdownSound;
    [SerializeField] private AudioSource countEndSound;
    [SerializeField] private AudioSource clickSound;





    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        newHSImg.SetActive(false);
        gameOverHS = PlayerPrefs.GetInt("HighScore");
        scoreCounter = FindObjectOfType<GameManager>();
        pauseButton.interactable = true;
        StartCoroutine(CountTimer());
        for(int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(true);
        }
        
    }


    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
            for (int i = 0; i < disableObjects.Length; i++)
            {
                disableObjects[i].SetActive(false);
            }
            //newHSImg.SetActive(true);
            if (scoreCounter.score > gameOverHS)
            {
                newHSImg.SetActive(true);
            }
            gameOverScore.text = "Score: " + PlayerPrefs.GetInt("Score");
            gameOverHighScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
            Time.timeScale = 0f;
            pauseButton.interactable = false;

        }
    }

    public void Restart()
    {
        clickSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverPanel.SetActive(false);
        isGameOver = false;
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(true);
        }
        StartCoroutine(CountTimer());
        Debug.Log("Game Has Restarted");
    }

    public void PauseGame()
    {
        clickSound.Play();
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(false);
        }
        pauseScore.text = "Score: " + PlayerPrefs.GetInt("Score");
        pauseHighScore.text = "Score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void ResumeGame()
    {
        clickSound.Play();
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        Debug.Log("Game Has Resumed");
        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(true);
        }
    }

    public void LoadMenu()
    {
        clickSound.Play();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Menu has been Loaded");
    }

    public void LoadSettings()
    {
        clickSound.Play();
        Time.timeScale = 0f;
        settingsPanel.SetActive(true);
        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(false);
        }
    }

    private IEnumerator CountTimer()
    {
        countDownPanel.SetActive(true);
        while (timer > 0)
        {
            countdownSound.Play();
            Time.timeScale = 0f;
            countDownText.text = timer.ToString();
            timer--;
            yield return new WaitForSecondsRealtime(1f);
            //Debug.Log("counting");
        }
        countEndSound.Play();
        countDownPanel.SetActive(false);
        Time.timeScale = 1f;
        yield return null;
    }
    #region temporary testing
    public void ResetKey()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
    #endregion
}
