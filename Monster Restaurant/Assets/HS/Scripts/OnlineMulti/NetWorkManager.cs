using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public int count;

    public GameObject Player1, Player2;

    public Action CountPlus;

    [SerializeField] private GameObject DisConnectBackground;
    [SerializeField] private GameObject DisConnectIcon;
    [SerializeField] private TextMeshProUGUI CurStateText;
    [SerializeField] private GameObject Managers;

    private bool DisconnectCorutineDelay;

    private void Awake()
    {
        Managers.GetComponent<GameManager>().enabled = false;
        Managers.GetComponent<OrderManager>().enabled = false;
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
    }

    private IEnumerator Start()
    {
        yield return PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.IsConnected)
        {
            print("����!");

            //yield return new WaitForSeconds(10f);
            //RoomOptions roomOptions = new RoomOptions();
            //roomOptions.MaxPlayers = 2;
            //if (PhotonNetwork.CountOfRooms == 0) PhotonNetwork.CreateRoom("1��1�߽Ǻ�", roomOptions, null);
            //else PhotonNetwork.JoinRandomRoom();

        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("�� �� �ִ� ���� ����");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedLobby()
    {
        print("�κ�");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        if (PhotonNetwork.CountOfRooms == 0)
            PhotonNetwork.CreateRoom("���ο� ��", roomOptions, null);
        else
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (DisconnectCorutineDelay == false)
        {
            DisConnectBackground.SetActive(true);
            DisConnectIcon.gameObject.SetActive(true);
            CurStateText.gameObject.SetActive(true);

            DisconnectCorutineDelay = true;
            StartCoroutine(ConnectTry());
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        ConnectPopupSetting();
    }

    public override void OnJoinedRoom()
    {
        print("�� ����!");
        PhotonNetwork.CurrentRoom.MaxPlayers = 2;
        StartCoroutine(FindPlayer());
    }
    public IEnumerator FindPlayer()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            CurStateText.text = "��븦 ã�� �ֽ��ϴ�...";
                yield return new WaitForSecondsRealtime(0.3f);
                StartCoroutine(FindPlayer());
        }
        else
        {
            DisConnectBackground.SetActive(false);
            DisConnectIcon.gameObject.SetActive(false);
            CurStateText.gameObject.SetActive(false);
            CurStateText.text = "";

            PhotonNetwork.CurrentRoom.IsOpen = false;

            Managers.GetComponent<GameManager>().enabled = true;
            Managers.GetComponent<OrderManager>().enabled = true;

            PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity);
        }
        yield return new WaitForSecondsRealtime(0.7f);
    }

    public IEnumerator ConnectTry()
    {
        print("������...");

        PhotonNetwork.ConnectUsingSettings();

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            CurStateText.text = "���ͳ� ���� ��...";
                yield return new WaitForSecondsRealtime(0.7f);
                StartCoroutine(ConnectTry());
        }
        else
        {
            print("�����");
            DisConnectBackground.SetActive(false);
            DisConnectIcon.gameObject.SetActive(false);
            CurStateText.gameObject.SetActive(false);
            DisconnectCorutineDelay = false;
            CurStateText.text = "";
        }
    }

    void ConnectPopupSetting()
    {
        DisConnectBackground.SetActive(true);
        DisConnectIcon.gameObject.SetActive(true);
        CurStateText.gameObject.SetActive(true);
    }
}
