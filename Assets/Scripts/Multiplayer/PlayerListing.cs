using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.UIElements;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    Transform m_PlayerListTransform;

    [SerializeField]
    ScrollView m_PlayerListSV;

    [SerializeField]
    GameObject m_PlayerNamePrefab;

    private Dictionary<string, GameObject> m_playerList = new Dictionary<string, GameObject>();

    /* Adding of new room to displayed list
     * Parameters:
     *      -_rmInfo: the new room to be added
     */
    public GameObject AddPlayer(Player _playerInfo)
    {
        //instantiate prefab
        GameObject player = Instantiate(m_PlayerNamePrefab, m_PlayerListTransform);
        //set TMP to show player name
        player.GetComponent<TextMeshProUGUI>().text = _playerInfo.NickName;
        //add to list
        m_playerList.Add(_playerInfo.NickName, player);
        return player;
    }
    /* Removing room from displayed list when removed from photon list
     * Parameters:
     *      -_rmInfo: the room being removed
     */
    public void RemovePlayer(Player _playerInfo)
    {
        if (!m_playerList.ContainsKey(_playerInfo.NickName)) 
            return;
        //Destroy the button gameobject
        Destroy(m_playerList[_playerInfo.NickName]);
        //remove from list
        m_playerList.Remove(_playerInfo.NickName);
    }

    /* For clearing of list of displayed rooms
     */
    public IEnumerator ClearAllChildObjects()
    {
        //loop destroy first child until no more child objects
        while(m_PlayerListTransform.childCount > 0)
        {
            Destroy(m_PlayerListTransform.GetChild(0));
            yield return new WaitForEndOfFrame();
        }
        //clear dictionary
        m_playerList.Clear();
    }
    public void ClearAll()
    {
        m_PlayerListSV.Clear();
        m_playerList.Clear();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
