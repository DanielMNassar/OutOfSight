using UnityEngine;
using UnityEngine.UI;

public class FlashlightBatteryUI : MonoBehaviour
{
    public Slider batterySlider;
    public Image fillImage;
    public Light flashlight;

    public float maxBattery = 100f;
    public float drainRate = 10f;
    public float rechargeRate = 5f;

    public Color fullColor = Color.green;
    public Color mediumColor = Color.yellow;
    public Color lowColor = Color.red;

    private float currentBattery;
    private bool flashlightOn = false;

    void Start()
    {
        currentBattery = maxBattery;
        batterySlider.maxValue = maxBattery;
        batterySlider.value = currentBattery;
    }

    void Update()
    {
        HandleFlashlightToggle();
        UpdateBattery();
        UpdateUI();
    }

    void HandleFlashlightToggle()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightOn = !flashlightOn;
            flashlight.enabled = flashlightOn;
        }
    }

    void UpdateBattery()
    {
        if (flashlightOn)
        {
            currentBattery -= drainRate * Time.deltaTime;
        }
        else
        {
            currentBattery += rechargeRate * Time.deltaTime;
        }

        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);

        if (currentBattery <= 0f && flashlightOn)
        {
            flashlightOn = false;
            flashlight.enabled = false;
        }
    }

    void UpdateUI()
    {
        batterySlider.value = currentBattery;

        float percent = currentBattery / maxBattery;

        if (percent < 0.5f)
        {
            fillImage.color = Color.Lerp(lowColor, mediumColor, percent * 2f);
        }
        else
        {
            fillImage.color = Color.Lerp(mediumColor, fullColor, (percent - 0.5f) * 2f);
        }
    }
}
