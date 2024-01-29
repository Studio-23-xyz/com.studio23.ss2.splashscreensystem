using Cysharp.Threading.Tasks;
using Studio23.SS2.SplashScreenSystem.Data;
using Studio23.SS2.SplashScreenSystem.UI;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.SplashScreenSystem.Core
{
    [System.Serializable]
    public class SplashScreenBehaviour : MonoBehaviour
    {
        [SerializeField] private SplashScreenData[] _splashScreens;
        public UnityEvent<bool> OnEULAResponse;
        public UnityEvent OnSplashScreenCompleted;

        private bool _eulaButtonClicked;

        private void Start()
        {
            ShowSplashScreen();
        }
        private async void ShowSplashScreen()
        {
            foreach (var splash in _splashScreens)
            {
                SplashScreenData currentSplash = splash;
                if (currentSplash.Data != null)
                    SplashScreenUIManager.Instance.DisplayData(currentSplash.Data);
                CrossFadeScreen(currentSplash.FadeDuration);
                if (currentSplash.Data.name != "EULA")
                    await UniTask.Delay(TimeSpan.FromSeconds(currentSplash.Duration));
                else
                    await UniTask.WaitUntil(() => _eulaButtonClicked);
            }

            OnSplashScreenCompleted?.Invoke();
        }
        public void CrossFadeScreen(float duration)
        {
            SplashScreenUIManager.Instance.CrossFadeData(duration);
        }
        public bool OnSubmit(bool status)
        {
            OnEULAResponse?.Invoke(status);
            _eulaButtonClicked = status;
            return status;
        }
    }
}