using Studio23.SS2.SplashScreenSystem.Data;
using UnityEngine;
using TMPro;


public class SplashScreenUIManager : MonoBehaviour
{
    public DisclaimerData disclaimerData;
    public EULAData eulaData;
    public ThirdPartyData thirdPartyData;

    public GameObject disclaimerPrefab;
    public GameObject eulaPrefab;
    public GameObject thirdPartyPrefab;

    public Transform PrefabParent;

    private void Start()
    {
        CreateDisclaimerUI();
        CreateEulaUI();
        //CreateThirdPartyUI();
    }

    private void CreateDisclaimerUI()
    {
        GameObject disclaimerObject = Instantiate(disclaimerPrefab, PrefabParent);
        TextMeshProUGUI[] texts = disclaimerObject.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = disclaimerData.DisclaimerTitle;
        texts[1].text = disclaimerData.DisclaimerDescription;
        // Other setup for the disclaimer UI
    }

    private void CreateEulaUI()
    {
        GameObject eulaObject = Instantiate(eulaPrefab, PrefabParent);
        TextMeshProUGUI[] texts = eulaObject.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = eulaData.EulaTitle;
        texts[1].text = eulaData.EulaDescription;
        // Other setup for the EULA UI
    }

    //private void CreateThirdPartyUI()
    //{
    //    GameObject thirdPartyObject = Instantiate(thirdPartyPrefab, transform);
    //   // ThirdPartyUIUpdater updater = thirdPartyObject.GetComponent<ThirdPartyUIUpdater>();
    //    //updater.UpdateThirdPartyUI(thirdPartyData.ThirdPartyEntries);
    //    // Other setup for the Third Party UI
    //}
}
