using Studio23.SS2.SplashScreenSystem.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ThirdPartyData : SplashScreenData
    {
        public List<ThirdPartyDataEntry> ThirdPartyEntries = new List<ThirdPartyDataEntry>();

        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {
            foreach (var entry in ThirdPartyEntries)
            {
                GameObject newImage = Instantiate(ui.ImagePrefab, ui.RectParent);
                newImage.GetComponent<Image>().sprite = Sprite.Create(entry.image, new Rect(0, 0, entry.image.width, entry.image.height), Vector2.zero);
                ui.ImageObjects.Add(newImage);
                ui.RectParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(entry.image.width, entry.image.height);
            }

            Vector2 pivot = new Vector2(0.5f, 0.5f);

            ThirdPartyDataEntry firstEntry = ThirdPartyEntries[0];
            ui.TitleText.text = firstEntry.title;
            ui.ScrollRect.GetComponentInChildren<TextMeshProUGUI>().text = "";
            ui.ButtonPanel.SetActive(false);
            ui.TitleText.gameObject.SetActive(false);
            ui.ScrollRect.gameObject.SetActive(false);
            ui.RectParent.gameObject.SetActive(true);
        }
    }

    [System.Serializable]
    public class ThirdPartyDataEntry
    {
        public string title;
        public Texture2D image;

        public ThirdPartyDataEntry(string title, Texture2D image)
        {
            this.title = title;
            this.image = image;
        }
    }

    public abstract class SplashScreenData : ScriptableObject
    {
        public abstract void UpdateAndShowSplashContainer(SplashScreenUIManager ui);
    }
}
