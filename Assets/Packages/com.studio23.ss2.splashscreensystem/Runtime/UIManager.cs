using Studio23.SS2.SplashScreenSystem.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject ButtonPanel;

    public TextMeshProUGUI titleText;
    public GameObject ScrollRect;
    public Transform rectParent;
  
    public GameObject imagePrefab;
    private ScriptableObject _splashData;
    private List<GameObject> imageObjects = new List<GameObject>();

    public SplashScreenBehaviour SplashScreenBehaviour;

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

  
    [ContextMenu("Populate UI")]
    void PopulateUI()
    {
        ThirdPartyData thirdPartyData = _splashData as ThirdPartyData;

        foreach (var entry in thirdPartyData.ThirdPartyEntries)
        {
            GameObject newImage = Instantiate(imagePrefab, rectParent);
            newImage.GetComponent<Image>().sprite = Sprite.Create(entry.image, new Rect(0, 0, entry.image.width, entry.image.height), Vector2.zero);
            imageObjects.Add(newImage);
            rectParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(entry.image.width, entry.image.height);
        }
    }

    public void DisplayData(ScriptableObject data)
    {
        _splashData = data;

        if (data is EULAData)
        {
            EULAData eulaData = data as EULAData;
            if (eulaData != null)
            {
                titleText.text = eulaData.EulaTitle;
                ScrollRect.GetComponentInChildren<TextMeshProUGUI>().text = eulaData.EulaDescription;
                ScrollRect.GetComponent<ScrollRect>().vertical = true;
                ButtonPanel.SetActive(true);
                titleText.gameObject.SetActive(true);
                ScrollRect.gameObject.SetActive(true);
                rectParent.gameObject.SetActive(false);

                AcceptBtn.onClick.AddListener(() =>
                {
                    SplashScreenBehaviour.OnSubmit(true);
                    SplashScreenBehaviour.HideSplashScreen();
                });
                DeclineBtn.onClick.AddListener(() =>
                {
                    SplashScreenBehaviour.OnSubmit(false);
                    SplashScreenBehaviour.HideSplashScreen();
                });
            }
        }
        else if (data is ThirdPartyData)
        {
            ThirdPartyData thirdPartyData = data as ThirdPartyData;

            if (thirdPartyData != null && thirdPartyData.ThirdPartyEntries.Count > 0)
            {
                PopulateUI();

                Vector2 pivot = new Vector2(0.5f, 0.5f);
    
                ThirdPartyEntry entry = thirdPartyData.ThirdPartyEntries[0];
                titleText.text = entry.title;
                ScrollRect.GetComponentInChildren<TextMeshProUGUI>().text = "";
                ButtonPanel.SetActive(false);
                titleText.gameObject.SetActive(false);
                ScrollRect.gameObject.SetActive(false);
                rectParent.gameObject.SetActive(true);
            }
        }
        else if (data is DisclaimerData)
        {
            DisclaimerData disclaimerData = data as DisclaimerData;
            if (disclaimerData != null)
            {
                titleText.text = disclaimerData.DisclaimerTitle;
                ScrollRect.GetComponentInChildren<TextMeshProUGUI>().text = disclaimerData.DisclaimerDescription;
                ButtonPanel.SetActive(false);
                titleText.gameObject.SetActive(true);
                ScrollRect.gameObject.SetActive(true);
                rectParent.gameObject.SetActive(false);
            }
        }
    }
}