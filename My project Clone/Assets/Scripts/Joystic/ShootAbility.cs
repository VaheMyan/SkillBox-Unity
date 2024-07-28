using System;
using UnityEngine;
using System.Collections;
using Photon.Pun;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullent;
    public float shootDelay;

    private float _shootTime = float.MinValue;

    public int stats;
    public PlayerStats playerStats;
    public DownloadJSON downloadJSON;

    public ParticleSystem effect;

    private CharacterData _characterData;

    private void Start()
    {
        _characterData = GetComponent<CharacterData>();
        //stats = new PlayerStats();
        var jsonString = PlayerPrefs.GetString("Stats");

        if (!jsonString.Equals(String.Empty, StringComparison.Ordinal)) // stugum a ete json_y datark chi apap =>
        {
            // stats = JsonUtility.FromJson<PlayerStats>(jsonString);
        }
        else
        {
            //stats = new PlayerStats();
        }
    }
    public void Execute()
    {
        if (Time.time < _shootTime + shootDelay) return; // ete jamanaky poqr e _shootTime + shootDelay-ic apa noric (return)

        _shootTime = Time.time;

        if (bullent != null)
        {
            var p = this.transform.position;
            PhotonNetwork.Instantiate(bullent.name, new Vector3(p.x, p.y + 0.8f, p.z), this.transform.rotation);

            effect.Play(); // fier effect Play
            //stats++;
            //downloadJSON.playerStats.ShootCout = stats;
            //Debug.Log("aaaaaaaaaaaaaaaaaaa" + downloadJSON.playerStats.ShootCout);

            _characterData.Score(10);

        }
        else
        {
            Debug.LogError("[SHOOT ABILITY] No prefab link!");
        }

    }
}
