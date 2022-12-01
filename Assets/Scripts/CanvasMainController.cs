using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasMainController : MonoBehaviour
{
    private Animator m_mainAnimCont;

    [SerializeField]
    [Tooltip("Sort button from store page")]
    private Animator m_storeSortAnimCont;
    private bool m_sortIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        m_mainAnimCont = GetComponent<Animator>();

        Debug.Assert(m_storeSortAnimCont != null);
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
    public void SortButtonTrigger()
    {
        if(!m_sortIsOpen)
        {
            m_storeSortAnimCont.SetTrigger("SortOpen");
            m_sortIsOpen = true;
        }
        else
        {
            m_storeSortAnimCont.SetTrigger("SortClose");
            m_sortIsOpen = false;
        }
    }
}
