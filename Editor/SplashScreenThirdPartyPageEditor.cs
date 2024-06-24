using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Studio23.SS2.SplashScreenSystem.Data;
using System.IO;

public class SplashScreenThirdPartyPageEditor : EditorWindow
{
    private List<ThirdPartyDataEntry> _thirdPartyEntries = new List<ThirdPartyDataEntry>();
    private Texture2D _newEntryImage;

    [MenuItem("Studio-23/Splash Screen System/3rd Party")]
    public static void ShowWindow()
    {
        GetWindow<SplashScreenThirdPartyPageEditor>("3rd Party Data");
    }

    private void OnGUI()
    {
        DrawThirdPartyTab();
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

        _newEntryImage = (Texture2D)EditorGUILayout.ObjectField("Image", _newEntryImage, typeof(Texture2D), false);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (GUILayout.Button("Add"))
        {
            _thirdPartyEntries.Add(new ThirdPartyDataEntry(_newEntryImage));
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

        // Create and save Third Party data
        var thirdPartyData = ScriptableObject.CreateInstance<ThirdPartyData>();
        thirdPartyData.ThirdPartyEntries = _thirdPartyEntries;
        string thirdPartyPath = dataFolderPath + "/ThirdParty.asset";
        AssetDatabase.CreateAsset(thirdPartyData, thirdPartyPath);
    }
}