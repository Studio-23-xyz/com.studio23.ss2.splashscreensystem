
using Studio23.SS2.SplashScreenSystem.UI;
using UnityEngine;

namespace Studio23.SS2.SplashScreenSystem.Data
{
    [CreateAssetMenu(fileName = "Splash Screen Prefab", menuName = "Splash Screen/Prefab", order = 1)]
    [System.Serializable]
    public class SplashScreenPrefabData : SplashScreenSO
    {
        public GameObject PrefabGameObject;
        public override void UpdateAndShowSplashContainer(SplashScreenUIManager ui)
        {
            ui.SetUpPrefabGameObject(PrefabGameObject);
        }
    }
}
