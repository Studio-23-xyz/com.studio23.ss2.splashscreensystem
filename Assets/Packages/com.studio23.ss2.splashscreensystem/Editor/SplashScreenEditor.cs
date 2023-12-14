using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Studio23.SS2.SplashScreenSystem.Data;
using UnityEngine.UI;


namespace Studio23.SS2.SplashScreenSystem.Editor
{
    public class SplashScreenEditor : EditorWindow
    {
        private int selectedTab = 0;
        private string disclaimerTitle = "";
        private string disclaimerDescription = "";
        private string eulaTitle = "";
        private string eulaDescription = "";
        private List<ThirdPartyEntry> thirdPartyEntries = new List<ThirdPartyEntry>();
        private string newEntryTitle = "";
        private Texture2D newEntryImage;

        [MenuItem("Studio-23/Splash Screen System/Widget")]
        public static void ShowWindow()
        {
            GetWindow<SplashScreenEditor>("Splash Screen System Widget");
        }

        private void OnGUI()
        {
            DrawTabs();

            switch (selectedTab)
            {
                case 0:
                    DrawDisclaimerTab();
                    break;
                case 1:
                    DrawEulaTab();
                    break;
                case 2:
                    DrawThirdPartyTab();
                    break;
                    
            }
        }


        private void DrawTabs()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Toggle(selectedTab == 0, "Disclaimer", "Button"))
            {
                selectedTab = 0;
            }
            if (GUILayout.Toggle(selectedTab == 1, "EULA", "Button"))
            {
                selectedTab = 1;
            }
            if (GUILayout.Toggle(selectedTab == 2, "3rd Party Container", "Button"))
            {
                selectedTab = 2;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }

        private void DrawDisclaimerTab()
        {
            GUILayout.Label("Disclaimer", EditorStyles.boldLabel);
            disclaimerTitle = EditorGUILayout.TextField("Title", disclaimerTitle);
            disclaimerDescription = EditorGUILayout.TextField("Description", disclaimerDescription, GUILayout.Height(100));
            GUILayout.Space(20);
        }

        private void DrawEulaTab()
        {
            GUILayout.Label("EULA", EditorStyles.boldLabel);
            eulaTitle = EditorGUILayout.TextField("Title", eulaTitle);
            eulaDescription = EditorGUILayout.TextField("Description", eulaDescription, GUILayout.Height(100));
            GUILayout.Space(20);
        }

        private void DrawThirdPartyTab()
        {
            GUILayout.Label("3rd Party Container", EditorStyles.boldLabel);

            foreach (var entry in thirdPartyEntries)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(entry.title);
                GUILayout.Box(entry.image, GUILayout.Width(50), GUILayout.Height(50));
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            newEntryTitle = EditorGUILayout.TextField("Title", newEntryTitle);
            newEntryImage = (Texture2D)EditorGUILayout.ObjectField("Image", newEntryImage, typeof(Texture2D), false);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Add"))
            {
                thirdPartyEntries.Add(new ThirdPartyEntry(newEntryTitle, newEntryImage));
                newEntryTitle = "";
                newEntryImage = null;
            }

            if (GUILayout.Button("Save Data"))
            {
                SaveData();
            }
        }

        private void SaveData()
        {
            string dataFolderPath = "Assets/Resources/Splash Screen Data";

            // Create a folder named "Resources" if it doesn't exist
            string resourcesPath = Application.dataPath + "/Resources";
            if (!Directory.Exists(resourcesPath))
            {
                Directory.CreateDirectory(resourcesPath);
                AssetDatabase.Refresh();
            }
            if (!AssetDatabase.IsValidFolder(dataFolderPath))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "Splash Screen Data");
                AssetDatabase.Refresh();
            }

            // Create and save Disclaimer data
            var disclaimerData = ScriptableObject.CreateInstance<DisclaimerData>();
            disclaimerData.DisclaimerTitle = disclaimerTitle;
            disclaimerData.DisclaimerDescription = disclaimerDescription;
            string disclaimerPath = dataFolderPath + "/Disclaimer.asset";
            AssetDatabase.CreateAsset(disclaimerData, disclaimerPath);

            // Create and save EULA data
            var EULAdata = ScriptableObject.CreateInstance<EULAData>();
            EULAdata.EulaTitle = eulaTitle;
            EULAdata.EulaDescription = eulaDescription;
            string eulaPath = dataFolderPath + "/EULA.asset";
            AssetDatabase.CreateAsset(EULAdata, eulaPath);

            // Create and save Third Party data
            var thirdPartyData = ScriptableObject.CreateInstance<ThirdPartyData>();
            thirdPartyData.ThirdPartyEntries = thirdPartyEntries;
            string thirdPartyPath = dataFolderPath + "/ThirdParty.asset";
            AssetDatabase.CreateAsset(thirdPartyData, thirdPartyPath);
        }


        [MenuItem("Studio-23/Splash Screen System/Install")]
        public static void InstallSplashScreenSystem()
        {
            bool installed = AreGameObjectsInstalled();

            if (installed)
            {
                EditorUtility.DisplayDialog("Splash Screen System", "Splash Screen System is already installed.", "OK");
            }
            else
            {
                GameObject canvasObj = GameObject.FindObjectOfType<Canvas>()?.gameObject;

                if (canvasObj == null)
                {
                    canvasObj = new GameObject("SplashScreenCanvas", typeof(Canvas));
                    canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                    canvasObj.AddComponent<CanvasScaler>();
                    canvasObj.AddComponent<GraphicRaycaster>();
                }

                CreateSplashScreenObjects(canvasObj);
            }
        }

        private static bool AreGameObjectsInstalled()
        {
            GameObject[] existingGameObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in existingGameObjects)
            {
                if (obj.name == "Disclaimer" || obj.name == "Eula" || obj.name == "3rd Party")
                {
                    return true;
                }
            }
            return false;
        }

        private static void CreateSplashScreenObjects(GameObject canvas)
        {
            CreateGameObject("Disclaimer", canvas.transform);
            CreateGameObject("Eula", canvas.transform);
            CreateGameObject("3rd Party", canvas.transform);
        }

        private static void CreateGameObject(string name, Transform parent)
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(parent);
            RectTransform rectTransform = obj.AddComponent<RectTransform>();
        }

    }

}