using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class ConfigSelector : MonoBehaviour
{
    public enum ConfigType
    {
        Settings,
        PlayerStats
    }

    public ScriptableObject selectedConfig;
    public ConfigType currentConfigType = ConfigType.Settings;

    public List<ScriptableObject> settingsList = new List<ScriptableObject>();
    public List<ScriptableObject> playerStatsList = new List<ScriptableObject>();

    public TMP_Dropdown configTypeDropdown;

    void OnEnable()
    {
        configTypeDropdown.ClearOptions();
        configTypeDropdown.AddOptions(new List<string> { "Settings", "PlayerStats" });

        configTypeDropdown.value = (int)currentConfigType;
        configTypeDropdown.onValueChanged.AddListener(OnConfigTypeChanged);
    }

    void OnConfigTypeChanged(int index)
    {
        currentConfigType = (ConfigType)index;
        UpdateSelectedConfig();
    }

    void UpdateSelectedConfig()
    {
        switch (currentConfigType)
        {
            case ConfigType.Settings:
                if (settingsList.Count > 0)
                {
                    selectedConfig = settingsList[0];
                }
                break;
            case ConfigType.PlayerStats:
                if (playerStatsList.Count > 0)
                {
                    selectedConfig = playerStatsList[0];
                }
                break;
        }
        UseSelectedConfig();
    }

    void UseSelectedConfig()
    {
        Debug.Log("Config name is a: " + selectedConfig.name);
    }
}
