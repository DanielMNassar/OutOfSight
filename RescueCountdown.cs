using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RescueCountdownWithIntro : MonoBehaviour
{
    public TextMeshProUGUI getReadyText;
    public TextMeshProUGUI timerText;
    public Image blackScreen;

    public float countdownTime = 120f;

    [Header("Visual Settings")]
    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float warningThreshold = 15f;
    public float fadeDuration = 1.5f;
    public float getReadyDuration = 2f;

    private float currentTime;
    private bool isFlashing = false;
    private bool countdownStarted = false;

    void Start()
    {
        timerText.alpha = 0f;

        // Setup black screen fully opaque
        blackScreen.color = new Color(0, 0, 0, 1f);
        getReadyText.alpha = 1f;
        getReadyText.text = "Get Ready...";

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // Wait before fade
        yield return new WaitForSeconds(getReadyDuration);

        // Fade out black screen and "Get Ready..." at same time
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            blackScreen.color = new Color(0f, 0f, 0f, alpha);
            getReadyText.alpha = alpha;

            yield return null;
        }

        blackScreen.gameObject.SetActive(false);
        getReadyText.gameObject.SetActive(false);

        // Start countdown
        currentTime = countdownTime;
        countdownStarted = true;
        StartCoroutine(FadeInTimer());
    }

    void Update()
    {
        if (!countdownStarted || currentTime <= 0f) return;

        currentTime -= Time.deltaTime;
        UpdateTimerDisplay();

        if (currentTime <= warningThreshold && !isFlashing)
        {
            StartCoroutine(FlashWarningColor());
            isFlashing = true;
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator FadeInTimer()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            timerText.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
    }

    IEnumerator FlashWarningColor()
    {
        float pulseSpeed = 2f;
        while (currentTime > 0f && currentTime <= warningThreshold)
        {
            float lerp = Mathf.PingPong(Time.time * pulseSpeed, 1f);
            timerText.color = Color.Lerp(warningColor, normalColor, lerp);
            yield return null;
        }

        timerText.color = warningColor;
    }
}
