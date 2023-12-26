using Cysharp.Threading.Tasks;
using Studio23.SS2.SplashScreenSystem.Data;
using Studio23.SS2.SplashScreenSystem.UI;
using System;
using UnityEngine;

namespace Studio23.SS2.SplashScreenSystem.Core
{
    public class SplashScreenBehaviour : MonoBehaviour
    {
        public SplashScreenData[] splashScreens;
        public event Action<bool> OnFinish;
        public int currentIndex = 0;

        private void Start()
        {
            ShowSplashScreen();
        }

        private async void ShowSplashScreen()
        {
            if (currentIndex < splashScreens.Length)
            {
                SplashScreenData currentSplash = splashScreens[currentIndex];
                if (currentSplash.data != null)
                    UIManager.Instance.DisplayData(currentSplash.data);
                CrossFadeScreen(currentSplash.fadeduration);
                if (currentSplash.data.name != "EULA")
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(currentSplash.duration));
                    HideSplashScreen();
                }
            }
            else
            {
                OnFinish?.Invoke(true);
                currentIndex = 0;
            }
        }

        public void HideSplashScreen()
        {
            currentIndex++;
            ShowSplashScreen();
        }

        public void CrossFadeScreen(float duration)
        {
            UIManager.Instance.CrossFadeData(duration);
        }

        public bool OnSubmit(bool status)
        {
            OnFinish?.Invoke(status);
            return status;
        }

    }
}