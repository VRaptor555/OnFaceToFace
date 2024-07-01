using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public record SettingsData
    {
        public bool IsFullScreen;
        public float VolumeAudio;
        public int GraphicQuality;
        public int ResolutionW;
        public int ResolutionH;
        public uint ResNumerator;
        public uint ResDenominator;
        public int ResolutionIndex;
        public List<string> ResolutionsList;
    }
}