using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject tutorialsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private AudioSource clickSound;
    [SerializeField] private TextMeshProUGUI collectibleCounter;



    private void Start()
    {
        mainMenuPanel.SetActive(true);
        tutorialsPanel.SetActive(false);
        settingsPanel.SetActive(false); 
        shopPanel.SetActive(false); 
        leaderboardPanel.SetActive(false);
        collectibleCounter.text = "x"+PlayerPrefs.GetInt("collectibleCount").ToString();

    }
    public void SelectLevel()
    {
        clickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        clickSound.Play();
        Application.Quit();
    }

    public void DisplayPanels()
    {
        clickSound.Play();
        mainMenuPanel.SetActive(false);
        if (EventSystem.current.currentSelectedGameObject.name == "TutorialButton")
        {
            tutorialsPanel.SetActive(true);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "SettingsButton")
        {
            settingsPanel.SetActive(true);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "LeaderBoardButton")
        {
            leaderboardPanel.SetActive(true);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ShopButton")
        {
            shopPanel.SetActive(true);
        }

    }

    public void CrossButtons()
    {
        clickSound.Play();
        mainMenuPanel.SetActive(true);
        if (EventSystem.current.currentSelectedGameObject.name == "TutorialsCrossButton")
        {
            tutorialsPanel.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "SettingsCrossButton")
        {
            settingsPanel.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ShopCrossButton")
        {
            shopPanel.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "LBCrossButton")
        {
            leaderboardPanel.SetActive(false);
            
        }
    }
}
