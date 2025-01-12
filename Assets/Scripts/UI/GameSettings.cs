using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.0f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.0f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.0f);

        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0.0f));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0.0f));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume", 0.0f));

        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void ResetSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", 0.0f);
        PlayerPrefs.SetFloat("MusicVolume", 0.0f);
        PlayerPrefs.SetFloat("SFXVolume", 0.0f);
        masterVolumeSlider.value = 0.0f;
        musicVolumeSlider.value = 0.0f;
        sfxVolumeSlider.value = 0.0f;
    }



}
