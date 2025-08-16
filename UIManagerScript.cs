using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject SettingsPanel;
    public GameObject playPanel;
    public GameObject gameplayRoot;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        playPanel.SetActive(false);
        gameplayRoot.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        playPanel.SetActive(false);
    }

    public void ShowPlayPanel()
    {
        Debug.Log("ShowPlayPanel called");
        mainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("StartGame called");
        mainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        playPanel.SetActive(false);
        gameplayRoot.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    

}
