using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.LocalPlayer.NickName = "asd";
    }
}
