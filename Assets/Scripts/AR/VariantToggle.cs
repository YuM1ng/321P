using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VariantToggle : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown m_VariantDropdown;

    [SerializeField]
    List<GameObject> m_Variants;
    
    public GameObject GetSlectedVariant()
    {
        return m_Variants[m_VariantDropdown.value];
    }
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var variant in m_Variants)
        {
            variant.SetActive(false);
        }
        m_VariantDropdown.onValueChanged.AddListener(ChangeActiveTarget);
    }
    /* When dropdown value change, change active AR variant
     */
    private void ChangeActiveTarget(int _val)
    {
        for(int i=0; i< m_Variants.Count; i++)
        {
            if(i != _val)
            {
                m_Variants[i].SetActive(false);
            }
            else
            {
                m_Variants[i].SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        m_Variants[m_VariantDropdown.value].SetActive(true);
    }
    private void OnDisable()
    {
        foreach (var variant in m_Variants)
        {
            variant.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
