using Studio23.SS2.SplashScreenSystem.UI;
using UnityEngine.UI;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ScreenTextData : SplashScreenSO
    {
        public string Title;
        public string Description;
        public bool ShowButton;

        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {
            ui.TitleText.text = Title;
            ui.ScrollRectText.text = Description;
            ui.TitleText.gameObject.SetActive(true);
            ui.ScrollRect.gameObject.SetActive(true);
            ui.RectParent.gameObject.SetActive(false);
            ui.ScrollRect.GetComponent<ScrollRect>().vertical = ShowButton;
            ui.ButtonPanel.SetActive(ShowButton);
            ui.AcceptBtn.onClick.RemoveAllListeners();
            ui.DeclineBtn.onClick.RemoveAllListeners();
            ui.AcceptBtn.onClick.AddListener(() =>
            {
                ui.SplashScreenBehaviour.OnSubmit(true);
            });
            ui.DeclineBtn.onClick.AddListener(() =>
            {
                ui.SplashScreenBehaviour.OnSubmit(false);
            });
        }
    }
}

