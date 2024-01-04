using Studio23.SS2.SplashScreenSystem.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ScreenTextData : SplashScreenData
    {
        public string Title;
        public string Description;
        public bool ShowButton;

        public override void show(SplashScreenUIManager ui)
        {
            ui.titleText.text = Title;
            ui.ScrollRectText.text = Description;
            ui.titleText.gameObject.SetActive(true);
            ui.ScrollRect.gameObject.SetActive(true);
            ui.rectParent.gameObject.SetActive(false);
            ui.ScrollRect.GetComponent<ScrollRect>().vertical = ShowButton;
            ui.ButtonPanel.SetActive(ShowButton);
            ui.AcceptBtn.onClick.RemoveAllListeners();
            ui.DeclineBtn.onClick.RemoveAllListeners();
            ui.AcceptBtn.onClick.AddListener(() =>
            {
                ui.SplashScreenBehaviour.OnSubmit(true);
                ui.SplashScreenBehaviour.HideSplashScreen();
            });
            ui.DeclineBtn.onClick.AddListener(() =>
            {
                ui.SplashScreenBehaviour.OnSubmit(false);
                ui.SplashScreenBehaviour.HideSplashScreen();
            });
        }
    }
}

