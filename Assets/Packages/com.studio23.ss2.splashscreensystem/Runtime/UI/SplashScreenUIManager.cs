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
        public GameObject ButtonPanel;

        public GameObject ParentPanel;
        public TextMeshProUGUI titleText;
        public GameObject ScrollRect;
        public Transform rectParent;

        public GameObject imagePrefab;
        private SplashScreenData _splashData;
        public List<GameObject> ImageObjects = new List<GameObject>();

        public SplashScreenBehaviour SplashScreenBehaviour;
        public TextMeshProUGUI ScrollRectText;

        //Button
        public Button AcceptBtn;
        public Button DeclineBtn;

        private void Awake()
        {
            ScrollRectText = ScrollRect.GetComponentInChildren<TextMeshProUGUI>();

            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }


 
        void PopulateUI(ThirdPartyData thirdPartyData)
        {
            foreach (var entry in thirdPartyData.ThirdPartyEntries)
            {
                GameObject newImage = Instantiate(imagePrefab, rectParent);
                newImage.GetComponent<Image>().sprite = Sprite.Create(entry.image, new Rect(0, 0, entry.image.width, entry.image.height), Vector2.zero);
                ImageObjects.Add(newImage);
                rectParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(entry.image.width, entry.image.height);
            }
        }

        public async void CrossFadeData(float duration)
        {
            float val = 0.0f;
            ParentPanel.GetComponent<CanvasGroup>().alpha = val;
            while (val < duration)
            {
                val += Time.deltaTime;
                var ratio = val / duration;
                ParentPanel.GetComponent<CanvasGroup>().alpha = ratio;
                await UniTask.Yield();
            }
        }

        public void DisplayData(SplashScreenData data)
        {
            _splashData = data;

            data.show(this);
        }
    }
}