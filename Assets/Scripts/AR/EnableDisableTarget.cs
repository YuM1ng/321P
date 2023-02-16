using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnableDisableTarget : MonoBehaviour
{
    [SerializeField]
    GameObject m_GOTarget;
    [SerializeField]
    private RawImage m_rawImg;
    [SerializeField]
    VariantToggle m_VariantToggle;

    private bool m_isEnabled = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!m_isEnabled)
        {
            m_rawImg.color = new Color(1f, 1f, 1f, 0.5f);
            if(m_VariantToggle!= null )
                m_VariantToggle.gameObject.SetActive(false);
            //else
                m_GOTarget.SetActive(false);
        }
        EventTrigger uiImgTap = transform.AddComponent<EventTrigger>();
        EventTrigger.Entry tapEvent = new EventTrigger.Entry();
        tapEvent.eventID = EventTriggerType.PointerClick;
        tapEvent.callback.AddListener((data) =>
        {
            transform.parent.GetComponent<MultiTargetManager>().EnableThisTarget(this);
        });
        uiImgTap.triggers.Add(tapEvent);

    }

    public void TargetEnable() 
    {
        m_isEnabled = true;
        if (m_VariantToggle != null)
            m_VariantToggle.gameObject.SetActive(true);
        //else
            m_GOTarget.SetActive(true);
        m_rawImg.color = new Color(1f, 1f, 1f, 1f);
    }
    public void TargetDisable()
    {
        m_isEnabled = false;
        if (m_VariantToggle != null)
            m_VariantToggle.gameObject.SetActive(false);
        //else
            m_GOTarget.SetActive(false);
        m_rawImg.color = new Color(1f, 1f, 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
