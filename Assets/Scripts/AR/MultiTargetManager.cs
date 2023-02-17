using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTargetManager : MonoBehaviour
{
    [SerializeField]
    List<EnableDisableTarget> m_AllTarget=new List<EnableDisableTarget>();
    EnableDisableTarget m_currTarget;

    public void EnableThisTarget(EnableDisableTarget _target)
    {
        foreach(var target in m_AllTarget) 
        {
            if(target == _target)
            {
                target.TargetEnable();
                m_currTarget= target;
            }
            else
            {
                target.TargetDisable();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var target in m_AllTarget)
        {
            target.TargetDisable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
