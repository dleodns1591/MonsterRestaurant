using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviourPunCallbacks
{
    public int count;

    public GameObject Player1, Player2;


    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false); //창모드
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    private IEnumerator Start()
    {
        yield return PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.IsConnected)
        {
            print("접속!");
        }
        else
        {
            Application.Quit();
        }
    }

    private void Update()
    {
        if(Player2 != null)
            Player2.transform.localPosition = new Vector3(52, 470);
    }

    public override void OnJoinedLobby()
    {
            print("로비");
        if (PhotonNetwork.CountOfRooms == 0) PhotonNetwork.CreateRoom("1대1뜨실분");
        else PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinedRoom()
    {
        print("방 접속!");

         PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity);
    }

    public void Click()
    {
        PhotonNetwork.JoinLobby();
    }
}
