using System;
using System.IO;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;

namespace Menu
{
    public static class SaveLoadSettings
    {
        public static void SaveSettings(SettingsData savedata)
        {
            PlayerPrefs.SetInt("FullScreen", Convert.ToInt32(savedata.IsFullScreen));
            PlayerPrefs.SetFloat("VolumeAudio", savedata.VolumeAudio);
            PlayerPrefs.SetInt("GraphicQuality", savedata.GraphicQuality);
            PlayerPrefs.SetInt("GraphicResolutionW", savedata.ResolutionW);
            PlayerPrefs.SetInt("GraphicResolutionH", savedata.ResolutionH);
            PlayerPrefs.SetInt("GraphicRefreshNumerator", Convert.ToInt32(savedata.ResNumerator));
            PlayerPrefs.SetInt("GraphicRefreshDenominator", Convert.ToInt32(savedata.ResDenominator));
            PlayerPrefs.Save();
        }

        public static SettingsData LoadSettings()
        {
            SettingsData loaddata = new SettingsData();
            loaddata.IsFullScreen = !PlayerPrefs.HasKey("FullScreen") || Convert.ToBoolean(PlayerPrefs.GetInt("FullScreen"));
            
            if (PlayerPrefs.HasKey("VolumeAudio"))
                loaddata.VolumeAudio = PlayerPrefs.GetFloat("VolumeAudio");
            else
                loaddata.VolumeAudio = 0;
            loaddata.GraphicQuality = PlayerPrefs.HasKey("GraphicQuality") ? PlayerPrefs.GetInt("GraphicQuality") : 2;
            loaddata.ResolutionW = PlayerPrefs.HasKey("GraphicResolutionW") ? PlayerPrefs.GetInt("GraphicResolutionW") : -1;
            loaddata.ResolutionH = PlayerPrefs.HasKey("GraphicResolutionH") ? PlayerPrefs.GetInt("GraphicResolutionH") : -1;
            loaddata.ResNumerator = PlayerPrefs.HasKey("GraphicRefreshNumerator") ? Convert.ToUInt32(PlayerPrefs.GetInt("GraphicRefreshNumerator")) : 0;
            loaddata.ResDenominator = PlayerPrefs.HasKey("GraphicRefreshDenominator") ? Convert.ToUInt32(PlayerPrefs.GetInt("GraphicRefreshDenominator")) : 0;

            return loaddata;
        }
    }
}