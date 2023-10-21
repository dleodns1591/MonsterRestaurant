using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingStatus : MonoBehaviourPunCallbacks, IPunObservable
{
    public NetWorkManager test;

    public int a;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(test.count);
        }
        else
        {
            int rivalCount = (int)stream.ReceiveNext();
            for (int i = 0; i < rivalCount; i++)
            {
                test.Player2.transform.GetChild(i).gameObject.SetActive(true);
            }
            if(rivalCount == 5)
            {
                GameManager.Instance.ReturnOrder();
            }
        }
    }

    void Start()
    {
        test = GameObject.Find("ConnetCanvas").GetComponent<NetWorkManager>();
        gameObject.transform.parent = GameObject.Find("ConnetCanvas").transform;
        gameObject.transform.localScale = Vector3.one;

        if (photonView.IsMine)
        {
            test.Player1 = this.gameObject;
            test.Player1.transform.localPosition = new Vector3(-609, 470);
        }
        else
        {
            test.Player2 = this.gameObject;
            test.Player2.transform.localPosition = new Vector3(52, 470);
        }

        if (photonView.IsMine)
        {
            test.CountPlus = () =>
            {
                test.Player1.transform.GetChild(test.count).gameObject.SetActive(true);
                test.count++;
            };
        }
    }
}
