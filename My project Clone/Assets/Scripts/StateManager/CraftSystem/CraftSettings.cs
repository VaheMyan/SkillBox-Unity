using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "CraftSettings", menuName = "Craft Create Data")]
public class CraftSettings : ScriptableObject
{
    public List<CraftCombination> combinations;
}

[Serializable]
public class CraftCombination
{
    public List<string> sources;
    public GameObject result;
}