using Studio23.SS2.SplashScreenSystem.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton instance

    // UI elements to display data
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public RawImage imageDisplay;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DisplayData(ScriptableObject data)
    {
        if (data is EULAData)
        {
            EULAData eulaData = data as EULAData;
            if (eulaData != null)
            {
                titleText.text = eulaData.EulaTitle;
                descriptionText.text = eulaData.EulaDescription;
                imageDisplay.texture = null;
                imageDisplay.gameObject.SetActive(false);
            }
        }
        else if (data is ThirdPartyData)
        {
            ThirdPartyData thirdPartyData = data as ThirdPartyData;

            if (thirdPartyData != null && thirdPartyData.ThirdPartyEntries.Count > 0)
            {
                Vector2 pivot = new Vector2(0.5f, 0.5f);

                ThirdPartyEntry entry = thirdPartyData.ThirdPartyEntries[0];
                titleText.text = entry.title;
                descriptionText.text = "";
                imageDisplay.gameObject.SetActive(true);
                imageDisplay.texture = entry.image;

            }
        }
        else if (data is DisclaimerData)
        {
            DisclaimerData disclaimerData = data as DisclaimerData;
            if (disclaimerData != null)
            {
                titleText.text = disclaimerData.DisclaimerTitle;
                descriptionText.text = disclaimerData.DisclaimerDescription;
                imageDisplay.texture = null;
                imageDisplay.gameObject.SetActive(false);
            }
        }
    }
}
