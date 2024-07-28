using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    public AK.Wwise.Event deathEvent = null;
    public Settings settings;
    public ShootAbility ShootAbility;
    public DownloadJSON downloadJSON;
    public UserInputSystem userInputSystem;
    public int _health = int.MaxValue;
    public bool isDie = false;
    public bool isDisable = false;

    private Entity _entity;
    private EntityManager _dsManager;
    private Image _deathPanel;
    private Image _death;
    private Text _wastedText;
    private ViewModel viewModel;
    SoundPlayer soundPlayer;

    [HideInInspector] public bool isGetHitAnim = false;

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
        isDisable = false;

        soundPlayer = GameObject.Find("GameManager").GetComponent<SoundPlayer>();
        _deathPanel = GameObject.Find("Death").GetComponent<Image>();
        _death = GameObject.Find("DeathPanel").GetComponent<Image>();
        _wastedText = GameObject.Find("WastedText").GetComponent<Text>();

        viewModel = FindObjectOfType<ViewModel>();

        var jsonString = JsonUtility.ToJson(ShootAbility.stats);
        //Debug.Log("Shoot Cout is : " + jsonString);

        Health = settings.HeroHealth;
    }

    public async void Die()
    {
        if (_entity != Entity.Null && _dsManager != null)
        {
            isDisable = true;
            soundPlayer.StopBackgroundMusic();

            deathEvent.Post(this.gameObject);
            isDie = true;
            _deathPanel.enabled = true;
            await Task.Delay(2450);
            _death.enabled = true;
            _wastedText.enabled = true;

            _dsManager.DestroyEntity(_entity);
            Destroy(this.gameObject);


            //await Task.Delay(2500);
            //PhotonNetwork.Destroy(this.gameObject);
            //await Task.Delay(2000);
            //Debug.Log("Restart");
            //networkManager.StartDisconnected();
            //await Task.Delay(4000);
            //if (networkManager.endDisconnected)
            //{
            //    networkManager.StartConected();
            //}
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
