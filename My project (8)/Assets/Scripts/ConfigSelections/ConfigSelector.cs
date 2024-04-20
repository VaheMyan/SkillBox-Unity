using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConfigSelector : MonoBehaviour
{
    [MenuItem("Tools/Clear PlayerPrefs %#d")]
    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs cleared");
    }

    public ScriptableObject selectedConfig;

    public List<ScriptableObject> configList = new List<ScriptableObject>();
    public string[] configNames = new string[] { "Settings", "PlayerStats" };

    void OnEnable()
    {
        string[] guids = AssetDatabase.FindAssets("t:Settings");
        string[] guids2 = AssetDatabase.FindAssets("t:PlayerStats");

        configList.Clear();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (obj != null && obj != selectedConfig)
            {
                configList.Add(obj);
            }
        }
        foreach (string guid in guids2)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

            if (obj != null && obj != selectedConfig)
            {
                configList.Add(obj);
            }
        }
    }

    void OnGUI()
    {
        if (configList.Count > 0)
        {
            for (int i = 0; i < configList.Count; i++)
            {
                configNames[i] = configList[i].name;
            }

            int selectedIndex = configList.IndexOf(selectedConfig);

            if (selectedIndex == -1)
                selectedIndex = 0;

            selectedIndex = EditorGUILayout.Popup("Select Config", selectedIndex, configNames);
            Debug.Log(selectedIndex);

            switch (selectedIndex)
            {
                case 0:
                    selectedConfig = configList[selectedIndex];
                    break;
                case 1:
                    selectedConfig = configList[selectedIndex];
                    break;
            }
        }
    }
}
