using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    private void Start()
    {
        SplashScreenBehaviour splashManager = FindObjectOfType<SplashScreenBehaviour>();
        if (splashManager != null)
        {
            splashManager.OnFinish += AllSplashScreensFinished;
        }
    }

    private void AllSplashScreensFinished(bool finished)
    {
        if (finished)
        {
            Debug.Log("All splash screens finished.");
            // Your code here for when all splash screens are done.
        }
    }
}
