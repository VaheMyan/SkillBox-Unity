using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerSample;
    public List<Transform> SpawnPoints;
    private ShrinkAbility shrinkAbiliti;

    public bool endDisconnected = false;

    void Start()
    {
        StartConected();
    }
    public void StartConected()
    {
        shrinkAbiliti = GameObject.Find("Trap (1)").GetComponent<ShrinkAbility>();

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

        PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[0].position, Quaternion.identity);
        shrinkAbiliti.FindGameObjectWithTag("Player");

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

    [PunRPC]
    public void Hello(byte playerID)
    {
        Debug.Log($"Player ID {playerID} said Hello!");
    }
}
