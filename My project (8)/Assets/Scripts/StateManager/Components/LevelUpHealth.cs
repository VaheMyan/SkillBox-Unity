using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpHealth : MonoBehaviour, ILevelUp
{
    public int MinLevel => minLevel;

    public int minLevel;

    private CharacterHealth _health;

    public void LevelUp(CharacterData data, int level)
    {
        if (_health == null)
        {
            _health = GetComponent<CharacterHealth>();
            if (_health == null) return;
        }

        if (data.CurrentLevel >= MinLevel)
        {
            _health.Health += 10;
        }
    }
}
