using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainController : MonoBehaviour
{
    private Animator m_mainAnimCont;
    // Start is called before the first frame update
    void Start()
    {
        m_mainAnimCont = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateToCreateAR()
    {
        m_mainAnimCont.SetTrigger("createAROpen");
        m_mainAnimCont.SetTrigger("createClose");
    }
}
