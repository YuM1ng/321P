using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomisationManager : DefaultObserverEventHandler
{
    [SerializeField]
    GameObject m_BtnPlace;
    [SerializeField]
    GameObject m_prefabScene;

    ImageTargetBehaviour ImgTarget;
    string dataSetPath = Application.streamingAssetsPath + "/Vuforia/FYP321.xml";
    string targetName = "TissuePaper";
    bool m_bInit;

    // Start is called before the first frame update
    protected override void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += VufInitialised;
        m_BtnPlace.SetActive(false);
        m_bInit = false;
        Debug.Log("CustMan Started");
    }

    private void VufInitialised(VuforiaInitError err)
    {
        Debug.Log($"VufInit: {err}");
        if(err == VuforiaInitError.NONE)
        {
            Debug.Log($"Texture img: {dataSetPath}");
            ImgTarget =  VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(dataSetPath, targetName);
            ImgTarget.OnTargetStatusChanged += TargetStatusChanged;
        }
    }
    private void TargetStatusChanged(ObserverBehaviour _behaviour, TargetStatus _status)
    {
        Debug.Log($"Img status: {_status.Status}");
        /*switch (_status.Status)
        {
            case Status.NO_POSE:
                break;
            case Status.TRACKED: 
                break;
            case Status.LIMITED: 
                break;
            case Status.EXTENDED_TRACKED: 
                break;
        }*/
        /*if(_status.Status != Status.NO_POSE)
        {
            m_BtnPlace.SetActive(true);
        }
        else
        {
            m_BtnPlace.SetActive(false);
        }*/
    }

    protected override void OnTrackingFound()
    {
        //base.OnTrackingFound();
        Debug.Log("Img found");
        if(!m_bInit)
        {
            GameObject spwn = Instantiate(m_prefabScene);
            spwn.transform.parent = ImgTarget.transform;
            spwn.SetActive(true);
            m_bInit= true;
        }
    }
    protected override void OnTrackingLost()
    {
        Debug.Log("Img lost");
        base.OnTrackingLost();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
