using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System.Threading.Tasks;
using Photon.Pun;

public class CharacterHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    public Settings settings;
    public ShootAbility ShootAbility;
    public DownloadJSON downloadJSON;
    public UserInputSystem userInputSystem;
    private NetworkManager networkManager;

    public int _health = int.MaxValue;

    private ViewModel viewModel;

    public bool isDie = false;
    public bool isDisable = false;

    private Entity _entity;
    private EntityManager _dsManager;

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

        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    public async void Die()
    {
        if (_entity != Entity.Null && _dsManager != null)
        {
            isDie = true;
            await Task.Delay(2500);
            PhotonNetwork.Destroy(this.gameObject);
            _dsManager.DestroyEntity(_entity);
            await Task.Delay(2000);
            Debug.Log("Restart");
            networkManager.StartDisconnected();
            await Task.Delay(4000);
            if (networkManager.endDisconnected)
            {
                networkManager.StartConected();
            }
        }
        else
        {
            Debug.LogWarning("Entity or EntityManager is not set properly.");
        }
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _entity = entity;
        _dsManager = dstManager;
    }
}
