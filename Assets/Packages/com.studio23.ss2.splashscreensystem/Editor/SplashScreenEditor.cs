using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Studio23.SS2.SplashScreenSystem.Data;

namespace Studio23.SS2.SplashScreenSystem.Editor
{
    public class SplashScreenEditor : EditorWindow
    {
        private int _selectedTab = 0;
        private string _disclaimerTitle = "";
        private string _disclaimerDescription = "";
        private string _eulaTitle = "";
        private string _eulaDescription = "";
        private List<ThirdPartyDataEntry> _thirdPartyEntries = new List<ThirdPartyDataEntry>();
        private string _newEntryTitle = "";
        private Texture2D _newEntryImage;

        [MenuItem("Studio-23/Splash Screen System/Widget")]
        public static void ShowWindow()
        {
            GetWindow<SplashScreenEditor>("Splash Screen System Widget");
        }

        private void OnGUI()
        {
            DrawTabs();

            switch (_selectedTab)
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
            if (GUILayout.Toggle(_selectedTab == 0, "Disclaimer", "Button"))
            {
                _selectedTab = 0;
            }
            if (GUILayout.Toggle(_selectedTab == 1, "EULA", "Button"))
            {
                _selectedTab = 1;
            }
            if (GUILayout.Toggle(_selectedTab == 2, "3rd Party Container", "Button"))
            {
                _selectedTab = 2;
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(20);
        }

        private void DrawDisclaimerTab()
        {
            GUILayout.Label("Disclaimer", EditorStyles.boldLabel);
            _disclaimerTitle = EditorGUILayout.TextField("Title", _disclaimerTitle);
            _disclaimerDescription = EditorGUILayout.TextField("Description", _disclaimerDescription, GUILayout.Height(100));
            GUILayout.Space(20);
        }

        private void DrawEulaTab()
        {
            GUILayout.Label("EULA", EditorStyles.boldLabel);
            _eulaTitle = EditorGUILayout.TextField("Title", _eulaTitle);
            _eulaDescription = EditorGUILayout.TextField("Description", _eulaDescription, GUILayout.Height(100));
            GUILayout.Space(20);
        }

        private void DrawThirdPartyTab()
        {
            GUILayout.Label("3rd Party Container", EditorStyles.boldLabel);

            foreach (var entry in _thirdPartyEntries)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(entry.title);
                GUILayout.Box(entry.image, GUILayout.Width(50), GUILayout.Height(50));
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            _newEntryTitle = EditorGUILayout.TextField("Title", _newEntryTitle);
            _newEntryImage = (Texture2D)EditorGUILayout.ObjectField("Image", _newEntryImage, typeof(Texture2D), false);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Add"))
            {
                _thirdPartyEntries.Add(new ThirdPartyDataEntry(_newEntryTitle, _newEntryImage));
                _newEntryTitle = "";
                _newEntryImage = null;
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
            var disclaimerData = ScriptableObject.CreateInstance<ScreenTextData>();
            disclaimerData.Title = _disclaimerTitle;
            disclaimerData.Description = _disclaimerDescription;
            string disclaimerPath = dataFolderPath + "/Disclaimer.asset";
            AssetDatabase.CreateAsset(disclaimerData, disclaimerPath);

            // Create and save EULA data
            var EULAdata = ScriptableObject.CreateInstance<ScreenTextData>();
            EULAdata.Title = _eulaTitle;
            EULAdata.Description = _eulaDescription;
            EULAdata.ShowButton = true;
            string eulaPath = dataFolderPath + "/EULA.asset";
            AssetDatabase.CreateAsset(EULAdata, eulaPath);

            // Create and save Third Party data
            var thirdPartyData = ScriptableObject.CreateInstance<ThirdPartyData>();
            thirdPartyData.ThirdPartyEntries = _thirdPartyEntries;
            string thirdPartyPath = dataFolderPath + "/ThirdParty.asset";
            AssetDatabase.CreateAsset(thirdPartyData, thirdPartyPath);
        }
    }
}