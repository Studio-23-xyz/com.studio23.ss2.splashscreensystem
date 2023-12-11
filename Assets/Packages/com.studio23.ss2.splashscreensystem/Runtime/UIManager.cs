using Studio23.SS2.SplashScreenSystem.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // Singleton instance

    // UI elements to display data
    public Text titleText;
    public Text descriptionText;
    public Image imageDisplay;

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
                // Set image if needed
                // imageDisplay.sprite = ...;
            }
        }
        else if (data is ThirdPartyData)
        {
            ThirdPartyData thirdPartyData = data as ThirdPartyData;
            if (thirdPartyData != null && thirdPartyData.ThirdPartyEntries.Count > 0)
            {
                // Display the first entry's data
                ThirdPartyEntry entry = thirdPartyData.ThirdPartyEntries[0];
                titleText.text = entry.title;
                // Set other UI elements accordingly
                // descriptionText.text = ...;
                // imageDisplay.sprite = ...;
            }
        }
        else if (data is DisclaimerData)
        {
            DisclaimerData disclaimerData = data as DisclaimerData;
            if (disclaimerData != null)
            {
                titleText.text = disclaimerData.DisclaimerTitle;
                descriptionText.text = disclaimerData.DisclaimerDescription;
                // Set image if needed
                // imageDisplay.sprite = ...;
            }
        }
    }
}
