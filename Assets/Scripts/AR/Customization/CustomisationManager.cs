using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomisationManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_BtnPlace;

    string dataSetPath = "Vuforia/FYP321.xml";
    string targetName = "TissuePaper";

    // Start is called before the first frame update
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += VufInitialised;
        m_BtnPlace.SetActive(false);
    }

    private void VufInitialised(VuforiaInitError err)
    {
        if(err == VuforiaInitError.NONE)
        {
            ImageTargetBehaviour ImgTarget =  VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(dataSetPath, targetName);
            ImgTarget.OnTargetStatusChanged += TargetStatusChanged;
        }
    }
    private void TargetStatusChanged(ObserverBehaviour _behaviour, TargetStatus _status)
    {
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
        if(_status.Status != Status.NO_POSE)
        {
            m_BtnPlace.SetActive(true);
        }
        else
        {
            m_BtnPlace.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
