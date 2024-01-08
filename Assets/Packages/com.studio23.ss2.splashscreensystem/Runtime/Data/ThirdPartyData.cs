using Studio23.SS2.SplashScreenSystem.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [System.Serializable]
    public class ThirdPartyData : SplashScreenSO
    {
        public List<ThirdPartyDataEntry> ThirdPartyEntries = new List<ThirdPartyDataEntry>();

        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {

            foreach (var entry in ThirdPartyEntries)
            {
                Image newImageComponent = Instantiate(ui.ImagePrefab.GetComponent<Image>(), ui.RectParent);
                newImageComponent.sprite = Sprite.Create(entry.Image, new Rect(0, 0, entry.Image.width, entry.Image.height), Vector2.zero);

                // Assuming cellSize should be set for each image individually
                ui.GridLayout.cellSize = new Vector2(entry.Image.width, entry.Image.height);
            }

            Vector2 pivot = new Vector2(0.5f, 0.5f);
            ThirdPartyDataEntry firstEntry = ThirdPartyEntries[0];
            ui.TitleText.text = firstEntry.Title;
            ui.ButtonPanel.SetActive(false);
            ui.TitleText.gameObject.SetActive(false);
            ui.ScrollRect.gameObject.SetActive(false);
            ui.RectParent.gameObject.SetActive(true);
        }
    }
}
