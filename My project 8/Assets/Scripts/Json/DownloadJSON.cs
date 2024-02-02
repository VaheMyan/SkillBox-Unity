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
        public int playerScore;
    }

    ShootAbility shootAbility;
    string fileName;
    public int playerScore;

    private void Update()
    {
        //Debug.Log(data.ShootCout);
    }

    [Obsolete]
    public void Post() => StartCoroutine(PostData());

    [Obsolete]
    public IEnumerator PostData()
    {
        Debug.Log("Loading...");
        string url = "https://drive.google.com/uc?export=download&id=1Hi2Uh1zxa6oYNrzPbRax93vugPe9Isf0";

        string jsonData = JsonUtility.ToJson(shootAbility.stats);

        using (UnityWebRequest request = UnityWebRequest.Post(url, "Post"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Recuest successful");
                Debug.Log("Shoot Cout is " + shootAbility.stats);
            }

        }
    }

    [Obsolete]
    public void Get() => StartCoroutine(GetData());

    [Obsolete]
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

            }
        }
    }

    // Метод для сохранения данных в JSON
    public void SavePlayerStatsToLocal(PlayerStats playerStats, string fileName)
    {
        string json = JsonUtility.ToJson(playerStats);

        // Полный путь к файлу в Application.persistentDataPath
        string savePath = Path.Combine(Application.persistentDataPath, fileName);

        // Сохранение JSON в файл
        File.WriteAllText(savePath, json);

        Debug.Log("PlayerStats saved to: " + savePath);
    }

    // Метод для загрузки данных из JSON
    public PlayerStats LoadPlayerStatsFromLocal(string fileName)
    {
        // Полный путь к файлу в Application.persistentDataPath
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Проверка наличия файла перед загрузкой
        if (File.Exists(filePath))
        {
            // Чтение JSON из файла и десериализация в объект PlayerStats
            string json = File.ReadAllText(filePath);
            PlayerStats loadedPlayerStats = JsonUtility.FromJson<PlayerStats>(json);

            Debug.Log("Player Score: " + loadedPlayerStats.playerScore);

            return loadedPlayerStats;
        }
        else
        {
            Debug.Log("No saved PlayerStats found.");
            return null;
        }
    }

    private void Start()
    {
        // Пример использования сохранения и загрузки PlayerStats
        PlayerStats playerStatsToSave = new PlayerStats
        {
            playerScore = 100
        };

        SavePlayerStatsToLocal(playerStatsToSave, "PlayerStats.json");

        PlayerStats loadedPlayerStats = LoadPlayerStatsFromLocal("PlayerStats.json");
    }
}
