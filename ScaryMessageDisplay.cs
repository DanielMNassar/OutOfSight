using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ScaryMessageSystem : MonoBehaviour
{
    [Header("TextMeshPro UI")]
    public TextMeshProUGUI scaryText;

    [Header("Timing Settings")]
    public float messageDuration = 4f;      // How long the message stays visible
    public float fadeDuration = 2f;         // How long the message fades out
    public float initialDelay = 5f;         // Delay before first auto message
    public float interval = 15f;            // Time between messages

    [Header("Message Pool")]
    public List<string> scaryMessages = new List<string>()
    {
        "Don’t turn around.",
        "You’re not alone.",
        "They see you.",
        "Run...",
        "It moved again.",
        "You won’t survive.",
        "Why are you here?",
        "You hear that, right?",
        "Your time is almost up.",
        "You’re not getting out."
    };

    private Coroutine autoMessageRoutine;
    private Coroutine fadeMessageRoutine;

    void Start()
    {
        // Hide text at start
        scaryText.text = "";
        scaryText.alpha = 0f;

        // Start auto message loop
        autoMessageRoutine = StartCoroutine(AutoSpawnMessages());
    }

    /// <summary>
    /// Show a scary message (manual or automatic)
    /// </summary>
    public void ShowMessage(string message)
    {
        if (fadeMessageRoutine != null)
            StopCoroutine(fadeMessageRoutine);

        scaryText.text = message;
        scaryText.alpha = 1f;
        fadeMessageRoutine = StartCoroutine(FadeOutMessage());
    }

    /// <summary>
    /// Stops all automatic messages
    /// </summary>
    public void StopAutoMessages()
    {
        if (autoMessageRoutine != null)
        {
            StopCoroutine(autoMessageRoutine);
            autoMessageRoutine = null;
        }
    }

    /// <summary>
    /// Coroutine to fade the message out
    /// </summary>
    private IEnumerator FadeOutMessage()
    {
        yield return new WaitForSeconds(messageDuration);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            scaryText.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        scaryText.text = "";
    }

    /// <summary>
    /// Automatically shows random messages during gameplay
    /// </summary>
    private IEnumerator AutoSpawnMessages()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            string randomMessage = scaryMessages[Random.Range(0, scaryMessages.Count)];
            ShowMessage(randomMessage);
            yield return new WaitForSeconds(interval);
        }
    }
}
