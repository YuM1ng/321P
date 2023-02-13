using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class SaveCustomisation : MonoBehaviour
{
    [SerializeField]
    GameObject m_SavingPanel;
    [SerializeField] 
    TMP_Text m_NameOfCust;
    [SerializeField]
    TMP_Dropdown m_ImgTargetDropdown;
    [SerializeField]
    Button m_SaveBtn;
    [SerializeField]
    TMP_Text m_responseTxt;
    [SerializeField]
    Button m_BkToMenuBtn;

    private SaveSceneSystem _saveSceneSys;
    private Vector3 m_objOffset;
    // Start is called before the first frame update
    void Start()
    {
        m_SavingPanel.SetActive(false);
        _saveSceneSys = FindObjectOfType<SaveSceneSystem>();
        _saveSceneSys.ClearTrackings();
        m_SaveBtn.interactable = false;
        m_SaveBtn.onClick.AddListener(SaveScene);
    }

    public void SaveBtnClick()
    {
        m_SavingPanel.SetActive(true);
        m_objOffset = FindObjectOfType<XROrigin>().Camera.transform.localPosition;
    }

    public void CancelBtnClick()
    {
        m_SavingPanel.SetActive(false);
    }
    public void SaveScene()
    {
        /*XROrigin orig = GameObject.FindObjectOfType<XROrigin>();
        Debug.Log("XR orig:" + orig.Origin.transform.position + "|Camera Offset:"+orig.CameraFloorOffsetObject.transform.position + "|Camera Pos:" + orig.Camera.transform.localPosition+ ", " + orig.Camera.transform.rotation.eulerAngles);*/
        _saveSceneSys.SaveScene(m_NameOfCust.text, m_ImgTargetDropdown.options[m_ImgTargetDropdown.value].image.texture, "My customisation", m_responseTxt.text, m_objOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_NameOfCust.text != "")
        {
            m_SaveBtn.interactable= true;
        }
    }
}
