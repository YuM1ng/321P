using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PHotonLaunch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    Transform m_PunLogMsgContentTransform;
    [SerializeField]
    GameObject m_PunLogMsgPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
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
