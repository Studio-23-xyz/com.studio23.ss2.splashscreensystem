using UnityEngine;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ThirdPartyDataEntry
    {
        public Texture2D Image;

        public ThirdPartyDataEntry(Texture2D image)
        {
            this.Image = image;
        }
    }
}


