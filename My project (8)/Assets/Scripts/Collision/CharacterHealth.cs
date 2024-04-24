using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System.Threading.Tasks;

public class CharacterHealth : MonoBehaviour
{
    public Settings settings;
    public ShootAbility ShootAbility;
    public DownloadJSON downloadJSON;
    public UserInputSystem userInputSystem;

    public int _health = int.MaxValue;

    private ViewModel viewModel;

    public bool isDie = false;
    public bool isDisable = false;

    public int Health
    {
        get => _health;
        set
        {
            if (_health == value) return;
            _health = value;
            if (viewModel != null) viewModel.Health = _health.ToString();
            if (_health <= 0)
            {
                Die();
                //WriteStatisctics();
                //Destroy(this.gameObject);

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
        viewModel = FindObjectOfType<ViewModel>();

        var jsonString = JsonUtility.ToJson(ShootAbility.stats);
        //Debug.Log("Shoot Cout is : " + jsonString);

        Health = settings.HeroHealth;
    }
    public async void Die()
    {
        isDie = true;
        await Task.Delay(2500);
        isDisable = true;
        Debug.Log("STOP!");
        Time.timeScale = 0f;
    }

}
