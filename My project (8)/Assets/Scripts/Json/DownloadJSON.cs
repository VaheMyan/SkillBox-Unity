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

    public ShootAbility shootAbility;
    public PlayerStats playerStats;

    //[Obsolete]
    //public void Post() => StartCoroutine(PostData());

    //[Obsolete]
    public void PostData()
    {
        PlayerStats playerStatsToSave = new PlayerStats
        {
            ShootCout = playerStats.ShootCout
        };

        SavePlayerStatsToLocal(playerStatsToSave, "PlayerStats.json");

        //    Debug.Log("Loading...");
        //    string url = "https://drive.google.com/uc?export=download&id=1Hi2Uh1zxa6oYNrzPbRax93vugPe9Isf0";

        //    string jsonData = JsonUtility.ToJson(shootAbility.stats);

        //    using (UnityWebRequest request = UnityWebRequest.Post(url, "Post"))
        //    {
        //        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        //        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        //        request.downloadHandler = new DownloadHandlerBuffer();
        //        request.SetRequestHeader("Content-Type", "application/json");

        //        yield return request.SendWebRequest();
        //        if (request.isNetworkError || request.isHttpError)
        //        {
        //            Debug.Log(request.error);
        //        }
        //        else
        //        {
        //            Debug.Log("Recuest successful");
        //            Debug.Log("Shoot Cout is " + shootAbility.stats);
        //        }

        //    }
    }

    //[Obsolete]
    //public void Get() => StartCoroutine(GetData());

    //[Obsolete]
    public void GetData()
    {
        PlayerStats loadedPlayerStats = LoadPlayerStatsFromLocal("PlayerStats.json");
        //Debug.Log("Loading...");
        //string url = "https://drive.google.com/uc?export=download&id=1Hi2Uh1zxa6oYNrzPbRax93vugPe9Isf0";
        //using (UnityWebRequest request = UnityWebRequest.Get(url))
        //{
        //    yield return request.SendWebRequest();
        //    if (request.isNetworkError || request.isHttpError)
        //    {
        //        Debug.Log(request.error);
        //    }
        //    else
        //    {

        //    }
        //}
    }

    public void SavePlayerStatsToLocal(PlayerStats playerStats, string fileName)
    {
        string json = JsonUtility.ToJson(playerStats);

        string savePath = Path.Combine(Application.persistentDataPath, fileName); // asum e te vortegh petq e pahpanvi PlayerStats.json fayly

        // teghadrum e json fayly savePath-i mej
        File.WriteAllText(savePath, json);

        Debug.Log("PlayerStats saved to: " + savePath);
        Debug.Log(json);
    }

    // Метод для загрузки данных из JSON
    public PlayerStats LoadPlayerStatsFromLocal(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName); // gtnum e PlayerStats.json fayli hascen "C:/Users/User/AppSata/..."

        if (File.Exists(filePath)) // stugum e ardyoq fayly goyutyun uni
        {
            string json = File.ReadAllText(filePath); // kardum e filePath-y
            PlayerStats loadedPlayerStats = JsonUtility.FromJson<PlayerStats>(json); // ev poxakerpum PlayerStats-i

            Debug.Log("Shoot Cout is: " + loadedPlayerStats.ShootCout);

            return loadedPlayerStats;
        }
        else
        {
            Debug.Log("No saved PlayerStats found.");
            return null;
        }
    }
}
