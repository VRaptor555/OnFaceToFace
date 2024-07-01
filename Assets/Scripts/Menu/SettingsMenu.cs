using System;
using System.Collections;
using System.Collections.Generic;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private Toggle _fullScreen;
    private Slider _audioSlider;
    private TMP_Dropdown[] _setingsDropdown;
    private TMP_Dropdown _qualityDropdown;
    private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private AudioSource audioSettings;
    [SerializeField] private AudioSource splashMusic;

    private void OnEnable()
    {
        _fullScreen = GetComponentInChildren<Toggle>();
        _audioSlider = GetComponentInChildren<Slider>();
        _setingsDropdown = GetComponentsInChildren<TMP_Dropdown>();
        foreach (var dd in _setingsDropdown)
        {
            if (dd.CompareTag("Quality"))
                _qualityDropdown = dd;
            else
                _resolutionDropdown = dd;
        }
        _fullScreen.isOn = Settings.instance.data.IsFullScreen;
        _audioSlider.value = Settings.instance.data.VolumeAudio;
        _qualityDropdown.value = Settings.instance.data.GraphicQuality;
        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(Settings.instance.data.ResolutionsList);
        _resolutionDropdown.value = Settings.instance.data.ResolutionIndex;
        if (audioSettings)
            audioSettings.PlayOneShot(audioSettings.clip);
        if (splashMusic)
            splashMusic.Stop();
    }

    private void OnDisable()
    {
        if (audioSettings)
            audioSettings.Stop();
        if (splashMusic)
            splashMusic.PlayOneShot(splashMusic.clip);
    }

    public void FullScreenToggle(bool toggle)
    {
        Settings.instance.SwapFullScreen(toggle);
    }
    
    public void AudioVolume(float sliderValue)
    {
        Settings.instance.data.VolumeAudio = sliderValue;
        Settings.instance.ChangeAudioVolume();
    }
    
    public void GraphicQuality(int index)
    {
        Settings.instance.data.GraphicQuality = index;
    }

    public void SetGraphicResolution(int index)
    {
        Settings.instance.SetGr(index);
        Settings.instance.data.ResolutionIndex = index;
    }

    public void SaveSettings()
    {
        Settings.instance.ChangeAllValues();
        SaveLoadSettings.SaveSettings(Settings.instance.data);
    }
}
