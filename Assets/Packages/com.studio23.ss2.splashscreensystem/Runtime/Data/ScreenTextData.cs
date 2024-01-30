using Studio23.SS2.SplashScreenSystem.UI;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ScreenTextData : SplashScreenSO
    {
        public string Title;
        public string Description;
        public bool ShowButton;
        public FontData TitleFontData;
        public FontData DescriptionFontData;

        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {
            ui.SetupPage(Title, Description, ShowButton, TitleFontData, DescriptionFontData);
        }
    }
}

