using Studio23.SS2.SplashScreenSystem.UI;
using System.Collections.Generic;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ThirdPartyData : SplashScreenSO
    {
        public List<ThirdPartyDataEntry> ThirdPartyEntries = new List<ThirdPartyDataEntry>();

        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {
            ui.SetupThirdPartyPage(ThirdPartyEntries);
        }
    }
}
