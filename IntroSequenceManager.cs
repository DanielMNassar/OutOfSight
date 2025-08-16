using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class IntroSequenceManager : MonoBehaviour
{
    public GameObject blackPanel;
    public TextMeshProUGUI getReadyText;
    public GameObject gamePlayRoot;

    public GameObject mainMenuPanel;
    public GameObject playPanel;

    public float introDuration = 2f;

    public void StartIntro()
    {
        StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        // Hide menu panels
        mainMenuPanel.SetActive(false);
        playPanel.SetActive(false);

        // Show black screen + text
        blackPanel.SetActive(true);
        getReadyText.text = "Get Ready...";
        getReadyText.alpha = 1f;

        yield return new WaitForSeconds(introDuration);

        // Hide overlay and enable gameplay
        blackPanel.SetActive(false);
        gamePlayRoot.SetActive(true);
    }
}
