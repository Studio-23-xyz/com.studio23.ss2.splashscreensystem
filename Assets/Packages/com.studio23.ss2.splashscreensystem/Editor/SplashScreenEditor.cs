using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using Studio23.SS2.SplashScreenSystem.Data;
using TMPro;
using UnityEngine.TextCore.Text;

namespace Studio23.SS2.SplashScreenSystem.Editor
{
    public class SplashScreenEditor : EditorWindow
    {
        private int _selectedTab = 0;
        private string _disclaimerTitle = "";
        private string _disclaimerDescription = "";
        private FontData _disclaimerTitleFontSettings;
        private FontData _disclaimerDescriptionFontSettings;
        private string _eulaTitle = "";
        private string _eulaDescription = "";
        private FontData _EULATitleFontSettings;
        private FontData _EULADescriptionFontSettings;

        private List<ThirdPartyDataEntry> _thirdPartyEntries = new List<ThirdPartyDataEntry>();
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

            GUILayout.Label("Title Font Settings", EditorStyles.boldLabel);
            _disclaimerTitleFontSettings = DrawTextSettingsFields(_disclaimerTitleFontSettings);

            GUILayout.Label("Description Font Settings", EditorStyles.boldLabel);
            _disclaimerDescriptionFontSettings = DrawTextSettingsFields(_disclaimerDescriptionFontSettings);


            GUILayout.Space(20);
        }

        private void DrawEulaTab()
        {
            GUILayout.Label("EULA", EditorStyles.boldLabel);
            _eulaTitle = EditorGUILayout.TextField("Title", _eulaTitle);
            _eulaDescription = EditorGUILayout.TextField("Description", _eulaDescription, GUILayout.Height(100));

            GUILayout.Label("Title Font Settings", EditorStyles.boldLabel);
            _EULATitleFontSettings = DrawTextSettingsFields(_EULATitleFontSettings);

            GUILayout.Label("Description Font Settings", EditorStyles.boldLabel);
            _EULADescriptionFontSettings = DrawTextSettingsFields(_EULADescriptionFontSettings);

            GUILayout.Space(20);
        }

        private void DrawThirdPartyTab()
        {
            GUILayout.Label("3rd Party Container", EditorStyles.boldLabel);

            foreach (var entry in _thirdPartyEntries)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Box(entry.Image, GUILayout.Width(50), GUILayout.Height(50));
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            //_newEntryTitle = EditorGUILayout.TextField("Title", _newEntryTitle);
            _newEntryImage = (Texture2D)EditorGUILayout.ObjectField("Image", _newEntryImage, typeof(Texture2D), false);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Add"))
            {
                _thirdPartyEntries.Add(new ThirdPartyDataEntry( _newEntryImage));
                //_newEntryTitle = "";
                _newEntryImage = null;
            }

            if (GUILayout.Button("Save Data"))
            {
                SaveData();
            }
        }


        private FontData DrawTextSettingsFields(FontData textSettings)
        {
            if (textSettings == null)
                textSettings = new FontData();

            textSettings.FontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", textSettings.FontAsset,
                typeof(TMP_FontAsset), false);
            textSettings.FontStyle = (TMPro.FontStyles)EditorGUILayout.EnumPopup("Font Style", textSettings.FontStyle);

            return textSettings;
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
            disclaimerData.TitleFontData = new FontData(_disclaimerTitleFontSettings);
            disclaimerData.DescriptionFontData = new FontData(_disclaimerDescriptionFontSettings); ;
            string disclaimerPath = dataFolderPath + "/Disclaimer.asset";
            AssetDatabase.CreateAsset(disclaimerData, disclaimerPath);

            // Create and save EULA data
            var EULAdata = ScriptableObject.CreateInstance<ScreenTextData>();
            EULAdata.Title = _eulaTitle;
            EULAdata.Description = _eulaDescription;
            EULAdata.ShowButton = true;

            EULAdata.TitleFontData = new FontData(_EULATitleFontSettings);
            EULAdata.DescriptionFontData = new FontData(_EULADescriptionFontSettings);

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