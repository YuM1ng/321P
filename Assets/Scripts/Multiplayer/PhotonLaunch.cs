using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonLaunch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    PunLogging m_PunLogger;

    [SerializeField]
    RoomListing m_RoomListing;
    [SerializeField]
    GameObject m_RoomPanel;
    [SerializeField]
    TextMeshProUGUI m_TMPRoomName;

    [SerializeField]
    PlayerListing m_PlayerListing;
    [SerializeField]
    GameObject m_PlayerPanel;
    [SerializeField]
    GameObject m_PlayerPrefabModel;

    [SerializeField]
    TextMeshProUGUI m_TMPPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        m_PunLogger.AddLogMsg("Connecting to Photon");
        PhotonNetwork.ConnectUsingSettings();
    }
    #region Connecting to photon
    public override void OnConnectedToMaster()
    {
        m_PunLogger.AddLogMsg("Connected to Photon");
        m_PunLogger.AddLogMsg("Joining lobby");
        //Debug.Log($"{System.DateTime.Now.Millisecond}{System.DateTime.Now.Millisecond}");
        if(PhotonNetwork.NickName == ""){
            /*System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int val = (int)(System.DateTime.UtcNow - dt).TotalMilliseconds;*/
            int val = (int)System.DateTime.Now.Ticks;
            PhotonNetwork.NickName = "Player " + val.ToString();
            m_PunLogger.AddLogMsg($"Player name set: {PhotonNetwork.NickName}");
            m_TMPPlayerName.text = PhotonNetwork.NickName;
        }
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        m_PunLogger.AddLogMsg($"Disconnected from server: {cause}");
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion
    
    #region Lobby related events
    public override void OnJoinedLobby()
    {
        m_PunLogger.AddLogMsg($"Joined lobby: {PhotonNetwork.CurrentLobby}");
        m_PlayerPanel.SetActive(false);
        m_RoomPanel.SetActive(true);
    }
    #endregion

    #region Room related events
    public override void OnCreatedRoom()
    {
        m_PunLogger.AddLogMsg($"Room successfully created");
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        m_PunLogger.AddLogMsg($"Room creation failed: {returnCode}-'{message}'");
    }
    public override void OnJoinedRoom()
    {
        m_PunLogger.AddLogMsg($"Joined room: {PhotonNetwork.CurrentRoom.Name}");
        //StartCoroutine(m_RoomListing.ClearAllChildObjects());
        //m_RoomListing.ClearAll();

        //initialise players in the room
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            m_PlayerListing.AddPlayer(player);
            if (player == PhotonNetwork.LocalPlayer)
            {
                //m_PunLogger.AddLogMsg($"It's a me {player.NickName}!");

            }
        }
        m_TMPRoomName.text = PhotonNetwork.CurrentRoom.Name;
        m_RoomPanel.SetActive(false);
        m_PlayerPanel.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        m_PunLogger.AddLogMsg($"Left Room ()");
        //StartCoroutine(m_PlayerListing.ClearAllChildObjects());
        m_PlayerListing.ClearAll();
        m_RoomPanel.SetActive(true);
        m_PlayerPanel.SetActive(false);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
            //checked if room is removed from list
            if(room.RemovedFromList)
            {
                m_RoomListing.RemoveRoom(room);
            }
            //At this point, room is added to list
            else
            {
                m_PunLogger.AddLogMsg($"Adding Room: {room}");
                GameObject rm = m_RoomListing.AddRoom(room);
                if (rm != null)
                {
                    rm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
                    rm.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        m_PunLogger.AddLogMsg($"Joining room '{room.Name}'");
                        PhotonNetwork.JoinRoom(room.Name);
                    });
                }
                else
                {
                    m_PunLogger.AddLogMsg($"Duplicate room({room.Name}) detected");
                }
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        m_PunLogger.AddLogMsg($"New player ({newPlayer.NickName}) entered the room");
        m_PlayerListing.AddPlayer(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        m_PunLogger.AddLogMsg($"Player ({otherPlayer.NickName}) left the room");
        m_PlayerListing.RemovePlayer(otherPlayer);

    }
    #endregion

    public void CreateJoinRoom(TMP_InputField _ipF)
    {
        m_PunLogger.AddLogMsg($"Creating and joining room '{_ipF.textComponent.text}'");
        PhotonNetwork.CreateRoom(_ipF.textComponent.text);
        //StartCoroutine(m_RoomListing.ClearAllChildObjects());
    }
    public void LeaveRoom()
    {
        m_PunLogger.AddLogMsg($"Leaving room '{PhotonNetwork.CurrentRoom.Name}'");
        PhotonNetwork.LeaveRoom();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
