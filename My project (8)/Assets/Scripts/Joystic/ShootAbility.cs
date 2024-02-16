using System;
using UnityEngine;

public class ShootAbility : MonoBehaviour, IAbility
{
    public GameObject bullent;
    public float shootDelay;

    private float _shootTime = float.MinValue;

    public int stats;
    public PlayerStats playerStats;
    public DownloadJSON downloadJSON;

    private void Start()
    {
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
            var t = transform;
            var newBullet = Instantiate(bullent, t.position, t.rotation);
            stats++;
            downloadJSON.playerStats.ShootCout = stats;
            Debug.Log("aaaaaaaaaaaaaaaaaaa" + downloadJSON.playerStats.ShootCout);

        }
        else
        {
            Debug.LogError("[SHOOT ABILITY] No prefab link!");
        }

    }
}
