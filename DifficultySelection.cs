using UnityEngine;
using TMPro;

public class DifficultySelection : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public float easyTime = 180f;
    public float mediumTime = 120f;
    public float hardTime = 60f;

    public IntroSequenceManager introManager;

    public void SelectEasy() => SelectDifficulty(easyTime, "So... taking the safe route? 👀");
    public void SelectMedium() => SelectDifficulty(mediumTime, "Fair warning… it's not what it seems.");
    public void SelectHard() => SelectDifficulty(hardTime, "Are you sure? Some never came back...");

    private void SelectDifficulty(float time, string message)
    {
        messageText.text = message;
        PlayerPrefs.SetFloat("GameTimer", time);

        introManager.StartIntro();
    }
}
