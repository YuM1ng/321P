using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject m_prefabPlayer;

    [SerializeField]
    GameObject m_HostStage;

    [SerializeField]
    GameObject[] m_ParticipantsStage;

    public void AddAsHost()
    {
        GameObject go = /*PhotonNetwork.*/Instantiate(m_prefabPlayer);
    }

    public void AddAsParticipant()
    {

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
