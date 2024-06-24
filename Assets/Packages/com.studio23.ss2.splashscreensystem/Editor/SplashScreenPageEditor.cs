using UnityEngine;
using UnityEditor;
using System.IO;
using Studio23.SS2.SplashScreenSystem.Data;
using TMPro;

namespace Studio23.SS2.SplashScreenSystem.Editor
{
    public class SplashScreenPageEditor : EditorWindow
    {
        private string _pageTitle = "";
        private string _pageDescription = "";
        private FontData _pageTitleFontSettings;
        private FontData _pageDescriptionFontSettings;
        public bool _isInteractable;

        [MenuItem("Studio-23/Splash Screen System/Page")]
        public static void ShowWindow()
        {
            
            GetWindow<SplashScreenPageEditor>("Splash Screen Data");
        }

        private void OnGUI()
        {
            DrawPage();
        }

        private void DrawPage()
        {
            GUILayout.Label("Page", EditorStyles.boldLabel);
            _pageTitle = EditorGUILayout.TextField("Title", _pageTitle);
            _pageDescription = EditorGUILayout.TextField("Description", _pageDescription, GUILayout.Height(100));

            GUILayout.Label("Title Font Settings", EditorStyles.boldLabel);
            _pageTitleFontSettings = DrawTextSettingsFields(_pageTitleFontSettings);

            GUILayout.Label("Description Font Settings", EditorStyles.boldLabel);
            _pageDescriptionFontSettings = DrawTextSettingsFields(_pageDescriptionFontSettings);

            _isInteractable = EditorGUILayout.Toggle("Is Interactable", _isInteractable);


            GUILayout.Space(20);


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
            var pageData = ScriptableObject.CreateInstance<ScreenTextData>();
            //pageData.Title = _pageTitle;
            //pageData.Description = _pageDescription;
            pageData.TitleFontData = new FontData(_pageTitleFontSettings);
            pageData.DescriptionFontData = new FontData(_pageDescriptionFontSettings);
            pageData.IsInteractable = _isInteractable;
            string disclaimerPath = dataFolderPath + $"/{_pageTitle}.asset";
            AssetDatabase.CreateAsset(pageData, disclaimerPath);
        }
    }
}