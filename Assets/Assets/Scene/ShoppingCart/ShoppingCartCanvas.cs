using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingCartCanvas : MonoBehaviour
{
    public static ShoppingCartCanvas Instance;
    
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
