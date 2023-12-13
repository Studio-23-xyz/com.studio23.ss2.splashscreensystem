using System.Collections.Generic;
using UnityEngine;


namespace Studio23.SS2.SplashScreenSystem.Data
{
  
    // ThirdPartyData scriptable object data class
    [System.Serializable]
    public class ThirdPartyData : ScriptableObject
    {
        public List<ThirdPartyEntry> ThirdPartyEntries = new List<ThirdPartyEntry>();
    }

    // ThirdPartyEntry class
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
