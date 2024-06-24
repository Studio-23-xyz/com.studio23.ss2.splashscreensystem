using Studio23.SS2.SplashScreenSystem.UI;
using UnityEngine;
using UnityEngine.Localization;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [CreateAssetMenu(fileName = "ScreenTextData", menuName = "SplashScreenSystem/ScreenTextData", order = 1)]
    [System.Serializable]
    public class ScreenTextData : SplashScreenSO
    {
        public LocalizedString Title;
        public LocalizedString Description;
        public bool ShowButton;
        public FontData TitleFontData;
        public FontData DescriptionFontData;

        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {
            ui.SetupPage(Title, Description, ShowButton, TitleFontData, DescriptionFontData);
        }
    }
}

