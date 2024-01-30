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
        public static SplashScreenUIManager Instance;

        private CanvasGroup _parentCanvasGroup;
        [SerializeField] private GameObject ButtonPanel;
        [SerializeField] private GameObject ParentPanel;
        [SerializeField] private TextMeshProUGUI TitleText;
        [SerializeField] private GameObject ScrollRect;
        [SerializeField] private Transform RectParent;
        [SerializeField] private Image ImagePrefab;
        [SerializeField] private SplashScreenBehaviour SplashScreenBehaviour;
        [SerializeField] private TextMeshProUGUI ScrollRectText;
        [SerializeField] private GridLayoutGroup GridLayout;
        [SerializeField] private Button AcceptBtn;
        [SerializeField] private Button DeclineBtn;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            Initialize();
        }

        public void SetupPage(string titleText, string descriptionText, bool showButton, FontData titleFontData, FontData descriptionFontData)
        {
            if (titleFontData != null) titleFontData.UpdateFontDataToText(TitleText);
            if (descriptionFontData != null) descriptionFontData.UpdateFontDataToText(ScrollRectText);
            TitleText.text = titleText;
            ScrollRectText.text = descriptionText;
            TitleText.gameObject.SetActive(true);
            ScrollRect.gameObject.SetActive(true);
            RectParent.gameObject.SetActive(false);
            ScrollRect.GetComponent<ScrollRect>().vertical = showButton;
            ButtonPanel.SetActive(showButton);
            AcceptBtn.onClick.RemoveAllListeners();
            DeclineBtn.onClick.RemoveAllListeners();
            AcceptBtn.onClick.AddListener(() =>
            {
                SplashScreenBehaviour.OnSubmit(true);
            });
            DeclineBtn.onClick.AddListener(() =>
            {
                SplashScreenBehaviour.OnSubmit(false);
            });
        }

        public void SetupThirdPartyPage(List<ThirdPartyDataEntry> thirdPartyEntries)
        {
            foreach(var entry in thirdPartyEntries)
            {
                Image newImageComponent = Instantiate(ImagePrefab, RectParent);
                newImageComponent.sprite = Sprite.Create(entry.Image, new Rect(0, 0, entry.Image.width, entry.Image.height), Vector2.zero);
                GridLayout.cellSize = new Vector2(entry.Image.width, entry.Image.height);
            }
            TitleText.gameObject.SetActive(false);
            ButtonPanel.SetActive(false);
            ScrollRect.gameObject.SetActive(false);
            RectParent.gameObject.SetActive(true);
        }

        private void Initialize()
        {
            ScrollRectText = ScrollRect.GetComponentInChildren<TextMeshProUGUI>();
            _parentCanvasGroup = ParentPanel.GetComponent<CanvasGroup>();
            GridLayout = RectParent.GetComponent<GridLayoutGroup>();
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