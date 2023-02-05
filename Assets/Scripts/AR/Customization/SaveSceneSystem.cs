using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;
using Image = UnityEngine.UI.Image;

//[CreateAssetMenu(menuName ="Singleton/SaveLoadSystem")]
public class SaveSceneSystem : MonoBehaviour //MonoBehaviourSingleton<SaveSceneSystem>
{
    public enum ObjectType
    {
        Wine,
        Flower,
        Lantern1,
        Mooncake,
        TotalType
    }

    /*  struct to hold per object's instance's informations, currently saves local position, local scale, and local quaternion rotation
     *  Variables:
     *      - _position : local position of object
     *      - _scale    : local scale of object
     *      - _rotation : local quaternion rotation of object
     */
    [System.Serializable]
    struct DataElement
    {
        public float[] _position;
        public float[] _scale;
        public float[] _rotation;

        /* Converts important informations into arrays for saving
         */
        public DataElement(Transform _transform)
        {
            _position= new float[] { _transform.localPosition.x, 
                                    _transform.localPosition.y, 
                                    _transform.localPosition.z };

            _scale = new float[] { _transform.localScale.x, 
                                    _transform.localScale.y, 
                                    _transform.localScale.z };

            _rotation = new float[] { _transform.localRotation.x, 
                                        _transform.localRotation.y, 
                                        _transform.localRotation.z, 
                                        _transform.localRotation.w };
        }
        /* For recreating objects based on saved information
         * Parameters:
         *      - theTrans: To set the object's transform, instatiated just before this function call, to the saved info
         */
        public void ToTransform(Transform _theTrans)
        {
            _theTrans.localPosition= new Vector3(_position[0], 
                                                _position[1], 
                                                _position[2]);
            _theTrans.localScale = new Vector3(_scale[0], 
                                                _scale[1], 
                                                _scale[2]);
            _theTrans.rotation= new Quaternion(_rotation[0], 
                                                _rotation[1], 
                                                _rotation[2], 
                                                _rotation[3]);
        }
    }
    /* struct for each unique prefab, to hold their individual instances in the scene
     */
    [System.Serializable]
    struct DataObject
    {
        public List<DataElement> _ObjectInstances;
        /* Converts the list of instances of an object into DataElement and add to list
         * Parameters:
         *      - _goInstances : All the instances of this object
         */
        public DataObject(List<GameObject> _goInstances)
        {
            _ObjectInstances = new List<DataElement>();
            foreach (GameObject go in _goInstances)
            {
                _ObjectInstances.Add(new DataElement(go.transform));
            }
        }
    }
    /* Struct to hold information of each savable object in scene
     * 
     */
    [System.Serializable]
    struct DataFile
    {
        public List<DataObject> m_ObjectList;
        public DataFile(List<GameObject>[] _dataList)
        {
            m_ObjectList = new List<DataObject>();
            for(int i=0; i<_dataList.Length; ++i)
            {
                m_ObjectList.Add(new DataObject(_dataList[i]));
                /*foreach (GameObject go in _dataList[i])
                {
                    m_ObjectList[i].Add(new DataElement(go.transform));
                }*/
            }

        }
    }

    List<GameObject>[] m_trackedObjects;

    private string m_userID;
    private int m_greetCardID;
    //private string m_greetCardImg;
    public RawImage m_disp;

    /* When an object that is savable (has SceneObject.cs script on it) is placed, this function will be called to add it to tracking
     * Parameters:
     *      - _type : the type of object (based on those defined in ObjectType enum)
     *      - _go   : the GameObject reference
     */
    public void AddToTracking(/*string _objName*/ObjectType _type, GameObject _go)
    {
        //if the list is not created yet
        if(m_trackedObjects == null)
        {
            //initialise the list
            m_trackedObjects = new List<GameObject>[(int)ObjectType.TotalType];
            /*m_trackedObjects[0] = new List<GameObject>();
            m_trackedObjects[1] = new List<GameObject>();*/

            //for each objects defined in ObjectType, create a new list for it
            for (int i = 0; i < (int)ObjectType.TotalType; ++i)
            {
                m_trackedObjects[i] = new List<GameObject>();
            }
        }
        //the enum number corresponds to the position in the array of list, so the object is added to that list
        m_trackedObjects[(int)_type].Add(_go);
        /*if (_objName == "Wine")
        {
            m_trackedObjects[0].Add(_go);
        }
        else if (_objName == "Flower")
        {
            m_trackedObjects[1].Add(_go);
        }*/

    }
    /*  Function to be called when the scene is to be saved
     *  Parameters:
     *      - _fileName: name of the scene to be saved
     */
    public void SaveScene(string _fileName, Texture2D _texIMG, string _msg)
    {
        //Creates a new DataFile variable based on the objects list in m_trackedObjects
        DataFile newSave = new DataFile(m_trackedObjects);

        //Converts it to Json string
        string rawJson = JsonUtility.ToJson(newSave, true);

        /*RenderTexture rdTexture = RenderTexture.GetTemporary(_texIMG.width,_texIMG.height,0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(_texIMG,rdTexture);
        RenderTexture prevTex = RenderTexture.active;
        RenderTexture.active = rdTexture;
        Texture2D copyTexture = new Texture2D(_texIMG.width, _texIMG.height);
        copyTexture.ReadPixels(new Rect(0, 0, rdTexture.width, rdTexture.height), 0, 0);
        copyTexture.Apply();
        RenderTexture.active = prevTex;
        RenderTexture.ReleaseTemporary(rdTexture);*/


        byte[] imgByte = DecompressTexture2D(_texIMG).EncodeToJPG();
        //byte[] imgByte = _texIMG.GetRawTextureData();
        /*string filePath = Application.persistentDataPath + "/test.jpg";
        Debug.Log(filePath);
        File.WriteAllBytes(filePath, imgByte);*/

        /*Texture2D tex = new Texture2D(2, 2);
        tex.LoadRawTextureData(imgByte);
        m_disp.texture = tex;*/

        string x64Img = Convert.ToBase64String(imgByte);
        x64Img = "data:image/jpg;base64, " + x64Img;
        Debug.Log($"img: {x64Img}");
        /*string filePath = Application.persistentDataPath + "/test.txt";
        Debug.Log(filePath);
        File.WriteAllText(filePath, x64Img);*/

        WWWForm custForm = new WWWForm();
        custForm.AddField("userId", m_userID);
        custForm.AddField("name", _fileName);
        custForm.AddField("image", x64Img);
        //custForm.AddField("greetingCardId", m_greetCardID);
        custForm.AddField("textMessage", _msg);
        custForm.AddField("options", rawJson);

        StartCoroutine(UploadToServer(custForm));

        /*//save to local path
        string filePath = Application.persistentDataPath + "/" + _fileName + ".json";
        Debug.Log(filePath);
        File.WriteAllText(filePath, rawJson);*/
    }
    public void LoadScene(string _fileName)
    {
        StartCoroutine(GetFromServer());
        /*string filePath = Application.persistentDataPath + "/" + _fileName + ".json";
        string rawJson = File.ReadAllText(filePath);
        DataFile loadedData = JsonUtility.FromJson<DataFile>(rawJson);*/

    }
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        /*VuforiaBehaviour.Instance.*/
        User curruser = GameObject.Find("UserManager").GetComponent<User>();
        if (curruser != null)
        {
            StartCoroutine(GetUserID(curruser.session_id));
        }

        //StartCoroutine(GetGreetCard());
    }

        // Update is called once per frame
        void Update()
    {
        
    }

    private Texture2D DecompressTexture2D(Texture2D _fromTexture)
    {
        RenderTexture rdTexture = RenderTexture.GetTemporary(_fromTexture.width, _fromTexture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        Graphics.Blit(_fromTexture, rdTexture);
        RenderTexture prevTex = RenderTexture.active;
        RenderTexture.active = rdTexture;
        Texture2D copyTexture = new Texture2D(_fromTexture.width, _fromTexture.height);
        copyTexture.ReadPixels(new Rect(0, 0, rdTexture.width, rdTexture.height), 0, 0);
        copyTexture.Apply();
        RenderTexture.active = prevTex;
        RenderTexture.ReleaseTemporary(rdTexture);
        return copyTexture;
    }

    IEnumerator UploadToServer(WWWForm _custForm)
    {
        /*WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("name", name);
        form.AddField("image", image);
        form.AddField("greetingCardId", greetingCardId);
        form.AddField("textmessage", "");
        form.AddField("options", _custJson);*/

        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/insertCustomization/", _custForm);
        yield return www.SendWebRequest();
        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log($"err: {www.error} | desc: {www.downloadHandler.text}");
        }
        else
        {
            Debug.Log("Cust upload done");
        }
    }
    IEnumerator GetFromServer()
    {
        WWWForm custForm = new WWWForm();
        custForm.AddField("userId", m_userID);

        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/getCustomizationbyUserId", custForm);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Get cust: {www.downloadHandler.text}");
        }
        else
        {
            Debug.Log($"err: {www.error} | desc: {www.downloadHandler.text}");
        }

    }

    private IEnumerator GetUserID(string _sess)
    {
        WWWForm form = new WWWForm();
        form.AddField("session_id", _sess);

        UnityWebRequest www = UnityWebRequest.Post("https://lunar-byte-371808.et.r.appspot.com/api/fetchUserProfilebyId", form);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.Success)
        {
            var userProfileResponses = JsonUtility.FromJson<UserProfileResponseList>("{\"users\":" + www.downloadHandler.text + "}");

            if (userProfileResponses.users.Count > 0)
            {
                m_userID = userProfileResponses.users[0].user_id;
            }
        }

    }
    /*private IEnumerator GetGreetCard()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://lunar-byte-371808.et.r.appspot.com/api/getGreetingCards");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("GetProduct Response: " + www.downloadHandler.text);
            var getGreetingCardResponseList = JsonUtility.FromJson<GreetingCardResponseList>(www.downloadHandler.text);
            var greetingCardsResponse = getGreetingCardResponseList.greeting_cards;
            if (greetingCardsResponse.Count > 0)
            {
                m_greetCardID = greetingCardsResponse[0].greeting_card_id;
                m_greetCardImg = greetingCardsResponse[0].image.Split(',')[1];
                Debug.Log($"Img taken: { greetingCardsResponse[0].name} ({m_greetCardID})");
            }
            else
            {
                Debug.Log("get card err");
            }
        }

    }*/
}
