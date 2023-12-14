using System;
using UnityEngine;

public class SplashScreenBehaviour : MonoBehaviour
{
    [Serializable]
    public class SplashScreen
    {
        public GameObject splashObject;
        public float duration;
        public ScriptableObject data;
    }

    public SplashScreen[] splashScreens;

    public event Action<bool> OnFinish;

     public int currentIndex = 0;

    private void Start()
    {
        ShowSplashScreen();
    }

    private void ShowSplashScreen()
    {
        if (currentIndex < splashScreens.Length)  
        {
            SplashScreen currentSplash = splashScreens[currentIndex];

            // Enable the current splash object
            currentSplash.splashObject.SetActive(true);

            // Pass data to display to the UI elements (assuming you have some script handling this)
            if (currentSplash.data != null)
            {
                // Example: Assuming you have a UIManager that handles displaying the data
                UIManager.Instance.DisplayData(currentSplash.data);
            }
            
            if(currentSplash.data.name != "EULA")
            {
                Invoke(nameof(HideSplashScreen), currentSplash.duration);
            }
        }

        else
        {
            // All splash screens shown
            OnFinish?.Invoke(true);
            currentIndex = 0;
        }
    }

    public void HideSplashScreen()
    {
        // Disable the current splash object
        if (currentIndex >= 0 && currentIndex < splashScreens.Length)
        {
            SplashScreen previousSplash = splashScreens[currentIndex];
            previousSplash.splashObject.SetActive(false);
        }
        currentIndex++;
        ShowSplashScreen();
    }

    public bool OnSubmit(bool status)
    {
        OnFinish?.Invoke(status);
        return status;
    }
}