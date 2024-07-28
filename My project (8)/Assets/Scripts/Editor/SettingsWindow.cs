using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

public class SettingsWindow : EditorWindow
{
    private string[] settingsList;

    private bool buttonPressed;

    [MenuItem("Window/Game Settings Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SettingsWindow));
    }

    private void OnGUI()
    {
        settingsList = AssetDatabase.FindAssets("t:settings");
        GUILayout.Label("Game Settings", EditorStyles.boldLabel);
        GUILayout.Space(10);
        foreach (var file in settingsList)
        {
            GUILayout.Label(AssetDatabase.GUIDToAssetPath(file), EditorStyles.label);
        }
        buttonPressed = GUILayout.Button("Adjust Health");
        if (buttonPressed)
        {
            foreach (var file in settingsList)
            {
                var settingsFile = AssetDatabase.LoadAssetAtPath<Settings>(AssetDatabase.GUIDToAssetPath(file));
                settingsFile.HeroHealth += 12;
            }
            AssetDatabase.SaveAssets();
        }
    }
}
