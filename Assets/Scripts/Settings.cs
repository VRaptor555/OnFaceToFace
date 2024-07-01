using System;
using System.Collections;
using System.Collections.Generic;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    [HideInInspector] public SettingsData data;

    [SerializeField] private AudioMixer am;

    private Resolution[] resolutions;
    private int toSaveRI;

    private void Awake()
    {
        instance = this;
        data = SaveLoadSettings.LoadSettings();
        Screen.fullScreen = data.IsFullScreen;
        am.SetFloat("masterVolume", data.VolumeAudio);
        QualitySettings.SetQualityLevel(data.GraphicQuality);
        GetGraphicResolutions();
    }

    public void SwapFullScreen(bool fullscreen)
    {
        data.IsFullScreen = fullscreen;
        Screen.fullScreen = data.IsFullScreen;
    }

    public void ChangeAudioVolume()
    {
        am.SetFloat("masterVolume", data.VolumeAudio);
    }

    public void ChangeAllValues()
    {
        QualitySettings.SetQualityLevel(data.GraphicQuality);
        SetGr(data.ResolutionIndex);
        data.ResolutionW = resolutions[data.ResolutionIndex].width;
        data.ResolutionH = resolutions[data.ResolutionIndex].height;
        
        data.ResNumerator = resolutions[data.ResolutionIndex].refreshRateRatio.numerator;
        data.ResDenominator = resolutions[data.ResolutionIndex].refreshRateRatio.denominator;
    }

    private void GetGraphicResolutions()
    {
        resolutions = Screen.resolutions; //Получение доступных разрешений
        data.ResolutionsList = new List<string> (); //Создание списка со строковыми значениями
        data.ResolutionIndex = -1;
        for(var i = 0; i < resolutions.Length; i++) //Поочерёдная работа с каждым разрешением
        {
            var option = resolutions [i].width + " x " + resolutions [i].height + " @ " + resolutions[i].refreshRateRatio + "Гц"; //Создание строки для списка
            data.ResolutionsList.Add(option); //Добавление строки в список
            
            if (resolutions[i].Equals(Screen.currentResolution) && data.ResolutionIndex == -1) //Если текущее разрешение равно проверяемому
            {
                data.ResolutionIndex = i; //То получается его индекс
            }

            if (
                data.ResolutionW != resolutions[i].width
                || data.ResolutionH != resolutions[i].height
                || data.ResNumerator != resolutions[i].refreshRateRatio.numerator
                || data.ResDenominator != resolutions[i].refreshRateRatio.denominator
                ) continue;
            Screen.SetResolution(
                data.ResolutionW,
                data.ResolutionH,
                data.IsFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed,
                new RefreshRate(){numerator = data.ResNumerator, denominator = data.ResDenominator}
            );    
            data.ResolutionIndex = i; //То получается его индекс
        }
    }
    public void SetGr(int index)
    {
        Screen.SetResolution(
            resolutions[index].width,
            resolutions[index].height,
            data.IsFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed,
            resolutions[index].refreshRateRatio
        );
    }
}
