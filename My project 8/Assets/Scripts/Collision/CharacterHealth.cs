using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CharacterHealth : MonoBehaviour
{
    public Settings settings;
    public ShootAbility ShootAbility;
    public DownloadJSON downloadJSON;

    public int _health = int.MaxValue;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            if (_health <= 0)
            {
                WriteStatisctics();
                Destroy(this.gameObject);

            }
        }
    }


    private void WriteStatisctics()
    {
        var jsonString = JsonUtility.ToJson(ShootAbility.stats);
        PlayerPrefs.SetString("Stats", jsonString);
        PlayerPrefs.Save();
        Debug.Log("Shoot Cout Saved and it is : " + jsonString);
    }

    private void Start()
    {
        var jsonString = JsonUtility.ToJson(ShootAbility.stats);
        Debug.Log("Shoot Cout is : " + jsonString);

        Health = settings.HeroHealth;

    }

}
