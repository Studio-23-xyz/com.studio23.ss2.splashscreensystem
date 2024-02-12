using Studio23.SS2.SplashScreenSystem.UI;
using UnityEngine;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    public abstract class SplashScreenSO : ScriptableObject
    {
        public bool IsInteractable;
        public abstract void UpdateAndShowSplashContainer(SplashScreenUIManager ui);
    }
}