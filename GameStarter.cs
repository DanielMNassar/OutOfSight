using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject playPanel;
    public GameObject settingsPanel;
    public GameObject gameplayRoot;

    public float easyTime = 180f;
    public float mediumTime = 120f;
    public float hardTime = 60f;

    void Start()
    {
        // Start with only the main menu visible
        mainMenuPanel.SetActive(true);
        playPanel.SetActive(false);
        settingsPanel.SetActive(false);
        gameplayRoot.SetActive(false);
    }

    public void OpenPlayPanel()
    {
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        playPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SelectEasy() => StartGame(easyTime, "So... taking the safe route? 👀");
    public void SelectMedium() => StartGame(mediumTime, "Fair warning… it's not what it seems.");
    public void SelectHard() => StartGame(hardTime, "Are you sure? Some never came back...");

    void StartGame(float timer, string message)
    {
        Debug.Log(message); // Optionally show message in a UI Text
        PlayerPrefs.SetFloat("GameTimer", timer);

        // Hide all UI panels
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(false);
        settingsPanel.SetActive(false);

        // Show gameplay
        gameplayRoot.SetActive(true);
    }
}
