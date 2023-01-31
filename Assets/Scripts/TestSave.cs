using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _go;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in _go)
        {
            go.GetComponent<SceneObject>().OnPlace();
        }
    }

    public void SaveBtn()
    {
        SaveSceneSystem.Instance.SaveScene("NewScene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
