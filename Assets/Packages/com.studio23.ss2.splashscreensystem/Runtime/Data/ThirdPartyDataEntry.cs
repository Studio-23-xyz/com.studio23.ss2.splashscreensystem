using UnityEngine;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ThirdPartyDataEntry
    {
        public string Title;
        public Texture2D Image;

        public ThirdPartyDataEntry(string title, Texture2D image)
        {
            this.Title = title;
            this.Image = image;
        }
    }
}


