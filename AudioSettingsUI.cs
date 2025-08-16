using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public void OnMusicChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        AudioListener.volume = value; // You can link this to an AudioMixer instead
    }

    public void OnSFXChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        // You can update individual SFX sources here later
    }
}
