using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IPunInstantiateMagicCallback
{
    [SerializeField]
    TextMeshPro m_PlayerNameTMP;

    [SerializeField]
    GameObject _myCamera;

    GameObject _lastCamera;

    [SerializeField]
    PhotonVoiceView m_PhotonVoiceView;

    [SerializeField]
    GameObject m_speakingIndicator;

    PhotonView m_photonView;
    public PhotonView photonView
    {
        get
        {
            if (m_photonView == null)
                m_photonView = GetComponent<PhotonView>();
            return m_photonView;
        }
    }
    public void SetName(string _newName)
    {
        m_PlayerNameTMP.text = _newName;
    }
    public void CameraOn()
    {
        if(_lastCamera == null)
        {
            _lastCamera = Camera.main.gameObject;
        }
        _lastCamera.SetActive(false);
        _myCamera.SetActive(true);
    }
    public void CameraOff()
    {
        _myCamera.SetActive(false);
        _lastCamera.SetActive(true);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //Debug.Log(info.Sender.NickName);
        SetName(info.Sender.NickName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_PhotonVoiceView.IsSpeaking)
        {
            Debug.Log($"{photonView.Owner.NickName} is speaking");
            m_speakingIndicator.SetActive(true);
        }
        else
        {
            m_speakingIndicator.SetActive(false);
        }
    }
}
