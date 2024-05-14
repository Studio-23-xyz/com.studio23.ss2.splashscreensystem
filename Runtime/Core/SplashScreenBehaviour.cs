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


        private void Start()
        {
            OnSplashScreenCompleted.AddListener(CancelAsyncToken);
            InitializeAsyncToken();
            ShowSplashScreen();
        }

        private void OnDestroy()
        {
            OnSplashScreenCompleted.RemoveListener(CancelAsyncToken);
        }


        private async void ShowSplashScreen()
        {
            foreach (var splash in _splashScreens)
            {
                _pageButtonClicked = false;
                SplashScreenData currentSplash = splash;
                if (currentSplash.Data == null) continue;
                SplashScreenUIManager.Instance.DisplayData(currentSplash.Data);
                await SplashScreenUIManager.Instance.CrossFadeData(currentSplash.FadeDuration).AttachExternalCancellation(_cancelSplashScreen.Token).SuppressCancellationThrow();
                if (!currentSplash.Data.IsInteractable)
                    await UniTask.Delay(TimeSpan.FromSeconds(currentSplash.Duration), cancellationToken: _cancelSplashScreen.Token, cancelImmediately: true).SuppressCancellationThrow();
                else
                    await UniTask.WaitUntil(() => _pageButtonClicked, cancellationToken: _cancelSplashScreen.Token, cancelImmediately: true).SuppressCancellationThrow();

                await UniTask.WaitForFixedUpdate();
            }
            
            OnSplashScreenCompleted?.Invoke();
        }


        public void SetSkipAll()
        {
            CancelAsyncToken();
        }

        public void SetSkipSingle()
        {
            CancelAsyncToken();
            InitializeAsyncToken();
        }

        private void InitializeAsyncToken()
        {
            _cancelSplashScreen = new CancellationTokenSource();
        }


        private void CancelAsyncToken()
        {
            _cancelSplashScreen?.Cancel();
        }

        public bool OnSubmit(bool status)
        {
            OnPageResponse?.Invoke(status);
            _pageButtonClicked = status;
            return status;
        }
    }
}