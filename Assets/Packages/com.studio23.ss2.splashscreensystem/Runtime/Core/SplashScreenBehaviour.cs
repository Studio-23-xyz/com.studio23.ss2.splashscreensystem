using Cysharp.Threading.Tasks;
using Studio23.SS2.SplashScreenSystem.Data;
using Studio23.SS2.SplashScreenSystem.UI;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Studio23.SS2.SplashScreenSystem.Core
{
    [System.Serializable]
    public class SplashScreenBehaviour : MonoBehaviour
    {
        [SerializeField] private SplashScreenData[] _splashScreens;
        public UnityEvent<bool> OnPageResponse;
        public UnityEvent OnSplashScreenCompleted;

        private bool _pageButtonClicked;
        private CancellationTokenSource _cancelSplashScreen;



        private void OnEnable()
        {
            _cancelSplashScreen = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            _cancelSplashScreen?.Cancel();
            _cancelSplashScreen?.Dispose();
        }


        private void Start()
        {
            ShowSplashScreen();
        }
        private async void ShowSplashScreen()
        {
            foreach (var splash in _splashScreens)
            {
                _pageButtonClicked = false;
                SplashScreenData currentSplash = splash;
                if (currentSplash.Data != null)
                    SplashScreenUIManager.Instance.DisplayData(currentSplash.Data);
                CrossFadeScreen(currentSplash.FadeDuration);
                if (!currentSplash.Data.IsInteractable)
                    await UniTask.Delay(TimeSpan.FromSeconds(currentSplash.Duration), cancellationToken: _cancelSplashScreen.Token, cancelImmediately: true).SuppressCancellationThrow();
                else
                    await UniTask.WaitUntil(() => _pageButtonClicked, cancellationToken: _cancelSplashScreen.Token, cancelImmediately: true).SuppressCancellationThrow();
            }
            
            OnSplashScreenCompleted?.Invoke();
        }
        public void CrossFadeScreen(float duration)
        {
            SplashScreenUIManager.Instance.CrossFadeData(duration);
        }
        public bool OnSubmit(bool status)
        {
            OnPageResponse?.Invoke(status);
            _pageButtonClicked = status;
            return status;
        }
    }
}