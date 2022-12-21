using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLaunch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    PunLogging m_PunLogger;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        m_PunLogger.AddLogMsg("Connecting to Photon");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        m_PunLogger.AddLogMsg("Connected to Photon");
        m_PunLogger.AddLogMsg("Joining lobby");
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        m_PunLogger.AddLogMsg($"Disconnected from server:\n{cause}");
    }
    public override void OnJoinedLobby()
    {
        m_PunLogger.AddLogMsg($"Joined lobby: {PhotonNetwork.CurrentLobby}");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
