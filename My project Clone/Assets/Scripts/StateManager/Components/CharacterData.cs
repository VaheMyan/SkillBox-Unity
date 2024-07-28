using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public GameObject InventoryUIRoot;
    public List<MonoBehaviour> levelUpActions;

    public int currentLevel = 1;
    public int score = 0;
    public int scoreToNextLevel = 20;

    private List<IItem> _items;
    public int CurrentLevel => currentLevel;
    public void Score(int scoreAmout)
    {
        score += scoreAmout;
        if (score >= scoreToNextLevel) LevelUp();
    }

    private void LevelUp()
    {
        currentLevel++;
        scoreToNextLevel *= 2;
        foreach (var action in levelUpActions)
        {
            if (!(action is ILevelUp levelUp)) return;
            levelUp.LevelUp(this, currentLevel);
        }
    }
    private void Start()
    {
        InventoryUIRoot = GameObject.Find("Panel");
        if (InventoryUIRoot != null) GameObject.Find("Inventory").SetActive(false);
    }
}
