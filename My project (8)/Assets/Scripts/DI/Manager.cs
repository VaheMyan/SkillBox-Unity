using Zenject;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 1)]
public class Data : ScriptableObject
{
    public int _health;
    public int _shootCout;
}

public class PlayerDataLoader : IDataProvider
{
    private Data data;

    public PlayerDataLoader(Data data)
    {
        this.data = data;
    }
    public void LoadData()
    {
        Debug.Log("Lading...");
        Debug.Log("Health: " + data._health);
        Debug.Log("Shoot Cout: " + data._shootCout);
    }
}
public class DummyDataProvider : IDataProvider
{
    private int _health = 100;
    private int _shootCout = 10;

    public void LoadData()
    {
        Debug.Log("Lading...");
        Debug.Log("Health: " + _health);
        Debug.Log("Shoot Cout: " + _shootCout);
    }
}

public class Manager : MonoBehaviour
{

}
