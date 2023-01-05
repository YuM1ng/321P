using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    public struct PosRotScale
    {
        public float[] m_position;
        public float[] m_rotation;
        public float[] m_scale;
        public PosRotScale(float[] position, 
            float[] rotation, 
            float[] scale)
        {
            m_position = position;
            m_rotation = rotation;
            m_scale = scale;
        }
        public PosRotScale(float _posX, float _posY, float _posZ,
            float _rotX, float _rotY, float _rotZ,
            float _scaleX, float _scaleY, float _scaleZ)
        {
            m_position = new float[3] { _posX, _posY,_posZ};
            m_rotation = new float[3] { _rotX, _rotY, _rotZ};
            m_scale = new float[3] { _scaleX, _scaleY, _scaleZ};
        }
    }
    public struct SaveData
    {
        /* Count of each unique placeable object
         * e.g. 3 object user can use for customization, array will consist [3,1,5]
         */
        public int[] m_CountOfEachObject;
        /* A list of position, rotation and scale value of each object counted in m_CountOfEachObject
         * e.g. (based on above e.g.) the list size will be 9
         */
        public List<PosRotScale> m_ListOfPosRotScale;
    }
    public static void Save(string _fileName, SaveData _data)
    {
        Debug.Log("Save starting|||");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + _fileName + ".gar";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, _data); 
        stream.Close();
        Debug.Log($"{_fileName} saved");
    }
    public static SaveData/*void */Load(string _fileName/*, SaveData _data*/)
    {
        string path = Application.persistentDataPath + "/" + _fileName + ".gar";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = (SaveData)formatter.Deserialize(stream);

            stream.Close();
            //_data = data;
            return data;
        }
        else
        {
            Debug.Log($"File path [{path}] not found");
            return new SaveData();
        }
    }
}
