using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour
{
    [SerializeField]
    SaveSceneSystem.ObjectType m_type;

    SaveSceneSystem _ssSystem;
    public void OnPlace()
    {
        _ssSystem.AddToTracking(m_type, this.gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        _ssSystem = GameObject.Find("SaveSystem").GetComponent<SaveSceneSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
