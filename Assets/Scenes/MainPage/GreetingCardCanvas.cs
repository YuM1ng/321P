using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingCardCanvas : MonoBehaviour
{
    public static GreetingCardCanvas Instance;
    
    void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
