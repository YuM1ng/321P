using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class ScanMyARManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The display text to show loading progress")]
    TextMeshProUGUI m_displayText;

    [Header("Ui for before scanning")]
    [SerializeField]
    [Tooltip("UI panel for requesting customization id")]
    GameObject m_goBeforeScan;
    [SerializeField]
    [Tooltip("Results object to display the retrieved data")]
    GameObject m_ResultsObject;
    [SerializeField]
    [Tooltip("content transform to parent the results info")]
    Transform m_contentTrans;


    [Header("Ui for during scanning")]
    [SerializeField]
    [Tooltip("UI panel for during scanning of AR")]
    GameObject m_goDuringScan;
    [SerializeField]
    [Tooltip("Image target reference when scanning")]
    RawImage m_duringScanImgPrompt;

    private string m_userID;
    // Start is called before the first frame update
    void Start()
    {
        m_goBeforeScan.SetActive(true);
        m_goDuringScan.SetActive(false);
        m_displayText.text = "Loading all Customization...";
        User curruser = GameObject.Find("UserManager").GetComponent<User>();
        if (curruser != null)
        {
            StartCoroutine(GetUserID(curruser.session_id));
        }
        //StartCoroutine(GetMyCustomizations(m_userID));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Coroutine to get current user's id using User's current sessionID
     *  Parameters:
     *      -_sess: the session ID of current user
     */
    private IEnumerator GetUserID(string _sess)
    {
        //Creates form with sesionId data
        WWWForm form = new WWWForm();
        form.AddField("session_id", _sess);

        //post request to retrieve id
        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/fetchUserProfilebyId", form);
        yield return www.SendWebRequest();
        //if request succeeds
        if (www.result == UnityWebRequest.Result.Success)
        {
            //converts response json
            var userProfileResponses = JsonUtility.FromJson<UserProfileResponseList>("{\"users\":" + www.downloadHandler.text + "}");
            //if there are data
            if (userProfileResponses.users.Count > 0)
            {
                //gets first user's ID
                //m_userID = userProfileResponses.users[0].user_id;
                StartCoroutine(GetMyCustomizations(userProfileResponses.users[0].user_id));
            }
        }
    }

    IEnumerator GetMyCustomizations(string _userid)
    {
        //Creates form with user id
        WWWForm getCustForm = new WWWForm();
        getCustForm.AddField("userId", _userid);

        //request server for customization
        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/getCustomizationbyUserId", getCustForm);
        yield return www.SendWebRequest();
        //if request succeeds
        if (www.result == UnityWebRequest.Result.Success)
        {
            //converts response json to CustomisationResponseList data type
            var resList = JsonUtility.FromJson<CustomisationResponseList>("{\"custList\":" + www.downloadHandler.text + "}");
            //if there are customization(s) found
            if (resList.custList.Count > 0)
            {
                //iterate through each one to instantiate them for user to see
                foreach (CustomisationResponse cust in resList.custList)
                {
                    //instatntiate the gameobject
                    GameObject go = Instantiate(m_ResultsObject, m_contentTrans);
                    //make sure object is active in scene
                    go.SetActive(true);
                    //get the ResultEntry script from it
                    ResultEntry reGO = go.GetComponent<ResultEntry>();
                    //set name of customised scene
                    reGO.SetNameOfCust(cust.name);
                    //Set display Id with retrieved value
                    reGO.SetID("ID:" + cust.customization_id);
                    /*************************************************************
                     * Due to current server data, one entry has different format, resulting in it not being able to split into 2 or more string
                     *************************************************************/
                    //split image string at each commas
                    string[] imgStrArr = cust.image.Split(",");
                    //variable for taking the actual base64 image string
                    string imgStr;
                    if (imgStrArr.Length > 1)
                        imgStr = imgStrArr[1];
                    else
                        imgStr = imgStrArr[0];
                    //converts base64 string to byte array
                    byte[] imgbyte = Convert.FromBase64String(imgStr);
                    //creates a texture for loading image byte into
                    Texture2D imgTexture = new Texture2D(2, 2);
                    if (imgTexture.LoadImage(imgbyte))
                    {
                        //if successfullt loaded, set this display image to this image
                        reGO.SetImage(imgTexture);
                    }
                    //Add custom on-click event to button that passes in the customisation json string and Texture2D created above
                    reGO.GetProceedButton().onClick.AddListener(()=> { 
                        CreateARScan(cust.options, imgTexture); 
                    });
                }
                //set display text for 
                m_displayText.text = "Loading done!";
            }
            else
            {
                //if no customisations found set display text to let ueser know
                m_displayText.text = "No customizatioon's found";
            }
        }
        else
        {
            //in the event of error show error text
            m_displayText.text = "Error encountered :(";
            Debug.Log($"err:{www.error}");
        }
    }
    /* Function that sets up AR image scanning in the scene, to be called by "Proceed to Scan" button
     *  Parameters:
     *      -_rawCust: the customization json string
     *      -_img: the Texture2D of the img to create an image target from
     */
    private void CreateARScan(string _rawCust, Texture2D _img)
    {
        //change ui display
        m_goBeforeScan.SetActive(false);
        m_goDuringScan.SetActive(true);
        //assign texture to display prompt
        m_duringScanImgPrompt.texture = _img;
        ImageTargetBehaviour imgTar = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(_img, 0.148f, "ImgTarget");
        imgTar.gameObject.AddComponent<DefaultObserverEventHandler>();
        //Loads customization json string onto image target transform
        SaveSceneSystem.LoadCustJsonToTransform(_rawCust, imgTar.transform);
    }
}
