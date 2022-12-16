using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PHotonLaunch : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
