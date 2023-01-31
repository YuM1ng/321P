using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vuforia;

[CreateAssetMenu(menuName ="Singleton/SaveLoadSystem")]
public class SaveSceneSystem : MonoBehaviourSingleton<SaveSceneSystem>
{
    public enum ObjectType
    {
        Wine,
        Flower,
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
    public void SaveScene(string _fileName)
    {
        //Creates a new DataFile variable based on the objects list in m_trackedObjects
        DataFile newSave = new DataFile(m_trackedObjects);
        //Converts it to Json string
        string rawJson = JsonUtility.ToJson(newSave,true);
        
        /*//save to local path
        string filePath = Application.persistentDataPath + "/" + _fileName + ".json";
        File.WriteAllText(filePath, rawJson);*/
    }
    public void LoadScene(string _fileName)
    {
        string filePath = Application.persistentDataPath + "/" + _fileName + ".json";
        string rawJson = File.ReadAllText(filePath);
        DataFile loadedData = JsonUtility.FromJson<DataFile>(rawJson);

    }
    IEnumerator UploadToServer(string _custJson)
    {
        WWWForm form = new WWWForm();
        /*form.AddField("userId", userId);
        form.AddField("name", name);
        form.AddField("image", image);
        form.AddField("greetingCardId", greetingCardId);
        form.AddField("textmessage", "");*/
        form.AddField("options", _custJson);
    }
    // Start is called before the first frame update
    void Start()
    {
        /*VuforiaBehaviour.Instance.*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
