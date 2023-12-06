using System.Collections.Generic;
using UnityEngine;


namespace Studio23.SS2.SplashScreenSystem.Data
{
    [CreateAssetMenu(fileName = "SplashScreenData", menuName = "Data/SplashScreenData", order = 1)]
    public class SplashScreenData : ScriptableObject
    {
        public string DisclaimerTitle;
        public string DisclaimerDescription;
        public string EulaTitle;
        public string EulaDescription;
        public ThirdPartyEntry[] ThirdPartyEntries;
    }

    [System.Serializable]
    public class ThirdPartyEntry
    {
        public string title;
        public Texture2D image;

        public ThirdPartyEntry(string title, Texture2D image)
        {
            this.title = title;
            this.image = image;
        }
    }
}



