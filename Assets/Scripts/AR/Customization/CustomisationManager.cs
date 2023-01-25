using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomisationManager : DefaultObserverEventHandler
{
    [SerializeField]
    GameObject m_BtnPlace;
    [SerializeField]
    GameObject m_prefabScene;
    [SerializeField]
    RawImage DispImg;

    ImageTargetBehaviour ImgTarget;
    GameObject m_goPlot;
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
        if(err == VuforiaInitError.NONE)
        {
            NativeGallery.GetImageFromGallery(ProcessIMG, "Select image to be tracked");
            /*ImgTarget =  VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(dataSetPath, targetName);
            ImgTarget.OnTargetStatusChanged += TargetStatusChanged;*/
        }
    }
    private void ProcessIMG(string _path)
    {

        byte[] imgArray = File.ReadAllBytes(_path);
        Texture2D texture = new Texture2D(2,2);
        if (texture.LoadImage(imgArray))
        {
            DispImg.transform.localScale = new Vector3(texture.width/texture.height, 1, 0);
            DispImg.texture = texture;

            ImgTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(texture, 0.115f, "TissuePaper");
            ImgTarget.OnTargetStatusChanged += TargetStatusChanged;
            DefaultObserverEventHandler deoh = ImgTarget.gameObject.AddComponent<DefaultObserverEventHandler>();

            m_goPlot = Instantiate(m_prefabScene, ImgTarget.transform);
            //spwn.transform.SetParent(ImgTarget.transform);
            //spwn.transform.parent = ImgTarget.transform;
            m_goPlot.transform.localPosition = Vector3.zero;
            m_bInit= true;

            /*DefaultObserverEventHandler doeh = ImgTarget.GetComponent<DefaultObserverEventHandler>();
            doeh.OnTargetFound.AddListener(TrackingFound);
            doeh.OnTargetLost.AddListener(TrackingLost);*/
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
        if (_status.Status != Status.NO_POSE)
        {
            m_BtnPlace.SetActive(true);
            m_goPlot.SetActive(true);
        }
        else
        {
            m_BtnPlace.SetActive(false);
            m_goPlot.SetActive(false);
        }
    }

    /*protected override */void TrackingFound()
    {
        //base.OnTrackingFound();
        Debug.Log("Img found");
        /*if(!m_bInit)
        {
            GameObject spwn = Instantiate(m_prefabScene);
            Debug.Log($"ImgTarget name: {ImgTarget.transform.name}, GO parent: {ImgTarget.gameObject.name}");
            spwn.transform.parent = ImgTarget.transform;
            spwn.transform.localPosition = Vector3.zero;
            spwn.SetActive(true);
            m_bInit= true;
        }*/
    }
    /*protected override */void TrackingLost()
    {
        Debug.Log("Img lost");
        //base.OnTrackingLost();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
