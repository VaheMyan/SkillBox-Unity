using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DownloadJSON : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int ShootCout;
    }

    public PlayerStats playerStats;

    public void PostData()
    {
        Debug.Log("Loading...");

        string filePath = Path.Combine(Application.persistentDataPath, "PlayerStats.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerStats loadedPlayerStats = JsonUtility.FromJson<PlayerStats>(json);

            Debug.Log("Json: " + loadedPlayerStats);

        }
        else
        {
            Debug.Log("No saved PlayerStats found.");
        }
    }

    public void Get() => StartCoroutine(GetData());

    public IEnumerator GetData()
    {
        Debug.Log("Loading...");
        string url = "https://drive.google.com/uc?export=download&id=1Hi2Uh1zxa6oYNrzPbRax93vugPe9Isf0";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                playerStats.ShootCout = int.Parse(request.downloadHandler.text);
                string json = JsonUtility.ToJson(playerStats);
                string savePath = Path.Combine(Application.persistentDataPath, "PlayerStats.json");
                File.WriteAllText(savePath, json);

                Debug.Log("Path: " + savePath + "  Json: " + json);
            }
        }
    }
}
