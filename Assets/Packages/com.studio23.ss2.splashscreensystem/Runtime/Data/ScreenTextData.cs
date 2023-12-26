using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ScreenTextData : ScriptableObject
    {
        public string Title;
        public string Description;
        public bool ShowButton;
    }
}

