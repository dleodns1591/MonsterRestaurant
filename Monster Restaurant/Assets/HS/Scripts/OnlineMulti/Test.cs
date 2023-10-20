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
        Screen.SetResolution(1920, 1080, false); //â���
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    private IEnumerator Start()
    {
        yield return PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.IsConnected)
        {
            print("����!");
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
            print("�κ�");
        if (PhotonNetwork.CountOfRooms == 0) PhotonNetwork.CreateRoom("1��1�߽Ǻ�");
        else PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinedRoom()
    {
        print("�� ����!");

         PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity);
    }

    public void Click()
    {
        PhotonNetwork.JoinLobby();
    }
}
