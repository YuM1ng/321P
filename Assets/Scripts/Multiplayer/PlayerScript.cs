using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    TextMeshPro m_PlayerNameTMP;



    public void SetName(string _newName)
    {
        m_PlayerNameTMP.text = _newName;
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
