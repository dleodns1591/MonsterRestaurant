using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestObj : MonoBehaviourPunCallbacks, IPunObservable
{
    public Test test;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(test.count);
        }
        else
        {
                int asd = (int)stream.ReceiveNext();
                for (int i = 0; i < asd; i++)
                {
                    test.Player2.transform.GetChild(i).gameObject.SetActive(true);
                }
        }
    }

    void Start()
    {
        test = GameObject.Find("OnlineBattleCanvas").GetComponent<Test>();
        gameObject.transform.parent = GameObject.Find("OnlineBattleCanvas").transform;
        gameObject.transform.localScale = Vector3.one;

        if (photonView.IsMine)
        {
            test.Player1 = this.gameObject;
            test.Player1.transform.localPosition = new Vector3(-609, 470);
        }
        else
        {
            test.Player2 = this.gameObject;
        }

        GameObject.Find("He").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (photonView.IsMine)
            {
                test.Player1.transform.GetChild(test.count).gameObject.SetActive(true);
                test.count++;
            }
        });
    }
}
