using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public int count;

    public GameObject Player1, Player2;

    [SerializeField] private GameObject DisConnectBackground;
    [SerializeField] private Image DisConnectIcon;
    [SerializeField] private TextMeshProUGUI CurStateText;
    [SerializeField] private GameObject Managers;

    [SerializeField] private Sprite[] DisConnectIconSpr;

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
        }
        else
        {
            Application.Quit();
        }
    }
    public override void OnJoinedLobby()
    {
        print("�κ�");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        if (PhotonNetwork.CountOfRooms == 0) PhotonNetwork.CreateRoom("1��1�߽Ǻ�", roomOptions, null);
        else PhotonNetwork.JoinRandomRoom();

    }

    public override void OnJoinedRoom()
    {
        print("�� ����!");
        StartCoroutine(FindPlayer(0));

        //PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity);
    }
    public IEnumerator FindPlayer(int IconSpr)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            DisConnectIcon.sprite = DisConnectIconSpr[IconSpr];
            CurStateText.text = "��븦 ã�� �ֽ��ϴ�...";
            if (IconSpr + 1 == DisConnectIconSpr.Length)
            {
                yield return new WaitForSecondsRealtime(0.7f);
                StartCoroutine(FindPlayer(0));
            }
            else
            {
                yield return new WaitForSecondsRealtime(0.7f);
                StartCoroutine(FindPlayer(IconSpr + 1));
            }
        }
        else
        {
            DisConnectBackground.SetActive(false);
            DisConnectIcon.gameObject.SetActive(false);
            CurStateText.gameObject.SetActive(false);

            Managers.GetComponent<GameManager>().enabled = true;
            Managers.GetComponent<OrderManager>().enabled = true;
        }
        yield return new WaitForSecondsRealtime(0.7f);
    }

    public IEnumerator ConnectTry(int IconSpr)
    {
        print("������...");

        PhotonNetwork.ConnectUsingSettings();

        yield return new WaitForSecondsRealtime(0.7f);


        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            DisConnectIcon.sprite = DisConnectIconSpr[IconSpr];
            CurStateText.text = "���ͳ� ���� ��...";
            if (IconSpr + 1 == DisConnectIconSpr.Length)
                StartCoroutine(ConnectTry(0));
            else
                StartCoroutine(ConnectTry(IconSpr + 1));
        }
        else
        {
            print("�����");
            Time.timeScale = 1;
            Managers.GetComponent<GameManager>().enabled = true;
            Managers.GetComponent<OrderManager>().enabled = true;
            DisConnectBackground.SetActive(false);
            DisConnectIcon.gameObject.SetActive(false);
            CurStateText.gameObject.SetActive(false);

            PhotonNetwork.JoinLobby();
        }
    }

    public void Click()
    {
        PhotonNetwork.JoinLobby();

        DisConnectBackground.SetActive(true);
        DisConnectIcon.gameObject.SetActive(true);
        CurStateText.gameObject.SetActive(true);
    }
}