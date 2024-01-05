using Cysharp.Threading.Tasks;
using Studio23.SS2.SplashScreenSystem.Core;
using Studio23.SS2.SplashScreenSystem.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SplashScreenSystem.UI
{
    public class SplashScreenUIManager : MonoBehaviour
    {
        private CanvasGroup _parentCanvasGroup;

        public static SplashScreenUIManager Instance;
        public GameObject ButtonPanel;
        public GameObject ParentPanel;
        public TextMeshProUGUI TitleText;
        public GameObject ScrollRect;
        public Transform RectParent;

        public GameObject ImagePrefab;
        public List<GameObject> ImageObjects = new List<GameObject>();

        public SplashScreenBehaviour SplashScreenBehaviour;
        public TextMeshProUGUI ScrollRectText;

        //Button
        public Button AcceptBtn;
        public Button DeclineBtn;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            ScrollRectText = ScrollRect.GetComponentInChildren<TextMeshProUGUI>();
            _parentCanvasGroup = ParentPanel.GetComponent<CanvasGroup>();
        }

        public async void CrossFadeData(float duration)
        {
            float val = 0.0f;
            _parentCanvasGroup.alpha = val;
            while (val < duration)
            {
                val += Time.deltaTime;
                var ratio = val / duration;
                _parentCanvasGroup.alpha = ratio;
                await UniTask.Yield();
            }
        }

        public void DisplayData(SplashScreenSO data)
        {
            data.UpdateAndShowSplashContainer(this);
        }
    }
}