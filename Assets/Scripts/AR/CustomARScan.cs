using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class CustomARScan : MonoBehaviour
{
    [Header("Pre scanning UI elements")]
    [SerializeField]
    [Tooltip("UI panel for requesting customization id")]
    GameObject m_PreScanPanel;
    [SerializeField]
    [Tooltip("The inputfield where user specify the customization ID")]
    TMP_InputField m_CustomizationIDInput;
    [SerializeField]
    TextMeshProUGUI m_ResponseText;
    [SerializeField]
    GameObject m_ResultPanel;

    [Header("UI when scanning AR ")]
    [SerializeField]
    [Tooltip("UI panel for during scanning of AR")]
    GameObject m_ScanningPanel;

    private ImageTargetBehaviour m_imgTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSearchCustomization()
    {
        m_ResponseText.gameObject.SetActive(true);
        m_ResponseText.text = "Searching for customization...";
        StartCoroutine(GetCustomizations());

    }
    IEnumerator GetCustomizations()
    {
        WWWForm getCustForm = new WWWForm();
        /***************************
         * update url
         ***************************/
        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/", getCustForm);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            m_ResponseText.text = "Customization found!";
            /***************************************
             * set cuztomisation values accordingly
             ***************************************/
            byte[] imgTarget /*= Convert.FromBase64String()*/;
            Texture2D tmpTexture = new Texture2D(2, 2);
            //tmpTexture.LoadImage(imgTarget);
            m_imgTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(tmpTexture, 0.115f, "CustomTarget");

        }
        else
        {
            m_ResponseText.text = "Couldn't find customization";
        }
    }
}
