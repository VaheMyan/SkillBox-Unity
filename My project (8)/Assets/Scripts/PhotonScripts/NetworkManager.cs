using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.Entities;

public class NetworkManager : MonoBehaviourPunCallbacks, IConvertGameObjectToEntity
{
    public GameObject PlayerSample;
    public GameObject TrapShrink;
    public GameObject[] Items;
    public List<Transform> SpawnPoints;
    public SoundPlayer soundPlayer;

    public bool endDisconnected = false;

    private Entity _entity;
    private EntityManager _dsManager;
    private GameObjectConversionSystem _conversionSystem;

    void Start()
    {
        StartConected();
    }
    public void StartConected()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }
    public void StartDisconnected()
    {
        endDisconnected = false;
        Debug.Log("Disconnecting...");

        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = false
        };
        PhotonNetwork.JoinOrCreateRoom("Test", options, TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnection was successful.");
        endDisconnected = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Connection was successful.");

        var id = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players and ID is " + id);

        Vector3 spawnPosition = SpawnPoints[id % SpawnPoints.Count].position;
        var Player = PhotonNetwork.Instantiate(PlayerSample.name, spawnPosition, Quaternion.identity, 0);
        Player.name = "Player " + id;

        soundPlayer.StartBackgroundMusic();

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            this.photonView.RPC("RPC_ChangePlayerName", RpcTarget.Others, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(TrapShrink.name, TrapShrink.transform.position, TrapShrink.transform.rotation);

            foreach (GameObject item in Items)
            {
                var _item = PhotonNetwork.Instantiate(item.name, item.transform.position, item.transform.rotation);
            }
        }
    }
    public void SayHello()
    {
        this.photonView.RPC("Hello", RpcTarget.Others, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
    }
    public void SendPlayerID(Player targetPlayer)
    {
        this.photonView.RPC("RPC_ChangePlayerName", targetPlayer, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        SendPlayerID(newPlayer);
    }
    public void IsDestroyedItem(string itemName)
    {
        this.photonView.RPC("DestroyItem", RpcTarget.Others, (string)itemName);
    }

    [PunRPC]
    public void Hello(byte playerID)
    {
        Debug.Log($"Player ID {playerID} said Hello!");
    }
    [PunRPC]
    public void RPC_ChangePlayerName(byte myActorNumber)
    {
        var otherPlayer = GameObject.Find("DogPBR(Clone)");
        if (otherPlayer != null)
        {
            otherPlayer.name = "Player" + myActorNumber;
        }
    }
    [PunRPC]
    public void DestroyItem(string itemName)
    {
        if (PhotonNetwork.IsMasterClient && GameObject.Find(itemName) != null)
        {
            PhotonNetwork.Destroy(GameObject.Find(itemName));

            DestroyTargetEntity(_conversionSystem, itemName);

            Debug.Log(itemName + " is destroyed");
        }
    }

    private void DestroyTargetEntity(GameObjectConversionSystem conversionSystem, string itemName)
    {
        if (GameObject.Find(itemName) != null)
        {
            var targetEntity = conversionSystem.GetPrimaryEntity(GameObject.Find(itemName));
            _dsManager.DestroyEntity(targetEntity);
        }
    }
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        _dsManager = dstManager;
        _entity = entity;
        _conversionSystem = conversionSystem;
    }
}