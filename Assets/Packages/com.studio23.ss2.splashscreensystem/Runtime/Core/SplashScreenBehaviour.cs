using Cysharp.Threading.Tasks;
using Studio23.SS2.SplashScreenSystem.Data;
using Studio23.SS2.SplashScreenSystem.UI;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio23.SS2.SplashScreenSystem.Core
{
    [System.Serializable]
    public class UnityEventWithBool : UnityEvent<bool> { }
    public class SplashScreenBehaviour : MonoBehaviour
    {
        public SplashScreen[] splashScreens;
        public UnityEventWithBool OnEULAResponse;
        public UnityEvent OnSplashScreenCompleted;
        public int currentIndex = 0;
        private bool _eulaButtonClicked;
        
        private void Start()
        {
            ShowSplashScreen();
        }

        private async void ShowSplashScreen()
        {
            foreach (var s in splashScreens)
            {
                SplashScreen currentSplash = s;
                if (currentSplash.data != null)
                    SplashScreenUIManager.Instance.DisplayData(currentSplash.data);
                CrossFadeScreen(currentSplash.fadeduration);
                if (currentSplash.data.name != "EULA")
                    await UniTask.Delay(TimeSpan.FromSeconds(currentSplash.duration));
                else
                    await UniTask.WaitUntil(() => _eulaButtonClicked);
            }
        }

        public void HideSplashScreen()
        {
            currentIndex++;
            ShowSplashScreen();
        }

        public void CrossFadeScreen(float duration)
        {
            SplashScreenUIManager.Instance.CrossFadeData(duration);
        }

        public bool OnSubmit(bool status)
        {
            OnEULAResponse?.Invoke(status);
            _eulaButtonClicked = true;
            return status;
        }

    }
}