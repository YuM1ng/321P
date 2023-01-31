using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField]
    SaveSceneSystem.ObjectType m_type;


    public void OnPlace()
    {
        SaveSceneSystem.Instance.AddToTracking(m_type, this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
