using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Vuforia;

public class SaveSceneSystem : MonoBehaviour
{
    [System.Serializable]
    struct DataElement
    {
        float[] _position;
        float[] _scale;
        float[] _rotation;

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
    [System.Serializable]
    struct DataFile
    {
        List<DataElement>[] m_ObjectList;
        public DataFile(List<GameObject>[] _dataList)
        {
            m_ObjectList = new List<DataElement>[_dataList.Length];
            for(int i=0; i<_dataList.Length; ++i)
            {
                foreach (GameObject go in _dataList[i])
                {
                    m_ObjectList[i].Add(new DataElement(go.transform));
                }
            }

        }
    }

    List<GameObject>[] m_trackedObjects;

    public void AddToTracking(string _objName, GameObject _go)
    {
        if (_objName == "Wine")
        {
            m_trackedObjects[0].Add(_go);
        }
        else if (_objName == "Flower")
        {
            m_trackedObjects[1].Add(_go);
        }
    }

    public void SaveScene(string _fileName)
    {
        DataFile newSave = new DataFile(m_trackedObjects);
        string rawJson = JsonUtility.ToJson(newSave);
        string filePath = Application.persistentDataPath + "/" + _fileName + ".json";
        File.WriteAllText(filePath, rawJson);
    }
    public void LoadScene(string _fileName)
    {
        string filePath = Application.persistentDataPath + "/" + _fileName + ".json";
        string rawJson = File.ReadAllText(filePath);
        DataFile loadedData = JsonUtility.FromJson<DataFile>(rawJson);

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
