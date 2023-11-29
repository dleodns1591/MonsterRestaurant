using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingStatus : MonoBehaviourPunCallbacks, IPunObservable
{
    public NetWorkManager NetWorkManager;

    const float Player1PosX = -609, Player1PosY = 470;
    const float Player2PosX = 52, Player2PosY = 470;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(NetWorkManager.count);
        }
        else if(stream.IsReading)
        {
            int rivalCount = (int)stream.ReceiveNext();
            for (int i = 0; i < rivalCount; i++) NetWorkManager.Player2.transform.GetChild(i).gameObject.SetActive(true);
            if(rivalCount == 5) GameManager.Instance.ReturnOrder();
        }
    }

    void Start()
    {
        NetWorkManager = GameObject.Find("ConnetCanvas").GetComponent<NetWorkManager>();
        gameObject.transform.parent = GameObject.Find("ConnetCanvas").transform;
        gameObject.transform.localScale = Vector3.one;

        if (photonView.IsMine)
        {
            NetWorkManager.Player1 = this.gameObject;
            NetWorkManager.Player1.transform.localPosition = new Vector3(Player1PosX, Player1PosY);
        }
        else
        {
            NetWorkManager.Player2 = this.gameObject;
            NetWorkManager.Player2.transform.localPosition = new Vector3(Player2PosX, Player2PosY);
        }

        if (photonView.IsMine)
        {
            NetWorkManager.CountPlus = () =>
            {
                NetWorkManager.Player1.transform.GetChild(NetWorkManager.count).gameObject.SetActive(true);
                NetWorkManager.count++;
            };
        }
    }
}
