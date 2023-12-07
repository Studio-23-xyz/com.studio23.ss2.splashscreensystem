using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Studio23.SS2.SplashScreenSystem.Data;


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

        [MenuItem("Studio-23/Splash Screen System")]
        public static void ShowWindow()
        {
            GetWindow<SplashScreenEditor>("Splash Screen System");
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
    }

    //    private void SaveData()
    //    {
    //        // Create a folder named "Resources" if it doesn't exist
    //        string resourcesPath = Application.dataPath + "/Resources";
    //        if (!Directory.Exists(resourcesPath))
    //        {
    //            Directory.CreateDirectory(resourcesPath);
    //            AssetDatabase.Refresh();
    //        }

    //        // Create a folder named "Splash Screen Data" inside "Resources" if it doesn't exist
    //        string dataFolderPath = "Assets/Resources/Splash Screen Data";
    //        if (!AssetDatabase.IsValidFolder(dataFolderPath))
    //        {
    //            AssetDatabase.CreateFolder("Assets/Resources", "Splash Screen Data");
    //            AssetDatabase.Refresh();
    //        }

    //        // Create a ScriptableObject instance and save it in the specified path
    //        var customData = ScriptableObject.CreateInstance<SplashScreenData>();
    //        customData.DisclaimerTitle = disclaimerTitle;
    //        customData.DisclaimerDescription = disclaimerDescription;
    //        customData.EulaTitle = eulaTitle;
    //        customData.EulaDescription = eulaDescription;
    //        customData.ThirdPartyEntries = thirdPartyEntries.ToArray();


    //        string path = "Assets/Resources/Splash Screen Data/Splash Screen Data.asset";
    //        AssetDatabase.CreateAsset(customData, path);
    //        AssetDatabase.SaveAssets();
    //        AssetDatabase.Refresh();
    //        EditorUtility.SetDirty(this);

    //        Debug.Log("ScriptableObject created and saved at path: " + path);
    //    }
    //}

}

