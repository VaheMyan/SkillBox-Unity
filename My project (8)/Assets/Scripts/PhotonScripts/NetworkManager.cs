using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.Entities;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerSample;
    public GameObject TrapShrink;
    public GameObject[] Items;
    public List<Transform> SpawnPoints;

    public bool endDisconnected = false;
    private Entity _entity;
    private EntityManager _dsManager;

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

        var Player = PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[0].position, Quaternion.identity);
        string MyPlayerId = "Player" + PhotonNetwork.LocalPlayer.ActorNumber;
        Player.name = MyPlayerId;

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            this.photonView.RPC("RPC_ChangePlayerName", RpcTarget.Others, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
        }

        //PhotonNetwork.Instantiate(TrapShrink.name, TrapShrink.transform.position, TrapShrink.transform.rotation);

        //for (int i = 0; i < Items.Length; i++)
        //{
        //    PhotonNetwork.Instantiate(Items[i].name, Items[i].transform.position, Items[i].transform.rotation);
        //}

        if (photonView.IsMine)
        {
            PhotonNetwork.Instantiate(TrapShrink.name, TrapShrink.transform.position, TrapShrink.transform.rotation);

            for (int i = 0; i < Items.Length; i++)
            {
                PhotonNetwork.Instantiate(Items[i].name, Items[i].transform.position, Items[i].transform.rotation);
            }
        }

        //_player = PhotonNetwork.LocalPlayer;
        //PhotonNetwork.SetMasterClient(_player);

        //if (id > (SpawnPoints.Count + 1))
        //{
        //    PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[0].position, Quaternion.identity);

        //    //Debug.LogError("No SPAWN POINT");
        //}
        //else
        //{
        //    PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[id - 1].position, Quaternion.identity);
        //    shrinkAbiliti.FindGameObjectWithTag("Player");
        //}
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
        otherPlayer.name = "Player" + myActorNumber;
    }
    [PunRPC]
    public void DestroyItem(string itemName)
    {
        if (PhotonNetwork.IsMasterClient && GameObject.Find(itemName) != null)
        {
            PhotonNetwork.Destroy(GameObject.Find(itemName));

            _dsManager.DestroyEntity(GameObject.Find(itemName).GetComponent<Entity>());

            Debug.Log(itemName + " is destroyed");
        }
    }

    public void Test()
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
    }
}
